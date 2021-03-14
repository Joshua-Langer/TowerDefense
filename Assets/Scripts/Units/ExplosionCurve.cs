using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Units
{
    public class ExplosionCurve : MonoBehaviour
    {
        public Image sprite;
        public AnimationCurve curve;

        void Start()
        {
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            var t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;
                var a = curve.Evaluate(t);
                sprite.color = new Color(0, 0, 0, a);
                yield return 0;
            }
        }
    }
}
