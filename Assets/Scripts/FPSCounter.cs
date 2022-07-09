using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefense
{
    public class FPSCounter : MonoBehaviour
    {
        public TextElement fpsDisplay;

        void Update()
        {
            var fps = 1 / Time.unscaledDeltaTime;
            fpsDisplay.text = string.Format("FPS {0:0.}", fps);
        }
    }
}
