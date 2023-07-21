using System;
using System.Collections.Generic;
using Core;
using Skills;
using Utilities;

namespace Store
{
    public class Repository : IRepository
    {
        private readonly IGamePreferences _gamePreferences;
        private readonly HashSet<TalentNameKey> _selectedTalentNameKeys;
        private static IRepository _instance;
        private Repository()
        {
            var testGamePreferences = FinderUtility.GetComponent<TestGamePreferences>();
            _gamePreferences = testGamePreferences != null 
                ? (IGamePreferences)testGamePreferences 
                : new GamePreferences();

            Level = _gamePreferences.GetInt(Preferences.Level, 1);
            AvailablePoints = _gamePreferences.GetInt(Preferences.AvailablePoints);
            NextMission = _gamePreferences.GetInt(Preferences.NextMission);

            var selectedTalents = _gamePreferences.GetString(Preferences.SelectedTalents);
            if (string.IsNullOrEmpty(selectedTalents))
            {
                _selectedTalentNameKeys = GetDefaultTalentNameKeys();
            }
            else
            {
                _selectedTalentNameKeys = TalentNameKeyConverter.GetKeysFromString(selectedTalents);
            }
        }

        public static IRepository Instance
        {
            get 
            {
                return _instance ?? (_instance = new Repository());
            }
        }

        public event Action AchievementsChanged;
        public event Action LevelChanged;

        public int AvailablePoints { get; private set; }

        public int NextMission { get; private set; }

        public int Level { get; private set; }

        public void LevelUp()
        {
            Level++;
            AvailablePoints++;
            _gamePreferences.SetInt(Preferences.Level, Level);
            _gamePreferences.SetInt(Preferences.AvailablePoints, AvailablePoints);
            _gamePreferences.Save();
            OnLevelChanged();
        }

        public void GoToNextMission(int newValue)
        {
            NextMission = newValue;
            _gamePreferences.SetInt(Preferences.NextMission, NextMission);
            _gamePreferences.Save();
        }

        public bool IsTalentSelected(TalentNameKey key)
        {
            return _selectedTalentNameKeys.Contains(key);
        }

        public void SelectTalent(TalentNameKey key)
        {
            Contract.Ensure(AvailablePoints >= 1, "_availablePoints < 1");

            _selectedTalentNameKeys.Add(key);
            AvailablePoints--;
            _gamePreferences.SetString(
                Preferences.SelectedTalents, 
                TalentNameKeyConverter.GetKeysInString(_selectedTalentNameKeys));
            _gamePreferences.SetInt(Preferences.AvailablePoints, AvailablePoints);
            _gamePreferences.Save();
            OnAchievementsChanged();
        }

        private void OnAchievementsChanged()
        {
            var handler = AchievementsChanged;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnLevelChanged()
        {
            var handler = LevelChanged;
            if (handler != null)
            {
                handler();
            }
        }

        private HashSet<TalentNameKey> GetDefaultTalentNameKeys()
        {
            var result = new HashSet<TalentNameKey>
            {
                TalentNameKey.Magicka_ShadowBolts,
                TalentNameKey.Magicka_NecromanticSword,
                TalentNameKey.Magicka_Resurrect,
                TalentNameKey.Magicka_Shade
            };

            _gamePreferences.SetString(
                Preferences.SelectedTalents, 
                TalentNameKeyConverter.GetKeysInString(result));
            _gamePreferences.Save();

            return result;
        }
    }
}