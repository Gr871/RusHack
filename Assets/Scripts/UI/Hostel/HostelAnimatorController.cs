using  System.Linq;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Hostel
{
    public class HostelAnimatorController:MonoBehaviour
    {
        
        [Header("Private chat")]
        [SerializeField] private Graphic[] showOnStart;
        [SerializeField] private Graphic[] privateClickHide;
        [SerializeField] private Graphic[] privateClickShow;
        [SerializeField] private Graphic[] opponentWindows;
        [SerializeField] private Graphic[] opponentWindowClickShow;
        
        [SerializeField] private float t_opponentWindowDessolve = 1.0f;
        [SerializeField] private TranslateContainerData translateDataContainer;

        [SerializeField] private GameObject[] chatObects;
        /////
        private string[] users = new string[0]; 
        
        
        
        private SocketIO.SocketIOComponent socket => SocketIOComponent.Instance;
        
        private void Start()
        {
            StartCoroutine(Dessolve(showOnStart, true));
        }

        public void PrivateChatBtnClick()
        {
            StartCoroutine(Dessolve(privateClickHide, false));
            StartCoroutine(Dessolve(privateClickShow, true));
            
            socket.On("loginUsers", OnLoginUsersResponse);
            StartCoroutine(LoginUsersRequest());
        }

        #region Socket
        private IEnumerator LoginUsersRequest()
        {
            yield return new WaitForSeconds(1);
            socket.Emit("send");
        }

        private void OnLoginUsersResponse(SocketIOEvent e)
        {
            var container = e.data.GetField("loginUsers");
            users = Enumerable.Range(0, container.list.Count)
                .Select(i => container.list[i].list[0].str).ToArray();

            for (int i = 0; i < users.Length; i++)
                Debug.Log($"{i} : {users[i]}");
            
        }
        #endregion
        

        public void PrivateChatCloseAnotherOpponents(int current)
        {
            var list = opponentWindows.ToList();
            list.RemoveAt(current);
            StartCoroutine(Dessolve(list.SelectMany(el=> el.GetComponentsInChildren<Graphic>()), false));
            
            StartCoroutine(Dessolve(opponentWindowClickShow, true));
            
            translateDataContainer = new TranslateContainerData(opponentWindows[current].rectTransform, translateDataContainer);
            StartCoroutine(TranslateContainer(translateDataContainer.rect, translateDataContainer.endPoint, translateDataContainer.t_translate));
            foreach (var obj in chatObects)
                obj.SetActive(true);
            
            
            socket.On("create", OnPrivateRoomCreateResponse);
            StartCoroutine(PrivateRoomRequest(current));
        }

        #region Socket

        private IEnumerator PrivateRoomRequest(int selectedIndex)
        {
            yield return new WaitForSeconds(1);
            Dictionary<string, string> json = new Dictionary<string, string>();
            json.Add("login1", PlayerManager.Instance.loginData.user);
            json.Add("login2", users[selectedIndex]);
            socket.Emit("personalRoom", JSONObject.Create(json));
        }

        private void OnPrivateRoomCreateResponse(SocketIOEvent e)
        {
            Debug.Log(e.name);
            Debug.Log(e.data);
        }
        #endregion

        private IEnumerator Dessolve(IEnumerable<Graphic> imgs, bool show)
        {
            float t_start = Time.time, ct;
            float start = show ? 0f : 1f, end = show ? 1f : 0f;
            while ((ct = (Time.time - t_start) / t_opponentWindowDessolve) < 1)
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

        private IEnumerator TranslateContainer(RectTransform rect, Vector2 endPoint, float t_delta)// 357.65  374.4
        {
            Vector2 startPoint = rect.anchoredPosition;
            float t_start = Time.time, ct;

            while ((ct = (Time.time - t_start) / t_delta) < 1f)
            {
                rect.anchoredPosition = Vector2.Lerp(startPoint, endPoint, ct);
                yield return null;
            }
            rect.anchoredPosition = endPoint;
        }
        
    }

    [System.Serializable]
    public struct TranslateContainerData
    {
        public RectTransform rect;
        public Vector2 startPoint;
        public Vector2 endPoint;
        public float t_translate;

        public TranslateContainerData(RectTransform rect, TranslateContainerData containerData)
        {
            this.rect = rect;
            startPoint = rect.anchoredPosition;
            endPoint = containerData.endPoint;
            t_translate = containerData.t_translate;
        }
    }
}

public static partial class Extensions
{
    public static Color ChangeAlpha(this Color color, float alpha)
        => new Color(color.r, color.g, color.b, alpha);

}