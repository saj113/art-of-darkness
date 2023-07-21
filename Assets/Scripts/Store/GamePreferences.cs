using UnityEngine;

namespace Store
{
    public class GamePreferences : IGamePreferences
    {
        public int GetInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public string GetString(string key)
        {
            return PlayerPrefs.GetString(key, string.Empty);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}