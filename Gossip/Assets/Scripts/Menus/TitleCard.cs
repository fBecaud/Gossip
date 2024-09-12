using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gossip.Menus
{
    public class TitleCard : Menu
    {
        [SerializeField] private Button _PlayButton;
        [SerializeField] private Button _SettingsButton;
        [SerializeField] private Button _CreditButton;
        [SerializeField] private Button _QuitButton;

        [SerializeField] private GameObject _SettingsPopupPrefab;

        protected override void Init()
        {
            _PlayButton.onClick.AddListener(Play);
            _SettingsButton.onClick.AddListener(DisplaySettings);
            _CreditButton.onClick.AddListener(Credit);
            _QuitButton.onClick.AddListener(Quit);

            base.Init();
        }

        private void Play()
        {
            TransitionIn(GAME_SCENE);
        }

        private void DisplaySettings()
        {
            Instantiate(_SettingsPopupPrefab, transform);
        }

        private void Credit()
        {
            TransitionIn(CREDIT_SCENE);
        }

        private void Quit()
        {
            Application.Quit();
        } 
    }
}

