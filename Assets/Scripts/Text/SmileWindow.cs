using TMPro;
using UnityEngine;

namespace Scripts.Text
{
    public class SmileWindow: MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        
        public void PutSmile(int index)
        {
            inputField.text += TextManager.GetSmileText(index);
        }
    }
}