using System.Runtime.Serialization;

namespace Strunchik.Model.User;

[DataContract]
public class SerializableUser
{
    [DataMember(Name = "email")]
    public string Email = "";
    [DataMember(Name = "password")]
    public string Password = "";

    public SerializableUser()
    {
        
    }

    public SerializableUser(UserModel user)
    {
        this.Email = user.Email;
        this.Password = user.Password;
    }
}
