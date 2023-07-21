using Core;
using UnityEngine;

namespace GUIScripts.Elements
{
    [RequireComponent(typeof(AudioSource))]
    public class MenuAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _buttonClickSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            Contract.Require(_buttonClickSound != null, "_buttonClickSound == null");
        }

        public void PlayButtonClicked()
        {
            _audioSource.PlayOneShot(_buttonClickSound);
        }
    }
}
