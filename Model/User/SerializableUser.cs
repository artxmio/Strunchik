using System.Runtime.Serialization;

namespace Strunchik.Model.User;

[DataContract]
public class SerializableUser
{
    [DataMember(Name = "email")]
    public string Email = "";
    [DataMember(Name = "name")]
    public string Name = "";
    [DataMember(Name = "password")]
    public string Password = "";

    public SerializableUser()
    {
        
    }

    public SerializableUser(UserModel user)
    {
        this.Email = user.Email;
        this.Name = user.Name;
        this.Password = user.Password;
    }
}
