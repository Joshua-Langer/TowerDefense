using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools.Edit
{
    public class ScriptTemplateEditor : EditorWindow
    {
        private bool isAdmin;
        private string jsonFolder;
        private string templateFolder;
        private List<ScriptTemplateEditorData> templates;
        private int selectedTemplate;
        private Vector2 contentScrollView;

        private ScriptTemplateEditorData Selected
        {
            get
            {
                if(templates == null || selectedTemplate > templates.Count - 1)
                {
                    return null;
                }
                return templates[selectedTemplate];
            }
            set
            {
                templates[selectedTemplate] = value;
            }
        }

        [MenuItem("Template Config/Script Templates")]
        private static void OpenWindow()
        {
            ((EditorWindow)(ScriptTemplateEditor)(object)EditorWindow.GetWindow(typeof(ScriptTemplateEditor), false, "Script Editor - Joshua Langer")).minSize = new Vector2(500f, 350f);
        }

        private void OnEnable()
        {
            templateFolder = Path.Combine(EditorApplication.applicationContentsPath, "Resources/ScriptTemplates/");
            jsonFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/GameTools/ScriptTemplateEditor/";
            if(!Directory.Exists(jsonFolder))
            {
                Directory.CreateDirectory(jsonFolder);
            }
            try
            {
                new DirectoryInfo(templateFolder).Attributes &= ~FileAttributes.ReadOnly;
                isAdmin = true;
            }
            catch(Exception)
            {
                isAdmin = false;
            }
            if(isAdmin)
            {
                Sync();
                LoadTemplates();
            }
        }

        private void OnGUI()
        {
            if(!isAdmin)
            {
                GUILayout.Label("You need to run Unity as an Admin in order to use Script Template Editor.", (GUILayoutOption[])(object)new GUILayoutOption[0]);
            }
            else
            {
                if(Selected == null)
                {
                    return;
                }
                GUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
                GUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[2]
                {
                    GUILayout.Width(300f),
                    GUILayout.MaxWidth(300f)
                });
                GUILayout.Space(5f);
                GUILayout.Label("Template", (GUILayoutOption[])(object)new GUILayoutOption[0]);
                var num = selectedTemplate;
                selectedTemplate = EditorGUILayout.Popup(selectedTemplate, templates.Select((ScriptTemplateEditorData x) => x.ListName).ToArray(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
                if(num != selectedTemplate && templates[num].NeedSave())
                {
                    if(EditorUtility.DisplayDialog("Discard Changes", "You have unsaved changes. If you click discard all of your changes will not be saved.", "Discard", "Stay"))
                    {
                        templates[num].Discard();
                    }
                    else
                    {
                        selectedTemplate = num;
                    }
                }
                GUILayout.Space(10f);
                GUILayout.Label("Name:", (GUILayoutOption[])(object)new GUILayoutOption[0]);
                Selected.name = EditorGUILayout.TextField(Selected.name, (GUILayoutOption[])(object)new GUILayoutOption[0]);
                GUILayout.Label((GUIContent)(object)new GUIContent("Use '/' to make folders.", "Ex. Folder1/Folder2/Name"), (GUILayoutOption[])(object)new GUILayoutOption[0]);
                GUILayout.Space(10f);
                GUILayout.Label("Menu Index:", (GUILayoutOption[])(object)new GUILayoutOption[0]);
                Selected.index = EditorGUILayout.IntField(Selected.index, (GUILayoutOption[])(object)new GUILayoutOption[0]);
                GUILayout.Space(10f);
                GUILayout.Label("Default File Name:", (GUILayoutOption[])(object)new GUILayoutOption[0]);
                Selected.defaultFileName = EditorGUILayout.TextField(Selected.defaultFileName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
                GUILayout.FlexibleSpace();
                if(Selected.isNew)
                {
                    //GUILayout.Label("Restart Unity to see new templates", (GUILayoutOption[])(object)new GUILayoutOption[0]);
                    if(GUILayout.Button("Create Template", EditorStyles.miniButton, (GUILayoutOption[])(object)new GUILayoutOption[1]
                    {
                        GUILayout.Width(300f)
                    }))
                    {
                        Save();
                    }
                }
                else
                {
                    GUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
                    if (GUILayout.Button("Save", EditorStyles.miniButton, (GUILayoutOption[])(object)new GUILayoutOption[1]
                    {
                        GUILayout.Width(150f)
                    }))
                    {
                        Save();
                    }
                    if (GUILayout.Button("Delete", EditorStyles.miniButton, (GUILayoutOption[])(object)new GUILayoutOption[1]
                    {
                        GUILayout.Width(150f)
                    }) && EditorUtility.DisplayDialog("Delete?", "Are you sure you want to delete " + Selected.name + "?", "Yes", "No"))
                    {
                        Delete();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
                    if (GUILayout.Button("Reset", EditorStyles.miniButton, (GUILayoutOption[])(object)new GUILayoutOption[1]
                    {
                        GUILayout.Width(150f)
                    }) && EditorUtility.DisplayDialog("Reset?", "Are you sure you want to reset " + Selected.name + "?", "Yes", "No"))
                    {
                        Discard();
                    }
                    if (GUILayout.Button("Reset To Original", EditorStyles.miniButton, (GUILayoutOption[])(object)new GUILayoutOption[1]
                    {
                        GUILayout.Width(150f)
                    }) && EditorUtility.DisplayDialog("Reset?", "Are you sure you want to reset " + Selected.name + " to the original?", "Yes", "No"))
                    {
                        ResetToOriginal();
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(5f);
                GUILayout.EndVertical();
                GUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
                contentScrollView = EditorGUILayout.BeginScrollView(contentScrollView, false, false, (GUILayoutOption[])(object)new GUILayoutOption[0]);
                Selected.content = EditorGUILayout.TextArea(Selected.content, (GUILayoutOption[])(object)new GUILayoutOption[2]
                {
                    GUILayout.ExpandHeight(true),
                    GUILayout.ExpandWidth(true)
                });
                EditorGUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
        
        private void LoadTemplates()
        {
            templates = new List<ScriptTemplateEditorData>();
            string[] files = Directory.GetFiles(jsonFolder);
            for(var i = 0; i < files.Length; i++)
            {
                ScriptTemplateEditorData scriptTemplateEditorData = new ScriptTemplateEditorData();
                EditorJsonUtility.FromJsonOverwrite(File.ReadAllText(files[i]), (object)scriptTemplateEditorData);
                scriptTemplateEditorData.SetDiscard();
                templates.Add(scriptTemplateEditorData);
            }
            ScriptTemplateEditorData scriptTemplateEditorData1 = new ScriptTemplateEditorData();
            scriptTemplateEditorData1.SetDiscard();
            scriptTemplateEditorData1.isNew = true;
            templates.Add(scriptTemplateEditorData1);
            selectedTemplate = 0;
        }

        private void Save()
        {
            if(!Validate())
            {
                return;
            }
            var newListName = Selected.NewListName;
            File.Delete(templateFolder + "/" + Selected.OldFullFileName + ".txt");
            File.Delete(jsonFolder + "/" + Selected.OldFullFileName + ".json");
            if(Selected.isNew)
            {
                Selected.SetReset();
            }
            Selected.isNew = false;
            Selected.hash = Selected.content.GetHashCode();
            CreateTemplateFile(Selected);
            File.WriteAllText(jsonFolder + "/" + Selected.FullFileName + ".json", EditorJsonUtility.ToJson((object)Selected));
            LoadTemplates();
            for(var i = 0; i < templates.Count; i++)
            {
                if(templates[i].ListName == newListName)
                {
                    selectedTemplate = i;
                }
            }
        }

        private void Delete()
        {
            File.Delete(templateFolder + "/" + Selected.OldFullFileName + ".txt");
            File.Delete(jsonFolder + "/" + Selected.OldFullFileName + ".json");
            LoadTemplates();
        }

        private void Discard()
        {
            Selected.Discard();
        }

        private void ResetToOriginal()
        {
            Selected.Reset();
        }

        private bool Validate()
        {
            string text = "\\|!$%&=?»«@£§€{}.-;'<>,";
            foreach (char value in text)
            {
                if (Selected.name.Contains(value))
                {
                    EditorUtility.DisplayDialog("Unable to save", "Name cannot contain special characters except from _ # ( ) /", "OK");
                    return false;
                }
            }
            if (Selected.name.Contains("__"))
            {
                EditorUtility.DisplayDialog("Unable to save", "Name cannot contain two _ after each other", "OK");
                return false;
            }
            if (Selected.name == string.Empty)
            {
                EditorUtility.DisplayDialog("Unable to save", "Name cannot be empty", "OK");
                return false;
            }
            bool flag = false;
            string[] array = templates.Select((ScriptTemplateEditorData x) => x.ListName).ToArray();
            for (int j = 0; j < array.Length; j++)
            {
                if (j != selectedTemplate && array[j].Equals(Selected.NewListName))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                EditorUtility.DisplayDialog("Unable to save", "There is already another template with the name " + Selected.name, "OK");
                return false;
            }
            if (Selected.name[0] == '/')
            {
                EditorUtility.DisplayDialog("Unable to save", "The name cannot begin with a /", "OK");
                return false;
            }
            if (Selected.name.EndsWith("/"))
            {
                EditorUtility.DisplayDialog("Unable to save", "The name cannot end with a /", "OK");
                return false;
            }
            if (Selected.name.Contains("//"))
            {
                EditorUtility.DisplayDialog("Unable to save", "The name cannot contains an empty folder", "OK");
                return false;
            }
            if (Selected.defaultFileName == string.Empty)
            {
                EditorUtility.DisplayDialog("Unable to save", "Default file name cannot be empty", "OK");
                return false;
            }
            text = "\\|!#$%&/()=?»«@£§€{}-;'<>,";
            foreach (char value2 in text)
            {
                if (Selected.defaultFileName.Contains(value2))
                {
                    EditorUtility.DisplayDialog("Unable to save", "Default file name cannot contain special characters except from _", "OK");
                    return false;
                }
            }
            if (Selected.defaultFileName.Contains("__"))
            {
                EditorUtility.DisplayDialog("Unable to save", "Default file name cannot contain two _ after each other", "OK");
                return false;
            }
            if (Selected.index < 1)
            {
                EditorUtility.DisplayDialog("Unable to save", "Index cannot be less then 1", "OK");
                return false;
            }
            return true;
        }

        private void Sync()
        {
            string[] files = Directory.GetFiles(jsonFolder);
            string[] files1 = Directory.GetFiles(templateFolder);
            for(var i = 0; i < files1.Length; i++)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files1[i]);
                var flag = true;
                string[] array = files;
                for(var j = 0; j < array.Length; j++)
                {
                    if(Path.GetFileNameWithoutExtension(array[j]) == fileNameWithoutExtension)
                    {
                        flag = false;
                    }
                }
                if(flag)
                {
                    CreateJsonFile(fileNameWithoutExtension);
                }
            }
            for(var k = 0; k < files.Length; k++)
            {
                ScriptTemplateEditorData scriptTemplateEditorData = new ScriptTemplateEditorData();
                EditorJsonUtility.FromJsonOverwrite(File.ReadAllText(files[k]), (object) scriptTemplateEditorData);
                var fullFileName = scriptTemplateEditorData.FullFileName;
                var flag2 = true;
                string[] array = files1;
                foreach(var path in array)
                {
                    if(Path.GetFileNameWithoutExtension(path) == fullFileName)
                    {
                        if(scriptTemplateEditorData.hash == File.ReadAllText(path).GetHashCode())
                        {
                            flag2 = false;
                        }
                        break;
                    }
                }
                if(flag2)
                {
                    CreateTemplateFile(scriptTemplateEditorData);
                }
            }
        }


        private void CreateJsonFile(string fileName)
        {
            ScriptTemplateEditorData scriptTemplateEditorData = new ScriptTemplateEditorData();
            string[] array = fileName.Split('-');
            scriptTemplateEditorData.index = int.Parse(array[0]);
            scriptTemplateEditorData.name = array[1].Replace("__", "/");
            scriptTemplateEditorData.defaultFileName = array[2];
            scriptTemplateEditorData.content = File.ReadAllText(templateFolder + fileName + ".txt");
            scriptTemplateEditorData.hash = scriptTemplateEditorData.content.GetHashCode();
            scriptTemplateEditorData.SetReset();
            File.WriteAllText(jsonFolder + "/" + scriptTemplateEditorData.FullFileName + ".json", EditorJsonUtility.ToJson((object)scriptTemplateEditorData));
        }

        private void CreateTemplateFile(ScriptTemplateEditorData jsonData)
        {
            File.WriteAllText(templateFolder + "/" + jsonData.FullFileName + ".txt", jsonData.content);
        }

        public ScriptTemplateEditor()
        {}
    }
}