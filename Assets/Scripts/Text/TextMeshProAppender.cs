using TMPro;
using UnityEngine;

namespace Scripts.Text
{
    public class TextMeshProAppender: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI field;
        
        public void AppendText(string text)
        {
            field.text += text;
        }
    }
}