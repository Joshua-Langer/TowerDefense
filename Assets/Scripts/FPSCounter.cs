using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class FPSCounter : MonoBehaviour
    {
        public Text fpsDisplay;

        void Update()
        {
            var fps = 1 / Time.unscaledDeltaTime;
            fpsDisplay.text = string.Format("FPS {0:0.}", fps);
        }
    }
}
