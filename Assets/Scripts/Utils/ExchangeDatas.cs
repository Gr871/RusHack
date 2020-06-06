using System;
using UnityEngine;

namespace Scripts.Utils.ExchangeDatas
{
    [System.Serializable]
    public class LogInData: DataTemplate
    {
        public string user;
        public string pass;

        public bool Validate
            => !(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass));
    }

    
    [System.Serializable]
    public class SignUpData: DataTemplate
    {
        public LogInData logInData;
        public Utils.CommonDatas.PersonalData personalData;
        public Utils.CommonDatas.CompanyInfo companyData;
        
        public CommonDatas.CountryInfo country;
        public CommonDatas.HobbyInfo[] hobbies;
        //public CommonDatas.ConferenceInfo[] conferences;
    }
}