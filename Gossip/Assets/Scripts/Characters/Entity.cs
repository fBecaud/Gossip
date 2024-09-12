using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;
using Gossip.Utilitaries.Managers;

public class Entity : Character
{
    [Header("Audio")]
    [SerializeField] private EventReference _CorruptedTalkingSoundReference;
    internal EventInstance _CorruptedTalkingSoundInstance;

    [Header("Colors")]
    [SerializeField] private Color _InRangeOutline;
    [SerializeField] private Color _SelectedOutline;
    [SerializeField] private Color _CurrentEntityOutline;

    [Space(10)]
    private bool _IsCorrupted;

    [SerializeField] private Outline _Outline;

    [SerializeField] private bool _MoveAtStart;

    [SerializeField] private MaterialUpdater _MaterialUpdater;

    [SerializeField] private GameObject _AutomatedCharacterSwap;

    protected override void Awake()
    {
        base.Awake();
        _Outline = GetComponentInChildren<Outline>();
        _MaterialUpdater = GetComponentInChildren<MaterialUpdater>();
    }

    protected override void Start()
    {
        base.Start();
        _CorruptedTalkingSoundInstance = RuntimeManager.CreateInstance(_CorruptedTalkingSoundReference);
        if (_MoveAtStart)
        {
            SetModeMove();
        }
    }

    protected override void Update()
    {
        base.Update();
        UpdateSoundPlayback(ref _TalkingSoundTimer, _TalkingSoundInstance, _TalkingSoundFrequency, _EntityDetection.enabled && IsCorrupted);
        UpdateSoundPlayback(ref _TalkingSoundTimer, _CorruptedTalkingSoundInstance, _TalkingSoundFrequency, _EntityDetection.enabled && !IsCorrupted);
    }

    public void UpdateCorrupted()
    {
        _IsCorrupted = true;
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
        base.SetModeCurrentEntity();
        if (_MaterialUpdater)
        {
            _MaterialUpdater.UpdateMaterial();
        }
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

    public void SwapToTarget()
    {
        PlayerManager.instance.ChangeEntityAutomated(_AutomatedCharacterSwap);
    }

    public bool IsCorrupted
    {
        get { return _IsCorrupted; }
        set { _IsCorrupted = value; }
    }
}