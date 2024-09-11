using UnityEngine;
using FMODUnity;
using System;

public class Entity : MonoBehaviour
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
    private bool _EntityInRange;
    public bool isCorrupted;


    [SerializeField] private EntityDetection _EntityDetection;
    [SerializeField] private Outline _Outline;

    private Action _Action;

    private void Awake()
    {
        _EntityDetection = GetComponentInChildren<EntityDetection>();
        _Outline = GetComponentInChildren<Outline>();
        SetModeVoid();
    }

    public bool EntityInRange
    {
        get { return _EntityInRange; }
        set { _EntityInRange = value; }
    }

    public void UpdateCorrupted()
    {
        isCorrupted = true;

    }

    public void SetModeVoid()
    {
        _Action = DoActionVoid;
    }

    private void DoActionVoid()
    {
        //Keep empty;
    }
    
    public void SetModeInRange()
    {
        _EntityInRange = true;
        SetOutline(_InRangeOutline);
        _Action = DoActionInRange;
    }

    private void DoActionInRange()
    {
        
    }

    public void SetModeSelected()
    {
        SetOutline(_SelectedOutline);
        _Action = DoActionSelected;
    }

    private void DoActionSelected()
    {
        
    }

    public void SetModeCurrentEntity()
    {
        SetOutline(_CurrentEntityOutline);
        _EntityDetection.enabled = true;
        _Action = DoActionCurrentEntity;
    }

    private void DoActionCurrentEntity()
    {
        
    }

    public void SetModeUsual()
    {
        _EntityInRange = false;
        _EntityDetection.enabled = false;
        DisableOutline();
        _Action = DoActionUsual;
    }

    private void DoActionUsual()
    {
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
}