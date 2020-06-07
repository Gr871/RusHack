using Scripts.Stream;
using UnityEngine;

namespace Scripts.Video
{
    public class VideoReciever: MonoBehaviour
    {
        private void OnMouseDown()
        {
            SwitchStream.GlobalPlay();
            gameObject.SetActive(false);
        }
    }
}