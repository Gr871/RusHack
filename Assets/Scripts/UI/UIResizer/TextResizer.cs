using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Resize
{
    public class TextResizer: MonoBehaviour, IResizer
    {
        [SerializeField] private RectTransform rectTr;
        [SerializeField] private ContentSizeFitter filter;

        public float Height => rectTr.GetHeight();
        
        private void Start()
        {
            if (rectTr == null)
                rectTr = GetComponent<RectTransform>();
            if (filter == null)
                filter = GetComponent<ContentSizeFitter>();
        }

        public void SetWidth(float value)
        {
            rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
            filter.SetLayoutVertical();
        }
    }
}