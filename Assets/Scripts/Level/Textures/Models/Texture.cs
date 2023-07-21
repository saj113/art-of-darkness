using UnityEngine;

namespace Level.Textures.Models
{
    public class Texture : MonoBehaviour, ITexture
    {
        public void Initialize(Vector2 position)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = position;
        }
    }
}