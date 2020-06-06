using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Text
{
    public class TextManager : MonoBehaviour
    {
        #region Singleton
        public static TextManager Instance { get; private set; }

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
        
        
        
        public static string GetSmileText(int index)
            => $"<sprite index={index}>";
        
    }
}

