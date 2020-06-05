using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Scripts.Utils
{
    public enum GenerateDataType{Text, Texture, Video}
    
    public class GenerateManager: MonoBehaviour
    {
        #region Singleton
        public static GenerateManager Instance { get; private set; }

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

        private Dictionary<System.Type, GenerateDataType> dictTypes = new Dictionary<System.Type, GenerateDataType>();

        [Header("Resources")] 
        [SerializeField, ArrayElementTitle("type")] private GenerateObject[] generateSources;

        [SerializeField] private Transform templateCollector;

        private void Start()
        {
            Instance.dictTypes.Clear();
            Instance.dictTypes.Add(typeof(UnityEngine.UI.Text), GenerateDataType.Text);
            Instance.dictTypes.Add(typeof(UnityEngine.UI.RawImage), GenerateDataType.Texture);
            Instance.dictTypes.Add(typeof(Video.VideoToTexture), GenerateDataType.Video);
        }

        private GenerateObject GetSource<T>(out bool success)
        {
            var _t = typeof(T);
            success = false;
            
            if (!dictTypes.ContainsKey(_t))
                return null;

            if (!generateSources.Any(e => e.type == dictTypes[_t]))
                return null;

            success = true;
            return generateSources.FirstOrDefault(e => e.type == dictTypes[_t]);
        }

        public static void Generate<T>(System.Action<T> callback, Transform parent)
        {
            var source = Instance.GetSource<T>(out bool success);
            if(!success)
                return;
            
            source.Load<T>(callback, Instance.templateCollector,parent);
        }
        
    }

    [System.Serializable]
    public class GenerateObject
    {
        private const string resourceFolder = "UI/Templates";
        
        public GenerateDataType type;
        public Object prefab;
        [HideInInspector]public GameObject gameObject;
        
        
        public void Load<T>(System.Action<T> callback, Transform templateCollector, [CanBeNull] Transform root)
        {
            var parent = root == null ? Object.FindObjectOfType<Canvas>().transform : root;
            
            if (gameObject == null)
            {
                var request = Resources.LoadAsync<GameObject>($"{resourceFolder}/{prefab.name}");
                request.completed += delegate(AsyncOperation ao)
                {
                    gameObject = Object.Instantiate(((ResourceRequest) ao).asset, templateCollector) as GameObject;
                    gameObject.SetActive(false);

                    callback(Object.Instantiate<GameObject>(gameObject, parent).GetComponent<T>());
                };
                return;
            }
            
            callback(Object.Instantiate<GameObject>(gameObject, parent).GetComponent<T>());
        }
    }
}