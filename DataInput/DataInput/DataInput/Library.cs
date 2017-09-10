using Xamarin.Forms;

public class Library : Application
{
    public string LoadSetting(string key)
    {
        if (Application.Current.Properties[key] != null)
        {
            return (string)Application.Current.Properties[key];
        }
        else
        {
            return string.Empty;
        }
    }

    public void SaveSetting(string key, string value)
    {
        Application.Current.Properties[key] = value;
    }
}