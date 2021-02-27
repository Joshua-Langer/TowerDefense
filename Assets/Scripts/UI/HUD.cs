using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI{
    public class HUD : MonoBehaviour
    {
        public Text playerHealth;
        public Text playerGold;
        public Text playerWave;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            playerGold.text = PlayerManager.Instance.Gold.ToString();
            playerHealth.text = PlayerManager.Instance.Health.ToString();
            playerWave.text = GameManager.Instance.Wave.ToString();
        }
    }
}
