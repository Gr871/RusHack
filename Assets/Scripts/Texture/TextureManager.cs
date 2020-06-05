using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Scripts.File;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


namespace Scripts.Texture
{
    public class TextureManager : MonoBehaviour
    {
        #region Singleton
        public static TextureManager Instance { get; private set; }

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

        [Obsolete]
        public static void LoadByWWWurl(RawImage rawImage, string url, [CanBeNull] System.Action callback)
        {
            Instance.StartCoroutine(Instance.CorLoadByWWWurl(rawImage, url, callback));
        }
        public static void LoadByUWRurl(RawImage rawImage, string url, [CanBeNull] System.Action<bool> callback)
        {
            Instance.StartCoroutine(Instance.CorLoadByUWRurl(rawImage, url, callback));
        }

        [Obsolete]
        private IEnumerator CorLoadByWWWurl(RawImage rawImage, string url, [CanBeNull] System.Action callback)
        {
            WWW www = new WWW (url);
            yield return new WaitUntil(()=> www.isDone);
            
            rawImage.texture = www.texture;
            callback?.Invoke();
        }
        private IEnumerator CorLoadByUWRurl(RawImage rawImage, string url, [CanBeNull] System.Action<bool> callback)
        {
            using (var w = UnityWebRequest.Get(url))
            {
                yield return w.SendWebRequest();

                bool success = !(w.isHttpError || w.isNetworkError);
                if(success)
                    LoadFromByteArray(rawImage, w.downloadHandler.data, callback);
                callback?.Invoke(success);
            }
        }

        public static void LoadFromLocal(RawImage rawImage, string path, [CanBeNull] System.Action callback)
        {
            LoadFromByteArray(rawImage, System.IO.File.ReadAllBytes(path), null);
            callback?.Invoke();
        }
        
        public static void LoadFromByteArray(RawImage rawImage, byte[] data, [CanBeNull] System.Action<bool> callback)
        {
            Texture2D sample = new Texture2D(2,2);
            bool isLoaded = sample.LoadImage(data);
            if (isLoaded)
                rawImage.texture = sample;
            callback?.Invoke(isLoaded);
        }

        public static void SaveLocal(RawImage image)
        {
            var fileSettings = OpenFileSettings.Base;
            fileSettings.defExt = "jpg";
            var fn = FileDialog.Save(fileSettings);
            if(string.IsNullOrEmpty(fn))
                return;
            
            System.IO.File.WriteAllBytes(fn, (image.texture as Texture2D).EncodeToJPG());
        }


        public static Vector2Int GetPixelSize(RawImage image)
            => new Vector2Int(image.texture.width, image.texture.height);

        public static byte[] Encode(string gameObjectSource)
            => ((Texture2D) GameObject.Find(
                gameObjectSource.Substring(1, gameObjectSource.Length - 1)
                    .Replace('\\', '/')
                ).GetComponent<RawImage>().texture)
                .EncodeToJPG();
    }    
}

