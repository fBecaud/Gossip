using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] private GameObject _PauseMenu;
    [SerializeField] private GameObject _SettingsPopupPrefab;

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
        _PauseMenu.SetActive(true);
        _GameManager.PauseGame();
    }

    public void ResumeGame()
    {
        _PauseMenu.SetActive(false);
        _GameManager.ResumeGame();
    }

    public void DisplaySettings()
    {
        Instantiate(_SettingsPopupPrefab, transform);
    }

    public void QuitGame()
    {
        _GameManager.QuitGame();
    }
}
