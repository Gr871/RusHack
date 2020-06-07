using System.Collections.Generic;
using Scripts.Video;
using UnityEngine;
using UnityEngine.Video;

namespace Scripts.Stream
{
    public enum StreamType {Video, Camera}
    public class SwitchStream: MonoBehaviour
    {
        public static List<SwitchStream> Instances = new List<SwitchStream>();

        [SerializeField] private StreamType type;
        [SerializeField] private GameObject importer;
        private void Awake()
        {
            Instances.Add(this);
        }

        public static void GlobalPlay()
        {
            foreach (var inst in Instances)
                inst.Play();
        }

        public static void GlobalStop()
        {
            foreach (var inst in Instances)
                inst.Stop();
        }
        
        

        public void Play()
        {
            GetComponent<IStreamer>().Run();
        }

        public void Stop()
        {
            GetComponent<IStreamer>().Stop();
        }

        private void OnMouseDown()
        {
            Export();
        }

        private void Export()
        {
            GlobalStop();
            importer.SetActive(true);
            switch (type)
            {
                case StreamType.Video:
                {
                    importer.GetComponent<WebCamVideo>().Stop();
                    importer.GetComponent<VideoPlayer>().url = GetComponent<VideoPlayer>().url;
                    importer.GetComponent<VideoToRenderer>().Run();
                    break;
                }
                case StreamType.Camera:
                {
                    importer.GetComponent<VideoToRenderer>().Stop();
                    importer.GetComponent<WebCamVideo>().Run();
                    break;
                }
            }
        }
    }
}