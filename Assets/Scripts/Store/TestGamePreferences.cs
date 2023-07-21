using System.Linq;
using Skills;
using UnityEngine;

namespace Store
{
    public class TestGamePreferences : MonoBehaviour, IGamePreferences
    {
        [SerializeField]
        private int _level;
        [SerializeField]
        private int _availablePoints;
        [SerializeField]
        private int _nextMission;
        [SerializeField]
        private TalentNameKey[] _talentNameKeys;

        public int GetInt(string key, int defaultValue)
        {
            switch(key)
            {
                case Preferences.Level:
                    return _level;
                case Preferences.AvailablePoints:
                    return _availablePoints;
                case Preferences.NextMission:
                    return _nextMission;
            }

            return defaultValue;
        }

        public string GetString(string key)
        {
            if (key == Preferences.SelectedTalents && _talentNameKeys.Length > 0)
            {
                return TalentNameKeyConverter.GetKeysInString(_talentNameKeys);
            }
            
            return string.Empty;
        }

        public void Save()
        {
            // do nothing
        }

        public void SetInt(string key, int value)
        {
            switch(key)
            {
                case Preferences.Level:
                    _level = int.Parse(key);
                    break;

                case Preferences.AvailablePoints:
                    _availablePoints = int.Parse(key);
                    break;

                case Preferences.NextMission:
                    _nextMission = int.Parse(key);
                    break;
            }
        }

        public void SetString(string key, string value)
        {
            if (key == Preferences.SelectedTalents)
            {
                _talentNameKeys = TalentNameKeyConverter.GetKeysFromString(value).ToArray();
            }
        }
    }
}