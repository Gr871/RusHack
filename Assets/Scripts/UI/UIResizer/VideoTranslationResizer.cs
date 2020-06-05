using Scripts.Video;
using UnityEngine;

namespace Scripts.UI.Resize
{
    public class VideoTranslationResizer: MonoBehaviour, IResizer
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private bool autoResize = false;

        public float Height => rectTransform.GetHeight();

        public void SetWidth(float value)
        {
            WebCamTexture cam;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
            float height = value * 3.0f / 4.0f;
            
            if ((cam = GetComponent<WebCamVideo>().webCam) == null)
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            else
            {
                if (autoResize)
                    height = cam.height* value / (float) (cam.width);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  height);
            }
                
        }
    }
}