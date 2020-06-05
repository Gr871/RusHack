using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace Scripts.Video
{
    public class VideoManager: MonoBehaviour
    {
        public static string videoSaveFolder { get; private set; } = "";

        #region Singleton
        public static VideoManager Instance { get; private set; }

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
            videoSaveFolder = $"{Application.persistentDataPath}/Assets/Resources/Videos".Replace('/', '\\');
        }
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
        #endregion

        public static void LoadByUWRurl(string fn, string url, [CanBeNull] System.Action<string> saveCallback)
        {
            var pn = $"{videoSaveFolder}\\{fn}";
            if (System.IO.File.Exists(pn))
            {
                saveCallback?.Invoke(pn);
                return;
            }
                    
            
            Instance.StartCoroutine(Instance.CorLoadByUWRurl(fn, url, saveCallback));
        }
        private IEnumerator CorLoadByUWRurl(string fn, string url, [CanBeNull] System.Action<string> saveCallback)
        {
            using (var w = UnityWebRequest.Get(url))
            {
                yield return w.SendWebRequest();

                bool success = !(w.isHttpError || w.isNetworkError);
                if(success)
                    SaveFromByteArray(fn, w.downloadHandler.data, saveCallback);
            }
        }

        public static void LoadFromLocal(string fullPath, [CanBeNull] System.Action<byte[]> callback)
        {
            if (callback == null)
                System.IO.File.ReadAllBytes(fullPath);
            else
                callback(System.IO.File.ReadAllBytes(fullPath));
            
            Debug.Log("Loading is completed at main thread!");
        }
        
        public static void SaveFromByteArray(string fn, byte[] data, [CanBeNull] System.Action<string> saveCallback)
        {
            var pn = $"{videoSaveFolder}\\{fn}";
            if (!System.IO.Directory.Exists(videoSaveFolder))
                System.IO.Directory.CreateDirectory(videoSaveFolder);
            
            if(!System.IO.File.Exists(pn))
                System.IO.File.WriteAllBytes(pn, data);

            saveCallback?.Invoke(pn);
        }

        public static void PlayVideoByURL(VideoToTexture handler, string fn, bool local = false)
        {
            if(local)
                fn = File.File.GetUrlFromLocalFile(fn);
            
            var player = handler.source;

            player.source = VideoSource.Url;
            player.url = fn;
            
            handler.Run();
        }
    }
}