using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Resize
{
    public class ImageResizer: MonoBehaviour, IResizer
    {
        [SerializeField] private RectTransform rectTr;
        [SerializeField] private RawImage rawImage;

        private void Start()
        {
            if (rectTr == null)
                rectTr = GetComponent<RectTransform>();
            if (rawImage == null)
                rawImage = GetComponent<RawImage>();
        }

        public float Height => rectTr.GetHeight();
        public void SetWidth(float value)
        {
            rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
            float scale = value / rawImage.texture.width;
            rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scale*rawImage.texture.height);
        }
    }
}