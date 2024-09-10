using System;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    private bool _IsMoving;

    private Action _Action;

    private void Start()
    {
        SetModeVoid();
    }

    public void SetModeVoid()
    {
        _Action = DoActionVoid;
    }

    private void DoActionVoid()
    {

    }

    public void SetModeMoving()
    {
    }

    private void DoActionMoving()
    {

    }
}
