using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gossip.Menus
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Button _QuitButton;
        [SerializeField] private Text _Message;
        [SerializeField] private GameObject _GoodMessage;

        [SerializeField] private bool _GoodEnding;

        private const string MESSAGE_START = "Il suffit de ";
        private const string MESSAGE_END = " personnes pour ruiner la vie d'une autre.";
        private const string TITLE_CARD_SCENE = "TitleCard";

        void Start()
        {
            if (ScoreManager.instance != null) _Message.text = MESSAGE_START + ScoreManager.instance._SpreadCount + MESSAGE_END;

            if (_GoodEnding) _GoodMessage.SetActive(true);
            else _GoodMessage.SetActive(false);

            _QuitButton.enabled = false;
            _QuitButton.onClick.AddListener(Quit);
        }

        private void Quit()
        {
            AudioManager.instance.PlayClickSound();

            SceneManager.LoadScene(TITLE_CARD_SCENE);
        }

        private void EnableQuit()
        {
            _QuitButton.enabled = true;
        }
    }
}

