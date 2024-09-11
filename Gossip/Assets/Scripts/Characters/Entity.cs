using UnityEngine;
using FMODUnity;
using System;
using UnityEngine.SceneManagement;

public class Entity : Character
{
    [Header("Audio")]
    [SerializeField] private EventReference _TalkingSound;
    [SerializeField] private EventReference _CorruptedTalkingSound;
    [SerializeField] private EventReference _WalkingSound;

    [Header("Colors")]
    [SerializeField] private Color _InRangeOutline;
    [SerializeField] private Color _SelectedOutline;
    [SerializeField] private Color _CurrentEntityOutline;

    [Space(10)]
    private bool _IsCorrupted;

    [SerializeField] private Outline _Outline;

    protected override void Awake()
    {
        base.Awake();
        _Outline = GetComponentInChildren<Outline>();
    }

    protected virtual void Start()
    {
        SetModeMove();
    }

    public void UpdateCorrupted()
    {
        if (IsCorrupted)
            return;
        IsCorrupted = true;
        ScoreManager.instance.IncreaseCount();
    }
    
    public override void SetModeInRange()
    {
        base.SetModeInRange();
        SetOutline(_InRangeOutline);
    }

    public void SetModeSelected()
    {
        SetOutline(_SelectedOutline);
    }

    public override void SetModeCurrentEntity()
    {
        UpdateCorrupted();
        base.SetModeCurrentEntity();
        SetOutline(_CurrentEntityOutline);
    }

    public override void SetModeUsual()
    {
        base.SetModeUsual();
        DisableOutline();
    }

    private void SetOutline(Color pColor)
    {
        _Outline.enabled = true;
        _Outline.OutlineColor = pColor;
    }

    private void DisableOutline()
    {
        _Outline.enabled = false;
    }

    public override void SetModeTravelCompleted()
    {
    }

    public bool IsCorrupted
    {
        get { return _IsCorrupted; }
        set { _IsCorrupted = value; }
    }
}