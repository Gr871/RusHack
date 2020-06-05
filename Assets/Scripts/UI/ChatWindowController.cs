using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scripts.UI
{
    public class ChatWindowController: MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private float yDelta = 10;
        [SerializeField] private Vector2 baseSize;
        public RectTransform root => _root;
        
        private List<RectTransform> pastedElements = new List<RectTransform>();
        
        
        private void Start()
        {
            
        }

        public void AddElement(RectTransform element)
        {
            AddElements(new []{element});
        }
        public void AddElements(IEnumerable<RectTransform> newElements)
        {
            float lastPositionY = 0;
            if (pastedElements.Any())
                lastPositionY = LastExistedPosition;
                
            foreach (var element in newElements)
                lastPositionY = SetAtWindow(element, lastPositionY);
            
            pastedElements.AddRange(newElements);

            ResizeWindow();
        }

        private float SetAtWindow(RectTransform element, float lastPosition)
        {
            lastPosition += yDelta;
            element.GetComponent<UI.Resize.IResizer>().SetWidth(baseSize.x);
            float sizeY = element.GetHeight();
                
            element.SetParent(_root,true);
            element.localScale = Vector3.one;
            element.anchoredPosition = Vector2.down * (lastPosition + (sizeY / 2.0f)) ;
                
            return lastPosition + sizeY;
        }

        private float LastExistedPosition
        {
            get
            {
                var lastRect = pastedElements.Last();
                return -lastRect.anchoredPosition.y + (lastRect.GetHeight() / 2.0f);
            }
        }

        private void ResizeWindow()
        {
            if(pastedElements.Count == 0)
                return;

            var lastPos = LastExistedPosition;
            if (baseSize.y < lastPos)
            {
                baseSize.y = lastPos;
                _root.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, baseSize.y);
            }
        }
    }
}