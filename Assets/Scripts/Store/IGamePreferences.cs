namespace Store
{
    public interface IGamePreferences
    {
        string GetString(string key);
        int GetInt(string key, int defaultValue = 0);
        void SetInt(string key, int value);
        void SetString(string key, string value);
        void Save();
    }
}