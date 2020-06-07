using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scripts.Video
{
    public class VideoToTexture: MonoBehaviour
    {
        [SerializeField] private bool playOnStart = false;
        [SerializeField] private bool autoPlay = false;
        [SerializeField]private RawImage destination;
        [SerializeField]private VideoPlayer _source;
        [SerializeField] private UI.Resize.VideoResizer resizer;
        public VideoPlayer source => _source;
        public RectTransform rectTransform => destination.rectTransform;
        
        private void Start()
        {
            if(playOnStart)
                Run();
        }

        public void Run()
        {
            StartCoroutine(
                Play(
                    source.source == VideoSource.VideoClip ?
                    new WaitWhile(() => source.clip == null) :
                    new WaitWhile(()=> string.IsNullOrEmpty(source.url))
                    )
               );
        }
        
        private void PlayOnReady(VideoPlayer player)
        {
            destination.texture = player.texture;
            if(resizer != null)
                resizer.SetWidth(destination.rectTransform.GetWidth());
            player.Play();
        }

        private IEnumerator Play(WaitWhile sourceClipReady)
        {
            yield return sourceClipReady;

            source.prepareCompleted += PlayOnReady;
            source.Prepare();
        }

        private void FixedUpdate()
        {
            if(!autoPlay)
                return;
            
            if(!source.isPrepared)
                return;

            if (source.isPaused && destination.color.a > 0.1f)
                source.Play();
            else if (source.isPlaying && destination.color.a < 0.1f)
                source.Pause();
        }
    }
}