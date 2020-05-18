using System.Runtime.Serialization;

namespace BLL
{
    [DataContract]
    public class LogInfo
    {
        [DataMember]
        public string Login { get; set; }

        public LogInfo() { }

        public LogInfo(string Login)
        {
            this.Login = Login;
        }
    }
}
