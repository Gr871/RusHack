using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Scripts.Video
{
    public class VideoToRenderer:MonoBehaviour, IStreamer
    {
        [SerializeField] private bool playOnStart = false;
        [SerializeField]private Renderer destination;
        [SerializeField]private VideoPlayer _source;
        
        public VideoPlayer source => _source;
        
        
        private void OnEnable()
        {
            if(playOnStart)
                Run();
        }

        public void Run()
        {
            source.renderMode = VideoRenderMode.MaterialOverride;
            source.targetMaterialRenderer = destination;
            
            StartCoroutine(
                Play(
                    source.source == VideoSource.VideoClip ?
                        new WaitWhile(() => source.clip == null) :
                        new WaitWhile(()=> string.IsNullOrEmpty(source.url))
                )
            );
        }

        public void Stop()
        {
            source.Stop();
        }
        
        private void PlayOnReady(VideoPlayer player)
        {
            player.Play();
        }

        private IEnumerator Play(WaitWhile sourceClipReady)
        {
            yield return sourceClipReady;

            source.prepareCompleted += PlayOnReady;
            source.Prepare();
        }
    }
}