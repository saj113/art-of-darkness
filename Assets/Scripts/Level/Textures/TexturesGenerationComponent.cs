using Core;
using Core.UnityFramework;
using Store;
using UnityEngine;

namespace Level.Textures
{
    public class TexturesGenerationComponent : MonoBehaviour
    {
        [SerializeField] private Transform TreesParent;
        
        private void Start()
        {
            TreesParent.ThrowIfNull(nameof(TreesParent));
            
            var levelPreferences = InstanceContainer.Instance.Resolve<ILevelPreferences>();
            
            var texturesPrefabProvider = new TexturePrefabsProvider();
            var gameObjectInstantiater = new GameObjectInstantiater();
            var levelTexturesProvider = new LevelTexturesInstantiater(
                texturesPrefabProvider, 
                gameObjectInstantiater, 
                Repository.Instance.Level,
                TreesParent);
            
            var textureInitializer = new TextureInitializer(levelPreferences, levelTexturesProvider);
            textureInitializer.InitializeTextures();
        }
    }
}