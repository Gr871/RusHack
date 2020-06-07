using Scripts.Stream;
using UnityEngine;

namespace Scripts.Camera
{
    public class HelpCameraSwitch:MonoBehaviour
    {
        private void OnMouseDown()
        {
            CameraSwitcher.Instance.Show(0);
            SwitchStream.GlobalStop();
        }
    }
}