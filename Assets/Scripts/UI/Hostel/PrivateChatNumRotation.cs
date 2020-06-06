using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Scripts.UI.Hostel
{
    public class PrivateChatNumRotation : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        // Start is called before the first frame update
        int angle = 0;
        private float speed = 0.025f;
        Vector2 cos_sin = Vector2.zero;
    
        
        [SerializeField] private RectTransform[] nums;
        
        
        void Start()
        {
            cos_sin = new Vector2(Mathf.Cos(speed), Mathf.Sin(speed));
        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            cos_sin = new Vector2(Mathf.Cos(-speed), Mathf.Sin(-speed));
            InvokeRepeating("circleRotation", 0, 0.02f);
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            cos_sin = new Vector2(Mathf.Cos(speed), Mathf.Sin(speed));
            InvokeRepeating("circleRotation", 0, 0.02f);
        }
        
        private void circleRotation()
        {
            foreach (var num in nums)
            {
                num.anchoredPosition = new Vector2(num.anchoredPosition.x * cos_sin.x - num.anchoredPosition.y * cos_sin.y,
                        num.anchoredPosition.y * cos_sin.x + num.anchoredPosition.x * cos_sin.y);
            }

            if (++angle > 20)
            {
                angle = 0;
                CancelInvoke("circleRotation");
            }
                
        }
    }
}

