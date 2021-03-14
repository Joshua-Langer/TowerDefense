using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class SceneFader : MonoBehaviour
    {
        public Image image;
        public AnimationCurve curve;

        void Start()
        {
            StartCoroutine(FadeIn());
        }

        public void FadeTo(string scene)
        {
            StartCoroutine(FadeOut(scene));
        }

        IEnumerator FadeIn()
        {
            var t = 1f;
            
            while(t > 0f)
            {
                t -= Time.deltaTime;
                var a = curve.Evaluate(t);
                image.color = new Color(0, 0, 0, a);
                yield return 0;
            }
        }

        IEnumerator FadeOut(string scene)
        {
            var t = 0f;
            
            while(t < 1f)
            {
                t += Time.deltaTime;
                var a = curve.Evaluate(t);
                image.color = new Color(0, 0, 0, a);
                yield return 0;
            }

            SceneManager.LoadScene(scene);
        }
    }
}
