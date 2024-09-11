using Gossip.Utilitaries.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _isPaused { get; private set; } = false;
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
                PauseMenuManager.instance.PopInMenu();
                PauseGame();
            }
            else
            {
                PauseMenuManager.instance.PopOutMenu();
                ResumeGame();
            }
        }
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
        Application.Quit();
    }
}
