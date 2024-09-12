using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
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

    void Start()
    {
        ScoreManager.instance.OnScoreUpdate += SetScore;
    }

    void Update()
    {
        
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
