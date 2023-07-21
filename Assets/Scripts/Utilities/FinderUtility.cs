using System.Linq;
using Core;
using Stats;
using Stats.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
    public static class FinderUtility
    {
        private static ICharacterStats _characterStatsCached;

        public static ICharacterStats GetPlayerStats()
        {
            if (_characterStatsCached != null)
            {
                return _characterStatsCached;
            }

            var component = GetComponent<CharacterStatsData>();
            Contract.Ensure(component != null);

            _characterStatsCached = component.Stats;

            return _characterStatsCached;
        }

        public static T GetComponent<T>() 
            where T : MonoBehaviour
        {
            return Object.FindObjectsOfType<T>().FirstOrDefault();
        }
    }
}
