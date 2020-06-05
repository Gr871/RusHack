using UnityEngine;
using Scripts.File;

namespace Scripts.Test
{
    public class TestGenerate: MonoBehaviour
    {
        [SerializeField] private UI.ChatWindowController chatWindowController;
        [SerializeField] private string remoteVideoURL = "http://10.13.4.52:5000/get/video/";
        
        private void Start()
        {
                    //Show text
        Scripts.Utils.GenerateManager.Generate<UnityEngine.UI.Text>(
            delegate(UnityEngine.UI.Text text)
            {
                text.text = "Hello!\nI just write this text to test ContentSizeFilterComponent\nGood luck!";
                text.gameObject.SetActive(true);
                chatWindowController.AddElement(text.rectTransform);
            }, chatWindowController.root);

        //show image from local file
        string imgName = Scripts.File.FileDialog.Open(OpenFileSettings.Base);
        if (!string.IsNullOrEmpty(imgName))
        {
            Scripts.Utils.GenerateManager.Generate<UnityEngine.UI.RawImage>(
                delegate(UnityEngine.UI.RawImage image)
                {
                    Scripts.Texture.TextureManager.LoadFromLocal(image, imgName, null);
                    image.gameObject.SetActive(true);
                    chatWindowController.AddElement(image.rectTransform);
                }, chatWindowController.root);
        }
        
        //show video from local file
        string videoName = Scripts.File.FileDialog.Open(OpenFileSettings.Base);
        if (!string.IsNullOrEmpty(videoName))
        {
            Scripts.Video.VideoManager.LoadFromLocal(videoName, null);
            Scripts.Utils.GenerateManager.Generate<Scripts.Video.VideoToTexture>(
                delegate(Scripts.Video.VideoToTexture converter)
                {
                    converter.gameObject.SetActive(true);
                    Scripts.Video.VideoManager.PlayVideoByURL(converter, videoName, true);
                    
                    chatWindowController.AddElement(converter.rectTransform);
                }, chatWindowController.root);
        }
        
        //show video from remote
        Scripts.Utils.GenerateManager.Generate<Scripts.Video.VideoToTexture>(
            delegate(Scripts.Video.VideoToTexture converter)
            {
                converter.gameObject.SetActive(true);
                Scripts.Video.VideoManager.PlayVideoByURL(converter, remoteVideoURL);
                    
                chatWindowController.AddElement(converter.rectTransform);
            }, chatWindowController.root);
        }
    }
}