using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gossip.Menus
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Slider _MasterSlider;
        [SerializeField] private Slider _MusicSlider;
        [SerializeField] private Slider _SFXSlider;

        [SerializeField] private Button _QuitButton;

        private const string MASTER_VOLUME = "master";
        private const string MUSIC_VOLUME = "music";
        private const string SFX_VOLUME = "sfx";

        void Start()
        {
            _MasterSlider.onValueChanged.AddListener(delegate { SetVolume(_MasterSlider, MASTER_VOLUME); });
            _MusicSlider.onValueChanged.AddListener(delegate { SetVolume(_MusicSlider, MUSIC_VOLUME); });
            _SFXSlider.onValueChanged.AddListener(delegate { SetVolume(_SFXSlider, SFX_VOLUME); });

            _QuitButton.onClick.AddListener(QuitSettings);

            SetSliders();
        }

        private void SetVolume(Slider pSlider, string pVolumeType)
        {
            switch (pVolumeType)
            {
                case MASTER_VOLUME:
                    AudioManager.masterVolume = pSlider.value;
                    break;
                case MUSIC_VOLUME:
                    AudioManager.musicVolume = pSlider.value;
                    break;
                case SFX_VOLUME:
                    AudioManager.SFXVolume = pSlider.value;
                    break;
            }

            AudioManager.instance.UpdateVolume();
        }

        private void SetSliders()
        {
            _MasterSlider.value = AudioManager.masterVolume;
            _MusicSlider.value = AudioManager.musicVolume;
            _SFXSlider.value = AudioManager.SFXVolume;
        }

        private void QuitSettings()
        {
            Destroy(gameObject);
        }
    }
}

