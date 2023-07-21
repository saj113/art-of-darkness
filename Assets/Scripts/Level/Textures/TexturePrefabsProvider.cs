using Core.Prefabs;
using Level.Textures.Models;

namespace Level.Textures
{
    public class TexturePrefabsProvider : PrefabProvider, ITexturePrefabsProvider
    {
        public Texture[] GetDarkTreePrefabs()
        {
            return GetPrefabs<Texture>("Textures/DarkTrees");
        }
    }
}