using UnityEngine;

namespace TowerDefense.Units
{
    public class HealthBar : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        private float originalScale;
        
        // Start is called before the first frame update
        void Start()
        {
            originalScale = gameObject.transform.localScale.x;
        }

        // Update is called once per frame
        void Update()
        {
            var tmpScale = gameObject.transform.localScale;
            tmpScale.x = currentHealth / maxHealth * originalScale;
            gameObject.transform.localScale = tmpScale;
        }
    }
}
