using System.Linq;
using UnityEngine;

namespace Core.Prefabs
{
    public abstract class PrefabProvider
    {
        protected T GetPrefab<T>(string path)
            where T : class
        {
            var variableForPrefab = Resources.Load(path, typeof(T));
            var result = variableForPrefab as T;

            Contract.Ensure(result != null, string.Format("Prefab by path '{0}' is not found!", path));
            return result;
        }
        
        protected T[] GetPrefabs<T>(string path)
            where T : class
        {
            var variableForPrefabs = Resources.LoadAll(path, typeof(T));
            Contract.Ensure(variableForPrefabs != null && variableForPrefabs.Any(), string.Format("Prefabs by path '{0}' is not found!", path));
            return variableForPrefabs.Cast<T>().ToArray();
        }
    }
}
