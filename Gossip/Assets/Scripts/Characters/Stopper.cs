using CurvedPathGenerator;
using FMODUnity;
using Gossip.Utilitaries.Managers;
using System;
using UnityEngine;

public class Stopper : Character
{
    [SerializeField] private bool _IsAware;

    private void Start()
    {
        SetModeVoid();
    }

    public override void SetModeCurrentEntity()
    {
        base.SetModeCurrentEntity();
        _IsAware = true;
    }

    public override void SetModeUsual()
    {
        base.SetModeUsual();
        if (IsAware)
        {
            SetModeMove();
        }
    }

    public override void SetModeMove()
    {
        base.SetModeMove();
        _EntityDetection.enabled = false;
    }

    public override void SetModeTravelCompleted()
    {
        _PathFollower.IsMove = false;
        print("arrived at the victim");
    }

    private void DoActionTravelCompleted()
    {
        _Action = DoActionTravelCompleted;
    }

    public bool IsAware
    {
        get { return _IsAware; }
        set { _IsAware = value; }
    }
}
