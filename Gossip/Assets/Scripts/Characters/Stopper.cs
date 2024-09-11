using FMODUnity;
using UnityEngine;

public class Stopper : Character
{
    [SerializeField] private bool _IsAware;
    [SerializeField] private EventReference _AwareSoundAlert;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void SetModeCurrentEntity()
    {
        base.SetModeCurrentEntity();
        _IsAware = true;
        AudioManager.instance.PlayOneShot(_AwareSoundAlert);
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