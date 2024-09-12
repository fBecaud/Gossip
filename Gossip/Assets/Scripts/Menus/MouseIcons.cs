using Gossip.Utilitaries.Managers;
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
    Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _State = State.Start;
        _Animator.SetTrigger("Start Tuto"); //Start tuto
        _State++;
    }

    private void OnEnable()
    {
       EventManager.instance.OnEntityChanged += CloseTutorial;
    }
    private void Ondisable()
    {
        EventManager.instance.OnEntityChanged -= CloseTutorial;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
    }
    void ChangeState()
    {
        if (_State == State.Rightclick)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _State++;
                _Animator.SetTrigger("Right Click"); //Rightclick
            }
        }
    }

    private void CloseTutorial()
    {
        if (_State == State.Leftclick)
        {
            _State++;
            _Animator.SetTrigger("Left Click"); //Leftclick
        }
    }
}