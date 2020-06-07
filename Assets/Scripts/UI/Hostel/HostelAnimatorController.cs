using  System.Linq;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Hostel
{
    public class HostelAnimatorController:MonoBehaviour
    {
        
//        [Header("Private chat")]
//        [SerializeField] private Graphic[] showOnStart;
//        //[SerializeField] private Graphic[] privateClickHide;
//        [SerializeField] private Graphic[] privateClickShow;
//        [SerializeField] private Graphic[] opponentWindows;
//        [SerializeField] private Graphic[] opponentWindowClickShow;
//        
//        [SerializeField] private float t_opponentWindowDessolve = 1.0f;
////        [SerializeField] private TranslateContainerData opponentTranslateData;
////        [SerializeField] private TranslateContainerData privateWindowTranslateData;
//        
//        [SerializeField] private GameObject[] chatObects;
//        /////
//        private string[] channels = new string[0];
//        private int lastSelected = -1;
//        private ChatWindowController chatWindow;
//        public string currentChannel => channels[lastSelected];
//        
//        
//        
//        private SocketIO.SocketIOComponent socket => SocketIOComponent.Instance;
//        
//        private void Start()
//        {
//            StartCoroutine(Dessolve(showOnStart, true));
//            chatWindow = chatObects[0].GetComponent<ChatWindowController>();
//        }
//
//        public void PrivateChatBtnClick()
//        {
//            StartCoroutine(Dessolve(showOnStart, false));
//            StartCoroutine(Dessolve(privateClickShow, true));
//            
//            socket.On("canals", OnChannelResponse);
//            StartCoroutine(ChannelRequest());
//        }
//
//        #region Socket
//        private IEnumerator ChannelRequest()
//        {
//            yield return new WaitForSeconds(1);
//            socket.Emit("sendCanals");
//        }
//
//        private void OnChannelResponse(SocketIOEvent e)
//        {
//            var container = e.data.GetField("canals");
//            channels = Enumerable.Range(0, container.list.Count)
//                .Select(i => container.list[i].list[0].str).ToArray();
//        }
//        #endregion
//        
//
//        public void PrivateChatCloseAnotherOpponents(int current)
//        {
//            lastSelected = current;
//            OpenClosePrivateChat(true);
//
//            socket.On("saveSms", OnPrivateRoomSmsResponse);
//            StartCoroutine(PrivateRoomSmsRequest(current));
//        }
//        private void OpenClosePrivateChat(bool open)
//        {
//            var list = opponentWindows.ToList();
//            list.RemoveAt(lastSelected);
//            StartCoroutine(Dessolve(list.SelectMany(el=> el.GetComponentsInChildren<Graphic>()), !open));
//            
//            StartCoroutine(Dessolve(opponentWindowClickShow, open));
//            
////            opponentTranslateData = new TranslateContainerData(opponentWindows[lastSelected].rectTransform, opponentTranslateData);
////            privateWindowTranslateData = new TranslateContainerData(privateWindowTranslateData);
//            StartCoroutine(TranslateContainer(opponentTranslateData.rect, opponentTranslateData.startPoint, opponentTranslateData.endPoint, opponentTranslateData.t_translate));
//            StartCoroutine(TranslateContainer(privateWindowTranslateData.rect, privateWindowTranslateData.endPoint,privateWindowTranslateData.startPoint, privateWindowTranslateData.t_translate));
//            foreach (var obj in chatObects)
//                obj.SetActive(open);
//        }
//
//        #region Socket
//        private IEnumerator PrivateRoomSmsRequest(int selectedIndex)
//        {
//            yield return new WaitForSeconds(1);
//            Dictionary<string, string> json = new Dictionary<string, string>();
//            json.Add("login", PlayerManager.Instance.loginData.user);
//            json.Add("room", channels[selectedIndex]);
//            
//            socket.Emit("room", JSONObject.Create(json));
//        }
//        private void OnPrivateRoomSmsResponse(SocketIOEvent e)
//        {
//            var container = e.data.GetField("saveSms");
//            var msgs = Enumerable.Range(0, container.list.Count)
//                .Select(i => container.list[i]).Select(msg =>
//                    $"<margin=1em>от {msg.list[1].str}</margin>\n\n<margin=5em><size=120%>{msg.list[0].str}</size></margin>").ToArray();
//
//            
//            foreach (var msg in msgs)
//                LoadSms(msg);
//            
//            socket.On("message", OnPrivateRoomGetMessageResponse);
//        }
//        private void OnPrivateRoomGetMessageResponse(SocketIOEvent e)
//        {
//            LoadSms($"<margin=1em>от {e.data.list[1].str}</margin>\n\n<margin=5em><size=120%>{e.data.list[0].str}</size></margin>");
//        }
//
//        public void LoadSms(string msg)
//        {
//            Scripts.Utils.GenerateManager.Generate<Text.TextMeshProAppender>(
//                delegate(Text.TextMeshProAppender textAppender)
//                {
//                    textAppender.gameObject.SetActive(true);
//                        
//                    textAppender.AppendText(msg);
//                    chatWindow.AddElement(textAppender.GetComponent<RectTransform>());
//                }, chatWindow.root);
//        }
//        #endregion
//
//        public void ClosePrivateChat()
//        {
//            socket.Off("canals", OnPrivateRoomSocketClose);
//            socket.Off("saveSms", OnPrivateRoomSocketClose);
//            chatObects[0].GetComponent<ChatWindowController>().Clear();
//            
//            OpenClosePrivateChat(false);
//        }
//
//        private void OnPrivateRoomSocketClose(SocketIOEvent e)
//        {
//            Debug.Log(e.data);
//        }
//
//        private IEnumerator Dessolve(IEnumerable<Graphic> imgs, bool show)
//        {
//            float t_start = Time.time, ct;
//            float start = show ? 0f : 1f, end = show ? 1f : 0f;
//            while ((ct = (Time.time - t_start) / t_opponentWindowDessolve) < 1)
//            {
//                foreach (var img in imgs)
//                    img.color = img.color.ChangeAlpha(Mathf.Lerp(start, end, ct));
//                
//                yield return null;
//            }
//            foreach (var img in imgs)
//                img.color = img.color.ChangeAlpha(end);
//
//            foreach (var btn in imgs.Select(img=> img.GetComponent<Button>()).Where(el=> el != null))
//                btn.interactable = show;
//
//        }
//
//        private IEnumerator TranslateContainer(RectTransform rect, Vector2 startPoint, Vector2 endPoint, float t_delta)// 357.65  374.4
//        {
//            float t_start = Time.time, ct;
//
//            while ((ct = (Time.time - t_start) / t_delta) < 1f)
//            {
//                rect.anchoredPosition = Vector2.Lerp(startPoint, endPoint, ct);
//                yield return null;
//            }
//            rect.anchoredPosition = endPoint;
//        }
//        
    }
}

public static partial class Extensions
{
    public static Color ChangeAlpha(this Color color, float alpha)
        => new Color(color.r, color.g, color.b, alpha);

}