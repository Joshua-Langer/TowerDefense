namespace Tools.Edit
{
    public class ScriptTemplateEditorData
    {
        public int index;
        public int indexDiscard;
        public int indexReset;
        public string name;
        public string nameDiscard;
        public string nameReset;
        public string defaultFileName;
        public string defaultFileNameDiscard;
        public string defaultFileNameReset;
        public string content;
        public string contentDiscard;
        public string contentReset;
        public int hash;
        public bool isNew;
        public string ListName => nameDiscard + " - " + defaultFileNameDiscard;
        public string NewListName => name + " - " + defaultFileName;
        public string FullFileName => index + "-" + name.Replace("/", "__") + "-" + defaultFileName;
        public string OldFullFileName => indexDiscard + "-" + nameDiscard.Replace("/", "__") + "-" + defaultFileNameDiscard;

        public ScriptTemplateEditorData()
        {
            index = 1;
            content = "";
            hash = content.GetHashCode();
            name = "- New Template";
            defaultFileName = "";
        }

        public void SetDiscard()
        {
            indexDiscard = index;
            nameDiscard = name;
            defaultFileNameDiscard = defaultFileName;
            contentDiscard = content;
        }

        public void Discard()
        {
            index = indexDiscard;
            name = nameDiscard;
            defaultFileName = defaultFileNameDiscard;
            content = contentDiscard;
        }

        public bool NeedSave()
        {
            if(index != indexDiscard)
            {
                return true;
            }
            if(name != nameDiscard)
            {
                return true;
            }
            if(defaultFileName != defaultFileNameDiscard)
            {
                return true;
            }
            if(content != contentDiscard)
            {
                return true;
            }
            return false;
        }

        public void SetReset()
        {
            indexReset = index;
            nameReset = name;
            defaultFileNameReset = defaultFileName;
            contentReset = content;
        }

        public void Reset()
        {
            index = indexReset;
            name = nameReset;
            defaultFileName = defaultFileNameReset;
            content = contentReset;
        }
    }
}