using FMODUnity;
using UnityEngine;

public class Stopper : Character
{
    [SerializeField] private bool _IsAware;
    [SerializeField] private EventReference _AwareSoundAlert;

    [Header("Stopper Particules")]
    [SerializeField] private GameObject _AngryParticuleGameObject;
    [SerializeField] private Transform _AngryParticulePosition;
    [SerializeField] private GameObject _PoContentParticuleGameObject;
    [SerializeField] private Transform _PoContentParticulePosition;

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
        PlayParticule(_AngryParticuleGameObject, _AngryParticulePosition.position);
        _IsAware = true;
        AudioManager.instance.PlayOneShot(_AwareSoundAlert);
        _RandomChildActivator.ActivateRandomChild();
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
        PlayParticule(_PoContentParticuleGameObject, _PoContentParticulePosition.position);
        _EntityDetection.enabled = false;
    }

    public override void SetModeTravelCompleted()
    {
        _PathFollower.IsMove = false;
        SetModeVoid();
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