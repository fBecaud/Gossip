using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public uint _SpreadCount { get; private set; }

    public event Action OnScoreUpdate;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        _SpreadCount = 0;
    }

    public void IncreaseCount()
    {
        _SpreadCount++;
        OnScoreUpdate?.Invoke();
    }
}
