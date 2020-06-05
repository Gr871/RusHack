using System.Collections;
using System.Collections.Generic;
using Scripts.Network.Url;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Network
{
    
    public class DBManager: MonoBehaviour, Interfaces.IUpdatable
    {
        #region Singleton
        public static DBManager Instance { get; private set; }

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

        [Header("URL")]
        private List<string> urlRequested = new List<string>();
        private bool CheckUrlRequests(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            
            if (urlRequested.IndexOf(url) >= 0)
                return false;
            
            urlRequested.Add(url);
            return true;
        }
        [SerializeField] private Url.UrlContainer urlContainer;

        [Header("IUpdatable")] [SerializeField] private float dt_upd;

        #region Monobehaviour
        
        private void Start()
        {
            DontDestroyOnLoad(this);

            
            
            //Callback после авторизации
            //StartUpdate();
        }

        private void OnDisable()
        {
            CancelUpdate();
            CancelInvoke();
            StopAllCoroutines();
        }
        #endregion
        


        public void Upload(UrlKeywords urlKey, WWWForm form, System.Action<bool, string> callback)
        {
            Upload(urlContainer.ValueByKey(urlKey), form, callback);
        }
        public void Upload(string url, WWWForm form, System.Action<bool, string> callback)
        {
            if(!CheckUrlRequests(url))
                return;
            StartCoroutine(Post(url, form, callback));
        }
        private IEnumerator Post(string url, WWWForm form, System.Action<bool, string> callback)
        {
            using (var w = UnityWebRequest.Post(url, form))
            {
                yield return w.SendWebRequest();
                if (w.isNetworkError || w.isHttpError)
                    callback(false, w.error);
                else
                    callback(true, w.downloadHandler.text);
            }

            urlRequested.Remove(url);
        }
        
        
        public void Download(UrlKeywords urlKey, System.Action<bool, string> callback)
        {
            Download(urlContainer.ValueByKey(urlKey), callback);
        }
        public void Download(string url, System.Action<bool, string> callback)
        {
            if(!CheckUrlRequests(url))
                return;
            StartCoroutine(Get(url, callback));
        }
        private IEnumerator Get(string url, System.Action<bool, string> callback)
        {
            using (var w = UnityWebRequest.Get(url))
            {
                yield return w.SendWebRequest();
                if (w.isNetworkError || w.isHttpError)
                    callback(false, w.error);
                else
                    callback(true, w.downloadHandler.text);
            }
            
            urlRequested.Remove(url);
        }

        #region IUpdatable
        public void StartUpdate()
        {
            InvokeRepeating("_Update", 0, dt_upd);
        }
        public void CancelUpdate()
        {
            CancelInvoke();
        }
        public void _Update()
        {
            Download(UrlKeywords.Update, DB_UpdateCallback);
        }

        private void DB_UpdateCallback(bool result, string response)
        {
            
        }
        #endregion
        
    }    
}

