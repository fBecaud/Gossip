using Gossip.Menus;
using Gossip.Utilitaries.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _EndScreen;
    public bool _isPaused { get; private set; } = false;

    private const string TITLE_CARD_SCENE = "TitleCard";

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                PauseMenuManager.instance.PauseGame();
            }
            else
            {
                PauseMenuManager.instance.ResumeGame();
            }
        }
    }

    public void SetEnding(bool pIsGoodEnding)
    {
        _EndScreen.SetActive(true);
        _EndScreen.GetComponent<EndScreen>().isGoodEnding = pIsGoodEnding;
    }

    public void PauseGame()
    {
        _isPaused = true;
        //TimeManager ref here
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        _isPaused = false;
        //TimeManager ref here
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(TITLE_CARD_SCENE);
    }
}
