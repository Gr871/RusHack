using UnityEngine;

namespace Scripts.Text
{
    public class TextSubmitClass:MonoBehaviour
    {
        private void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                Input.GetKeyDown(KeyCode.Return))
            {
                
            }
            
        }

        public void Submit()
        {
            
        }
    }
}