using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] private GameObject _PauseMenu;
    [SerializeField] private GameObject _SettingsPopupPrefab;

    public bool isPaused;

    private GameManager _GameManager;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        _GameManager = GameManager.instance;
    }
    public void PauseGame()
    {
        AudioManager.instance.PlayClickSound();
        isPaused = true;

        _PauseMenu.SetActive(true);
        _GameManager.PauseGame();
    }

    public void ResumeGame()
    {
        AudioManager.instance.PlayClickSound();
        isPaused = false;

        _PauseMenu.SetActive(false);
        _GameManager.ResumeGame();
    }

    public void DisplaySettings()
    {
        AudioManager.instance.PlayClickSound();

        Instantiate(_SettingsPopupPrefab, transform);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayClickSound();

        _GameManager.QuitGame();
    }
}
