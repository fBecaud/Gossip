using Gossip.Menus;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : Menu
{
    [SerializeField] private TextMeshProUGUI _ScoreDisplay;
    [SerializeField] private Animator _ScoreAnim;

    private const string ADD_SCORE = "AddScore";

    public static HudManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    protected override void Init()
    {
        ScoreManager.instance.OnScoreUpdate += SetScore;

        base.Init();
    }

    private void SetScore()
    {
        _ScoreAnim.SetTrigger(ADD_SCORE);
        _ScoreDisplay.text = ScoreManager.instance._SpreadCount.ToString();
    }

    private void OnDestroy()
    {
        ScoreManager.instance.OnScoreUpdate -= SetScore;
    }
}
