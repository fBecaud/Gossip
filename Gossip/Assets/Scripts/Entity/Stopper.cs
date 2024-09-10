using CurvedPathGenerator;
using FMODUnity;
using Gossip.Utilitaries.Managers;
using System;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    private Action _Action;

    private PathFollower _PathFollower;

    private float _PFSpeed;

    private bool _IsAware;

    public bool stopperInRange;

    private void Awake()
    {
        _PathFollower = GetComponent<PathFollower>();
        stopperInRange = false;
    }

    private void Start()
    {
        SetModeVoid();
    }

    private void Update()
    {
        _Action();
    }

    public void SetModeVoid()
    {
        _PathFollower.IsMove = false;
        _Action = DoActionVoid;
    }

    private void DoActionVoid()
    {

    }

    public void SetModeInRange()
    {
        stopperInRange = true;
    }

    public void SetModeUsual()
    {
        stopperInRange = false;
    }

    public void SetModeMoving()
    {
        _PathFollower.IsMove = true;
        _PFSpeed = _PathFollower.Speed;
        _Action = DoActionMoving;
    }

    private void DoActionMoving()
    {
        _PathFollower.Speed = _PFSpeed;// To be multiplied by tick system
    }

    public void SetModeTravelCompleted()
    {
        _PathFollower.IsMove = false;
        print("arrived at the victim");
    }

    private void DoActionTravelCompleted()
    {
        _Action = DoActionTravelCompleted;
    }

    public bool StopperInRange
    {
        get { return stopperInRange; }
        set { stopperInRange = value; }
    }

    public bool IsAware
    {
        get { return _IsAware; }
        set { _IsAware = value; }
    }
}
