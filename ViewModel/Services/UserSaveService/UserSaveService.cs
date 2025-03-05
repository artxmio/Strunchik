using Newtonsoft.Json;
using Strunchik.Model.User;
using System.IO;
using System.Windows;

namespace Strunchik.ViewModel.Services.UserSaveService;

public class UserSaveService
{
    private string ApplicationFolder = "";

    public SerializableUser User { get; set; } = null!;

    public UserSaveService()
    {
        GetApplicationFolder();
        LoadUserData();
    }

    private void GetApplicationFolder()
    {
        var localApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var companyName = "app";
        var applicationName = "strunchik";

        ApplicationFolder = Path.Combine(localApplicationPath, companyName, applicationName);

        Directory.CreateDirectory(ApplicationFolder);
    }

    public void SaveUserData(UserModel user)
    {
        try
        {
            var serializableUser = new SerializableUser(user);
            var json = JsonConvert.SerializeObject(serializableUser);

            File.WriteAllText(ApplicationFolder + @"\data.json", json);
        }
        catch (JsonSerializationException ex)
        {
            MessageBox.Show("Ошибка сериализации JSON: " + ex.Message, "Внимание");
        }
        catch (JsonWriterException ex)
        {
            MessageBox.Show("Ошибка записи JSON: " + ex.Message, "Внимание");
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message, "Внимание");
        }
        catch (IOException ex)
        {
            MessageBox.Show("Ошибка ввода-вывода при записи в файл: " + ex.Message, "Внимание");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Ошибка доступа при записи в файл: " + ex.Message, "Внимание");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }
    public void DeleteUserData()
    {
        try
        {
            File.Delete(ApplicationFolder + @"\data.json");
        }
        catch (JsonSerializationException ex)
        {
            MessageBox.Show("Ошибка сериализации JSON: " + ex.Message, "Внимание");
        }
        catch (JsonWriterException ex)
        {
            MessageBox.Show("Ошибка записи JSON: " + ex.Message, "Внимание");
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show("Ошибка инициализации: " + ex.Message, "Внимание");
        }
        catch (IOException ex)
        {
            MessageBox.Show("Ошибка ввода-вывода при записи в файл: " + ex.Message, "Внимание");
        }
        catch (UnauthorizedAccessException ex)
        {
            MessageBox.Show("Ошибка доступа при записи в файл: " + ex.Message, "Внимание");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }
    public void LoadUserData()
    {
        if (File.Exists(ApplicationFolder + @"\data.json"))
        {
            var jsonString = File.ReadAllText(ApplicationFolder + @"\data.json");

            User = JsonConvert.DeserializeObject<SerializableUser>(jsonString);
        }
    }

}
