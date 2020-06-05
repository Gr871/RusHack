using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Scripts.Network.Url
{
    public enum UrlKeywords {Null = 0, SignUp, LogIn, LogOut, Update}
    
    [CreateAssetMenu(fileName = "urlContainer", menuName = "Network/UrlContainer")]
    public class UrlContainer: ScriptableObject
    {
        [System.Serializable]
        public struct KeyURL
        {
            public UrlKeywords Key;
            public string Value;
        }


        [SerializeField, ArrayElementTitle("Key")] private KeyURL[] Datas;

        public string ValueByKey(UrlKeywords key)
        {
            return Datas.FirstOrDefault(d => d.Key == key).Value;
        }

        public UrlKeywords KeyByValue(string url)
        {
            if (!Datas.Any(d => d.Value == url))
                return UrlKeywords.Null;

            return Datas.First(d => d.Value == url).Key;
        }
    }
}