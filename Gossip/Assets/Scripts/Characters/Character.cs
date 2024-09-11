using CurvedPathGenerator;
using Gossip.Utilitaries.Managers;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Action _Action;
    [SerializeField] protected EntityDetection _EntityDetection;
    [SerializeField] protected PathFollower _PathFollower;
    [SerializeField] protected float _Speed;
    [SerializeField] protected bool _IsInRange;


    protected virtual void Awake()
    {
        _PathFollower = GetComponent<PathFollower>();
        _EntityDetection = GetComponentInChildren<EntityDetection>();
        SetModeVoid();
    }

    private void Start()
    {
        
    }

    protected virtual void Update()
    {
        _Action();
    }

    public virtual void SetModeVoid()
    {
        _IsInRange = false;
        _PathFollower.IsMove = false;
        _EntityDetection.enabled = false;
        _Action = DoActionVoid;
    }

    protected virtual void DoActionVoid()
    {
        //Keep empty;
    }

    public virtual void SetModeUsual()
    {
        _IsInRange = false;
        _EntityDetection.enabled = false;
    }

    public virtual void SetModeMove()
    {
        _PathFollower.IsMove = true;
        _Speed = _PathFollower.Speed;
        _Action = DoActionMove;
    }

    public virtual void SetModeInRange()
    {
        _IsInRange = true;
    }

    public virtual void SetModeCurrentEntity()
    {
        _EntityDetection.enabled = true;
    }

    protected virtual void DoActionMove()
    {
        _PathFollower.Speed = _Speed * TimeManager.instance.TimeScale;
    }

    public virtual void SetModeTravelCompleted()
    {
    }

    public bool IsInRange
    {
        get { return _IsInRange; }
        set { _IsInRange = value; }
    }
}
