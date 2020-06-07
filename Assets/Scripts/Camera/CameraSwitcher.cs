using System.Linq;
using UnityEngine;

namespace Scripts.Camera
{
    public class CameraSwitcher: MonoBehaviour
    {
        #region Singleton
        public static CameraSwitcher Instance { get; private set; }

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
        
        
        [System.Serializable]
        public struct CameraInfo
        {
            public GameObject camera;
            public int type;
            public GameObject[] additionalObjects;

            public CameraInfo(CameraInfo info)
            {
                camera = info.camera;
                type = info.type;
                additionalObjects = info.additionalObjects.ToArray();
            }
        }

        public CameraInfo[] infos;
        public CameraInfo currentInfo;

        public void Show(int type)
        {
            var newInfo = infos.First(inf => inf.type == type);
            
            currentInfo.camera.SetActive(false);
            newInfo.camera.SetActive(true);
            
            foreach (var oldObj in currentInfo.additionalObjects)
                oldObj.SetActive(false);
            foreach (var newObj in newInfo.additionalObjects)
                newObj.SetActive(true);
            
            currentInfo = new CameraInfo(newInfo);
        }

    }
}