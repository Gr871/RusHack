using Scripts.UI.Resize;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Video
{
    public enum WebCamDestinationType {RawImage, Renderer}
    public class WebCamVideo: MonoBehaviour, IStreamer
    {
        private int camID = -1;
        [SerializeField] private WebCamDestinationType type;
        [SerializeField] private VideoTranslationResizer resizer;
        [SerializeField] private GameObject noCameraText;
        [SerializeField] private bool playOnAwake = true;

        
        
        public WebCamTexture webCam { get; private set; }
        
        private void OnEnable()
        {
            if(playOnAwake)
                Run();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void Run()
        {
            if(camID == -1)
                SwitchCamera();

            webCam = new WebCamTexture(WebCamTexture.devices[camID].name);
            switch (type)
            {
                case WebCamDestinationType.RawImage:
                {
                    GetComponent<RawImage>().texture = webCam;
                    break;
                }
                case WebCamDestinationType.Renderer:
                {
                    GetComponent<Renderer>().material.mainTexture = webCam;
                    break;
                }
            }
            if(resizer != null)
                resizer.SetWidth(GetComponent<RectTransform>().GetWidth());
            webCam.Play();
        }
        public void Stop()
        {
            if(webCam == null)
                return;
            
            webCam.Stop();
            switch (type)
            {
              case WebCamDestinationType.RawImage:
                  {
                      GetComponent<RawImage>().texture = null;
                      break;
                  }
              case WebCamDestinationType.Renderer:
                  {
                    GetComponent<Renderer>().material.mainTexture = null;
                    break;
                  }
            }
            webCam.Stop();
            webCam = null;
        }
        
        
        public void SwitchCamera()
        {
            camID += 1;
            int size = WebCamTexture.devices.Length; 
            if (size == 0)
            {
                NoCameras();
                return;
            }
            camID %= size;
        }

        private void NoCameras()
        {
            camID = -1;
            if(noCameraText == null)
                return;
            
            noCameraText.SetActive(true);
            InvokeRepeating("InvNoCamera", 0, 1);
        }
        private void InvNoCamera()
        {
            if (WebCamTexture.devices.Length == 0)
                return;
    
            noCameraText.SetActive(false);
            SwitchCamera();
            CancelInvoke("InvNoCamera");
        }
    }
}