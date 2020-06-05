using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;


namespace Scripts.Utils
{
    [System.Serializable]
    public class DataTemplate
    {
        private static readonly System.Type templateType = typeof(DataTemplate);
        
        public DataTemplate(){}

        public static T Generate<T>(string json) where T: DataTemplate
            => JsonUtility.FromJson<T>(json);
        
        public override string ToString()
            => JsonUtility.ToJson(this);
        
        public virtual WWWForm GetForm()
        {
            var form = new WWWForm();

            var infos = GetFieldsValues();
            foreach (var fieldInfo in infos)
            {
                form.AddField(
                    fieldInfo.key,
                    fieldInfo.isBinnary ?
                        System.Convert.ToBase64String(LoadBinnaryData(fieldInfo)):
                        (string)fieldInfo.value
                    );
            }

            return form;
        }

        private byte[] LoadBinnaryData(FieldInfo info)
        {
            byte[] value;
            switch (info.binnaryType)
            {
                case BinnaryDataType.Image:
                {
                   var source = (string) info.value;
                   value = 
                       source[0] == '$' ?
                           Scripts.Texture.TextureManager.Encode(source):
                           System.IO.File.ReadAllBytes(source);
                   break;
                }
                default:
                {
                    value = new byte[0];
                    break;
                }
            }

            return value;
        }

        public virtual IEnumerable<FieldInfo> GetFieldsValues()
        {
            var arr = new List<FieldInfo>();
            foreach (var field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                //Проверка значения
                var value = field.GetValue(this);
                if (value == null)
                    continue;

                //Получение формата бинарных данных
                BinnaryDataType binnaryType = BinnaryDataType.Null;
                var attrs = field.GetCustomAttributes(typeof(BinnaryData));
                if (attrs.Any())
                    binnaryType = (attrs.First() as BinnaryData).type;
                
                var _t = field.FieldType;
                var fieldName = field.Name;//$"{parent}{field.Name}";
                
                //Обработка для массива
                if (_t.IsArray)
                {
                    List<string> jsonValues = new List<string>();
                    foreach (var element in (System.Array)value)
                        jsonValues.Add(element.ToString());
                    arr.Add(new FieldInfo(fieldName, System.String.Join("|", jsonValues)));
                    continue;
                }
                
                //Обработка дочернего класса от DataTemplate
                if (_t.IsSubclassOf(templateType))
                {
                    arr.AddRange(((DataTemplate) value).GetFieldsValues());//($"{fieldName}."));
                    continue;
                }
                
                //Добавление поля
                arr.Add( new FieldInfo(
                    fieldName ,
                    binnaryType,
                    _t.IsEnum ? 
                        System.Convert.ToInt32(value).ToString() :
                        value.ToString())
                );
            }
            return arr;
        }

        public void Test(System.Action<string> call)
        {
//            foreach (var field in GetFieldsValues(""))
//                call($"{field.key} : {(string) field.value}");

            call(System.String.Join("\n", GetFieldsValues().Select(v => v.key)));
        }
        
        public struct FieldInfo
        {
            public string key;
            public Utils.BinnaryDataType binnaryType;
            public object value;

            public bool isBinnary => binnaryType != BinnaryDataType.Null;

            public FieldInfo(string key, object value)
            {
                this.key = key;
                this.binnaryType = BinnaryDataType.Null;
                this.value = value;
            }
            
            public FieldInfo(string key, BinnaryDataType binnaryType, object value)
            {
                this.key = key;
                this.binnaryType = binnaryType;
                this.value = value;
            }
        }
    }
}
namespace Scripts.Utils.CommonDatas
{
    [System.Serializable]
    public class CountryInfo: DataTemplate
    {
        public string country;
        public SystemLanguage language;
    }

    [System.Serializable]
    public class PersonalData : DataTemplate
    {
        public string firstName;
        public string lastName;
        public string email;
        public string dateOfBirth;

        [BinnaryData(BinnaryDataType.Image)]
        public string profileImage;
    }

    [System.Serializable]
    public class CompanyInfo : DataTemplate
    {
        public string company;
        public string position;
    }
    
    [System.Serializable]
    public class HobbyInfo: DataTemplate
    {
        public string hobby;
    }

    [System.Serializable]
    public class ConferenceInfo:DataTemplate
    {
        public string conference;
    }
}