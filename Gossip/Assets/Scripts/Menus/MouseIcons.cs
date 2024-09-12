using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIcons : MonoBehaviour
{
    enum State
    {
        Start,
        Rightclick,
        Leftclick,
        End
    };
    State _State;

    [SerializeField] GameObject _RightClickIcon;
    [SerializeField] GameObject _LeftClickIcon;

    // Start is called before the first frame update
    void Start()
    {
        _State = State.Start;
        PopUpIcon(_RightClickIcon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
        }
    }
    void ChangeState()
    {
        _State++;
        if (_State == State.Rightclick)
        {
            PopOutIcon(_RightClickIcon);
            PopUpIcon(_LeftClickIcon);
        }
        else if (_State == State.Leftclick)
        {
            PopOutIcon(_LeftClickIcon);
        }
        else if (_State == State.End)
        {
            Destroy(gameObject);
        }
    }
    void PopUpIcon(GameObject icon)
    {
        icon.SetActive(true);
        icon.GetComponent<Animation>().Play();
    }
    void PopOutIcon(GameObject icon)
    {
        icon.GetComponent<Animation>().Play();
        icon.SetActive(false);
    }
}
