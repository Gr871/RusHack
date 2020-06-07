using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Video;
using SocketIO;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Privatechat
{
    public class PrivateChatMainController : MonoBehaviour
    {
        [SerializeField] private float t_oppWindDes = 1.0f;
        
        [SerializeField] private Graphic[] opponentWindows;
        [SerializeField] private Graphic logo;
        [SerializeField] private TranslateContainerData translateWindow;
        
        private string[] channels = new string[0];
        private int lastSelected = -1;
        [SerializeField]private ChatWindowController chatWindow;
        private Animator animator;
        private int backstep = 0;
        [SerializeField] private WebCamVideo myProfile;
        
        private SocketIO.SocketIOComponent socket => SocketIOComponent.Instance;
        public string currentChannel => channels[lastSelected];

        private void Start()
        {
            //chatWindow = GetComponentInChildren<ChatWindowController>();
            animator = gameObject.GetComponent<Animator>();
        }

        public void GetDataOnStart()
        {
            socket.On("canals", OnChannelResponse);
            StartCoroutine(ChannelRequest());
        }
        
        public void SelectOpponent(int index)
        {
            backstep++;
            animator.SetTrigger("OpenChat");
            
            lastSelected = index;
            OpenCloseChat(true);
            
            socket.On("saveSms", OnPrivateRoomSmsResponse);
            StartCoroutine(PrivateRoomSmsRequest(index));
            
            myProfile.Run();
        }

        public void Hide()
        {
            if (--backstep < 0)
            {
                BtnClose();
                return;
            }
            
            animator.ResetTrigger("OpenChat");
            animator.SetTrigger("HideChat");
            OpenCloseChat(false);
        }

        private void BtnClose()
        {
            myProfile.Stop();
            
            backstep = 0;
            animator.ResetTrigger("HideChat");
            animator.SetTrigger("CloseWindow");
        }

        public void BackToMenu()
        {
            NavigateManager.GoToMainMenu(gameObject);
        }
        
        private void OpenCloseChat(bool open)
        {
            var list = opponentWindows.ToList();
            list.RemoveAt(lastSelected);
            StartCoroutine(Dessolve(list.SelectMany(el=> el.GetComponentsInChildren<Graphic>()), !open));
            StartCoroutine(Dessolve(new [] {logo}, !open));

            if (open)
            {
                translateWindow.rect = opponentWindows[lastSelected].rectTransform;
                translateWindow.startPoint = translateWindow.rect.anchoredPosition;
            }
            StartCoroutine(TranslateContainer(translateWindow.rect, open ? translateWindow.startPoint : translateWindow.endPoint, open ? translateWindow.endPoint : translateWindow.startPoint, translateWindow.t_translate));   
        }

        #region Socket
        private IEnumerator ChannelRequest()
        {
            yield return new WaitForSeconds(1);
            socket.Emit("sendCanals");
        }

        private void OnChannelResponse(SocketIOEvent e)
        {
            var container = e.data.GetField("canals");
            channels = Enumerable.Range(0, container.list.Count)
                .Select(i => container.list[i].list[0].str).ToArray();
            
            Debug.Log("Channels are ready!");
        }
        private IEnumerator PrivateRoomSmsRequest(int selectedIndex)
        {
            yield return new WaitForSeconds(1);
            Dictionary<string, string> json = new Dictionary<string, string>();
            json.Add("login", PlayerManager.Instance.loginData.user);
            json.Add("room", channels[selectedIndex]);
            
            socket.Emit("room", JSONObject.Create(json));
        }
        private void OnPrivateRoomSmsResponse(SocketIOEvent e)
        {
            var container = e.data.GetField("saveSms");
            var msgs = Enumerable.Range(0, container.list.Count)
                .Select(i => container.list[i]).Select(msg =>
                    $"<margin=1em>от {msg.list[1].str}</margin>\n\n<margin=5em><size=120%>{msg.list[0].str}</size></margin>").ToArray();

            
            foreach (var msg in msgs)
                LoadSms(msg);
            
            socket.On("message", OnPrivateRoomGetMessageResponse);
        }
        private void OnPrivateRoomGetMessageResponse(SocketIOEvent e)
        {
            LoadSms($"<margin=1em>от {e.data.list[1].str}</margin>\n\n<margin=5em><size=120%>{e.data.list[0].str}</size></margin>");
        }

        public void LoadSms(string msg)
        {
            Scripts.Utils.GenerateManager.Generate<Text.TextMeshProAppender>(
                delegate(Text.TextMeshProAppender textAppender)
                {
                    textAppender.gameObject.SetActive(true);
                        
                    textAppender.AppendText(msg);
                    chatWindow.AddElement(textAppender.GetComponent<RectTransform>());
                }, chatWindow.root);
        }

        #endregion
        
        private IEnumerator TranslateContainer(RectTransform rect, Vector2 startPoint, Vector2 endPoint, float t_delta)// 357.65  374.4
        {
            float t_start = Time.time, ct;

            while ((ct = (Time.time - t_start) / t_delta) < 1f)
            {
                rect.anchoredPosition = Vector2.Lerp(startPoint, endPoint, ct);
                yield return null;
            }
            rect.anchoredPosition = endPoint;
        }
        private IEnumerator Dessolve(IEnumerable<Graphic> imgs, bool show)
        {
            float t_start = Time.time, ct;
            float start = show ? 0f : 1f, end = show ? 1f : 0f;
            while ((ct = (Time.time - t_start) / t_oppWindDes) < 1)
            {
                foreach (var img in imgs)
                    img.color = img.color.ChangeAlpha(Mathf.Lerp(start, end, ct));
                
                yield return null;
            }
            foreach (var img in imgs)
                img.color = img.color.ChangeAlpha(end);

            foreach (var btn in imgs.Select(img=> img.GetComponent<Button>()).Where(el=> el != null))
                btn.interactable = show;
        }
        
        [System.Serializable]
        public struct TranslateContainerData
        {
            public RectTransform rect;
            public Vector2 startPoint;
            public Vector2 endPoint;
            public float t_translate;
        }
    }    

}

