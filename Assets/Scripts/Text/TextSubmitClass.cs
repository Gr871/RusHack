using System.Collections.Generic;
using Scripts.Network.Url;
using SocketIO;
using TMPro;
using UnityEngine;

namespace Scripts.Text
{
    public class TextSubmitClass:MonoBehaviour
    {
        private TMP_InputField textBox;
        private SocketIO.SocketIOComponent socket => SocketIOComponent.Instance;

        private void Start()
        {
            textBox = GetComponent<TMP_InputField>();
            socket.On("error", OnError);
        }

        private void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                Input.GetKeyDown(KeyCode.Return))
            {
                Submit();
            }
            
        }

        public void Submit()
        {
            string value = textBox.text;
            if(string.IsNullOrEmpty(value))
                return;

            if (value.EndsWith("\n"))
                value = value.Substring(0, value.Length - 1);
            textBox.text = "";

            Dictionary<string, string> kvps = new Dictionary<string, string>
            {
                {"mess", value},
                {"room", GetComponentInParent<UI.Hostel.HostelAnimatorController>().currentChannel},
                {"login", PlayerManager.Instance.loginData.user}
            };
            socket.Emit("reqMes", JSONObject.Create(kvps));
        }

        private void OnError(SocketIOEvent e)
        {
            Debug.Log($"{e.name}\n{e.name}");
        }
    }
}