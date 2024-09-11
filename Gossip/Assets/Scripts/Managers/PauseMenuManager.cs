using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] GameObject _PauseMenu;
    [SerializeField] GameObject _Settings;

    [SerializeField] GameManager _GameManager;

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
        PopInMenu();
        _GameManager.PauseGame();
    }
    public void PopInMenu()
    {
        _PauseMenu.SetActive(true);
        _Settings.SetActive(false);
    }

    public void ResumeGame()
    {
       PopOutMenu();
      _GameManager.ResumeGame();
    }
    // Update is called once per frame
    public void PopOutMenu()
    {
        _PauseMenu.SetActive(false);
        _Settings.SetActive(true);
    }
    public void QuitGame()
    {
        _GameManager.QuitGame();
    }
}
