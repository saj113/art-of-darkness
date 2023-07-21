using System.Collections.Generic;
using Core;
using Level.Textures.Models;
using UnityEngine;
using Utilities;

namespace Level.Textures
{
    public class TextureInitializer : ITextureInitializer
    {
        private const int LevelCutInMeters = 10;
        private const int MaxTreesPerLevelCut = 10;
        
        private readonly ILevelPreferences _levelPreferences;
        private readonly ILevelTexturesInstantiater _levelTexturesInstantiater;

        public TextureInitializer(ILevelPreferences levelPreferences, ILevelTexturesInstantiater levelTexturesInstantiater)
        {
            _levelPreferences = levelPreferences.ThrowIfNull(nameof(levelPreferences));
            _levelTexturesInstantiater = levelTexturesInstantiater.ThrowIfNull(nameof(levelTexturesInstantiater));
        }

        public void InitializeTextures()
        {
            var horizontalBounds = GetHorizontalFromToBounds();
            var verticalBound = new FromToBound((int)_levelPreferences.InitialBottomBorder, (int)_levelPreferences.InitialTopBorder);
            
            InitializeTrees(horizontalBounds, verticalBound);
        }

        private void InitializeTrees(FromToBound[] horizontalBounds, FromToBound verticalBound)
        {
            foreach (var horizontalBound in horizontalBounds)
            {
                var treesCount = ValueUtility.GetRandom(0, MaxTreesPerLevelCut);
                for (var i = 0; i < treesCount; i++)
                {
                    var randomX = ValueUtility.GetRandom(horizontalBound.From, horizontalBound.To);
                    var randomY = ValueUtility.GetRandom(verticalBound.From, verticalBound.To);
                    var tree = _levelTexturesInstantiater.InstantiateNewTreeTexture();
                    tree.Initialize(new Vector2(randomX, randomY));
                }
            }
        }

        private FromToBound[] GetHorizontalFromToBounds()
        {
            var list = new List<FromToBound>();
            for (var from = (int)_levelPreferences.InitialLeftBorder; from < _levelPreferences.InitialRightBorder; from += LevelCutInMeters)
            {
                var to = from + LevelCutInMeters;
                list.Add(new FromToBound(from, to));
            }

            return list.ToArray();
        }
    }
}