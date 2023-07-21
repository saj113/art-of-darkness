using System;
using UnityEngine;

namespace Level.SpawnEnemies.Models
{
    [RequireComponent(typeof(Collider2D))]
    public class SpawnTrigger : MonoBehaviour, ISpawnTrigger
    {
        public event Action<ISpawnTrigger> TriggerEntered;
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable(float position)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = new Vector2(position, gameObject.transform.position.y);
        }

        void OnTriggerEnter2D(Collider2D colliderComponent)
        {
            if (!colliderComponent.gameObject.CompareTag("Player")) return;
            
            OnTriggerEntered();
        }

        private void OnTriggerEntered()
        {
            TriggerEntered?.Invoke(this);
        }
    }
}