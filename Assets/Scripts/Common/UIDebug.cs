using System.Collections.Generic;

using UnityEngine;

using Scripts.Network;
using ui = UnityEngine.UI;

namespace Scripts.Common
{
    public class UIDebug: MonoBehaviour
    {
        private List<string> msgs = new List<string>();
        [SerializeField] private ui.Text textWindow;

        [Header("DBManager test")] 
        [SerializeField] private ui.InputField url;


        public void DB_Test_Post()
        {
            WWWForm form = new WWWForm();
            form.AddField("Keyword", "Hello");
            DBManager.Instance.Upload(url.text, form, DB_Test_Callback);
        }
        public void DB_Test_Get()
        {
            DBManager.Instance.Download(url.text, DB_Test_Callback);
        }


        public void DB_Test_Callback(bool result, string msg)
        {
            string h = result ? "Success" : "Error";
            string newMsg = $"[{h}]:{msg}{System.Environment.NewLine}";
            
            msgs.Add(newMsg);
            textWindow.text += newMsg;
            if (msgs.Count > 9)
            {
                textWindow.text = textWindow.text.Remove(0, msgs[0].Length);
                msgs.RemoveAt(0);
            }
        }
    }
}