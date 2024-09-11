using Gossip.Utilitaries.Managers;

public class Transitioner : Entity
{
    private bool _HasBeenUsed;

    public override void SetModeCurrentEntity()
    {
        base.SetModeCurrentEntity();
        if (!_HasBeenUsed)
        {
            _HasBeenUsed = true;
            SetModeMove();
        }
    }

    protected override void Start()
    {

    }

    public override void SetModeMove()
    {
        base.SetModeMove();
        PlayerManager.instance.IsOnTransitioner = true;
    }

    public override void SetModeTravelCompleted()
    {
        PlayerManager.instance.IsOnTransitioner = false;
        _PathFollower.IsMove = false;
        print("changed room");
    }
}
