using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scripts.UI.Resize
{
    public class VideoResizer: MonoBehaviour, IResizer
    {
        [SerializeField] private RectTransform rectTr;
        [SerializeField] private VideoPlayer player;

        private void Start()
        {
            if (rectTr == null)
                rectTr = GetComponent<RectTransform>();
            if (player == null)
                player = GetComponent<VideoPlayer>();
        }

        public float Height => rectTr.GetHeight();
        public void SetWidth(float value)
        {
            rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
            if (player.isPrepared)
            {
                float scale = value / (float)(player.width);
                rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scale*player.height);    
            }
            else
            {
                rectTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
            }
            
        }
    }
}