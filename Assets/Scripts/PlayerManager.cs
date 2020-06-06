using Scripts.Utils.ExchangeDatas;
using UnityEngine;

namespace Scripts
{
    public class PlayerManager: MonoBehaviour
    {
        #region Singleton
        public static PlayerManager Instance { get; private set; }

        private void Init()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void Awake()
        {
            Init();
        }
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
        #endregion
        
        public Scripts.Utils.ExchangeDatas.LogInData loginData = new LogInData();
        
    }
}