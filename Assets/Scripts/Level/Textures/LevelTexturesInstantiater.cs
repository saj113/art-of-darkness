using System;
using Core;
using Core.UnityFramework;
using Level.Textures.Models;
using UnityEngine;
using Texture = Level.Textures.Models.Texture;

namespace Level.Textures
{
    public class LevelTexturesInstantiater : ILevelTexturesInstantiater
    {
        private readonly ITexturePrefabsProvider _texturePrefabsProvider;
        private readonly IGameObjectInstantiater _gameObjectInstantiater;
        private readonly Texture[] _treeTexturePrefabs;
        private readonly Transform _treesParent;

        public LevelTexturesInstantiater(
            ITexturePrefabsProvider texturePrefabsProvider, 
            IGameObjectInstantiater gameObjectInstantiater,
            int level,
            Transform treesParent)
        {
            _texturePrefabsProvider = texturePrefabsProvider;
            _gameObjectInstantiater = gameObjectInstantiater;
            _treeTexturePrefabs = GetPrefabs(level);
            _treesParent = treesParent;
        }

        public ITexture InstantiateNewTreeTexture()
        {
            var randomElement = _treeTexturePrefabs.GetRandomElement();
            var texture = _gameObjectInstantiater.Instantiate(randomElement, false);
            texture.transform.parent = _treesParent;
            return texture;
        }

        private Texture[] GetPrefabs(int level)
        {
            switch (level)
            {
                case 1:
                    return _texturePrefabsProvider.GetDarkTreePrefabs();
                
                default:
                    throw new NotSupportedException($"The level {level} is not supported");
            }
        }
    }
}