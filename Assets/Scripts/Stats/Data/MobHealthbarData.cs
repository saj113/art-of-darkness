using System;
using UnityEngine;

namespace Stats.Data
{
    [Serializable]
    public class MobHealthbarData
    {
        [SerializeField] private SpriteRenderer _spriteRendererPrefab;
        [SerializeField] private float _yOffset = 1.5f;

        public SpriteRenderer SpriteRendererPrefab
        {
            get { return _spriteRendererPrefab; }
        }

        public float YOffset
        {
            get { return _yOffset; }
        }
    }
}
