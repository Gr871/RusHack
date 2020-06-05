using Scripts.Network.Url;
using Scripts.Utils.CommonDatas;
using Scripts.Utils.ExchangeDatas;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Test
{
    public class TestSignUp: MonoBehaviour
    {
        [SerializeField] private Transform container;
        
        private void Start()
        {
            Invoke("_Start", 1.0f);
        }

        private void _Start()
        {
            Scripts.Utils.GenerateManager.Generate<UnityEngine.UI.RawImage>(
                delegate(RawImage image) 
                { 
                    Texture.TextureManager.LoadFromLocal(image,@"C:\Users\User\Pictures\Saved Pictures\Снимок5.PNG",null);
                    image.gameObject.SetActive(true);
                    
                    var obj = new SignUpData()
                    {
                        logInData = new LogInData()
                        {
                            user = "user",
                            pass = "password"
                        },
                        personalData = new PersonalData()
                        {
                            email = "@mail.ru",
                            dateOfBirth = System.DateTime.Now.ToString("yyyy-MM-dd"),
                            firstName  = "Andrew",
                            lastName = "Kuzin"
                            ,
                            profileImage = $"${image.gameObject.FullName()}"
                        },
                        companyData = new CompanyInfo()
                        {
                            company = "Russian Army",
                            position = "Tupoi sro4"
                        },
                        country = new CountryInfo()
                        {
                            country = "Russia",
                            language = SystemLanguage.Russian
                        },
                        hobbies = new HobbyInfo[]
                        {
                            new HobbyInfo() {hobby = "Programming"},
                            new HobbyInfo() {hobby = "Ur mom fucking"}
                        }
                    };
                    
                    //Debug.Log("Rere");
                    Scripts.Network.DBManager.Instance.Upload(UrlKeywords.SignUp, obj.GetForm(), (b, s) => { Debug.Log($"Success?:{b} | Info:{s}");});
                },  container
            );
        }
    }
}