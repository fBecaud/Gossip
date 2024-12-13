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
    [SerializeField] private Color _UsualOutline;

    [Space(10)]
    private bool _EntityInRange;
    public bool isCorrupted;


    [SerializeField] private Material _EntityMat;
    [SerializeField] private EntityDetection _EntityDetection;

    private Action _Action;

    private void Awake()
    {
        _EntityDetection = GetComponentInChildren<EntityDetection>();
        _EntityMat = GetComponent<Renderer>().material;
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
        SetColor(_InRangeOutline);
        _Action = DoActionInRange;
    }

    private void DoActionInRange()
    {
        
    }

    public void SetModeSelected()
    {
        SetColor(_SelectedOutline);
        _Action = DoActionSelected;
    }

    private void DoActionSelected()
    {
        
    }

    public void SetModeCurrentEntity()
    {
        SetColor(_CurrentEntityOutline);
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
        SetColor(Color.grey);
        _Action = DoActionUsual;
    }

    private void DoActionUsual()
    {
    }

    public void SetColor(Color pColor)
    {
        _EntityMat.SetColor("_BaseColor", pColor);
    }
}