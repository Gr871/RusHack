using UnityEngine;

namespace Scripts
{
    public class NavigateManager: MonoBehaviour
    {
        #region Singleton
        public static NavigateManager Instance { get; private set; }

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
        
        [SerializeField] private GameObject ChatTypeSelectWindow;
        
        public static void GoToChatTypeSelectWindow(GameObject currentWindow)
        {
            currentWindow.SetActive(false);
            Instance.ChatTypeSelectWindow.SetActive(true);
        }
        
    }
}