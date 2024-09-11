using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gossip.Menus
{
    public class TitleCard : MonoBehaviour
    {
        [SerializeField] private Button _PlayButton;
        [SerializeField] private Button _SettingsButton;
        [SerializeField] private Button _CreditButton;
        [SerializeField] private Button _QuitButton;

        private const string GAME_SCENE = "Main";
        private const string CREDIT_SCENE = "Credit";

        void Start()
        {

        }

        private void Play()
        {
            SceneManager.LoadScene(GAME_SCENE);
        }

        private void DisplaySettings()
        {

        }

        private void Credit()
        {
            SceneManager.LoadScene(CREDIT_SCENE);
        }

        private void Quit()
        {
            Application.Quit();
        } 
    }
}

