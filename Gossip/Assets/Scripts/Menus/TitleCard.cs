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

        [SerializeField] private Image _TransitionScreen;
        [SerializeField] private float _TransitionSpeed;

        private float _ElapsedTime;

        private const string GAME_SCENE = "Main";
        private const string CREDIT_SCENE = "Credit";

        void Start()
        {
            _PlayButton.onClick.AddListener(Play);
            _SettingsButton.onClick.AddListener(DisplaySettings);
            _CreditButton.onClick.AddListener(Credit);
            _QuitButton.onClick.AddListener(Quit);
        }

        private void Play()
        {
            _TransitionScreen.gameObject.SetActive(true);
            StartCoroutine(TransitionCoroutine());
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

        private IEnumerator TransitionCoroutine()
        {
            while (_TransitionScreen.color != Color.black)
            {
                _ElapsedTime += Time.deltaTime;
                _TransitionScreen.color = Color.Lerp(Color.clear, Color.black, _ElapsedTime * _TransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(GAME_SCENE);

            StopCoroutine(TransitionCoroutine());
        }
    }
}

