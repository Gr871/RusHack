using System;

namespace Scripts.Utils
{
    public enum BinnaryDataType {Null, Image}
    [AttributeUsage(AttributeTargets.All)]
    public class BinnaryData:System.Attribute
    {
        public readonly BinnaryDataType type; 
        
        public BinnaryData(BinnaryDataType type)
        {
            this.type = type;
        }
    }
}