using Scripts.Network.Url;
using UnityEngine;
using UnityEngine.UI;

using Scripts.Utils.ExchangeDatas;

namespace Scripts.UI.ClientBack
{
    public class AuthorizeSend: MonoBehaviour
    {
        [SerializeField] private InputField userField;
        [SerializeField] private InputField passwordField;

        private LogInData logInData;
        public void Send()
        {
            logInData = new LogInData()
            {
                user = userField.text,
                pass = passwordField.text
            };

            if (!logInData.Validate)
            {
                Debug.Log($"Uncorrect username:[{logInData.user}] or password:[{logInData.pass}]");
                return;
            }
            
            Scripts.Network.DBManager.Upload(UrlKeywords.LogIn, logInData.GetForm(), AuthorizeCallback);
        }

        
        public void AuthorizeCallback(bool success, string information)
        {
            Debug.Log($"Result: [{(success ? "Succeeded" : "Failed")}]");
            Debug.Log($"Information:\n{information}");
            
            if(!success)
                return;
            
            //Костыль
            if(information.Length <= 2)
                return; //Не найден пользователь

            PlayerManager.Instance.loginData = logInData;
            
            NavigateManager.GoToChatTypeSelectWindow(gameObject);
        }
        
    }
}