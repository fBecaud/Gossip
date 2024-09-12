using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gossip.Menus
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Slider _MasterSlider;

        [SerializeField] private Button _QuitButton;

        void Start()
        {
            _MasterSlider.onValueChanged.AddListener(delegate { SetVolume(_MasterSlider); });

            _QuitButton.onClick.AddListener(QuitSettings);

            SetSliders();
        }

        private void SetVolume(Slider pSlider)
        {
            AudioManager.masterVolume = pSlider.value;
            AudioManager.instance.UpdateVolume();
        }

        private void SetSliders()
        {
            _MasterSlider.value = AudioManager.masterVolume;
        }

        private void QuitSettings()
        {
            AudioManager.instance.PlayClickSound();

            Destroy(gameObject);
        }
    }
}

