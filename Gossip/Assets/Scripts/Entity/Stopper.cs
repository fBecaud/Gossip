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

    [SerializeField] private bool _IsAware;

    public bool stopperInRange;

    [SerializeField] private EntityDetection _EntityDetection;

    private void Awake()
    {
        _PathFollower = GetComponent<PathFollower>();
        _EntityDetection = GetComponentInChildren<EntityDetection>();
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
        _EntityDetection.enabled = false;
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
        _EntityDetection.enabled = false;
    }

    public void SetModeCurrentEntity()
    {
        _IsAware = true;
        _EntityDetection.enabled = true;
    }

    public void SetModeMoving()
    {
        _EntityDetection.enabled = false;
        _PathFollower.IsMove = true;
        _PFSpeed = _PathFollower.Speed;
        _Action = DoActionMoving;
    }

    private void DoActionMoving()
    {
        _PathFollower.Speed = _PFSpeed * TimeManager.instance.TimeScale;
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
