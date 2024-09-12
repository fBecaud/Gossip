using CurvedPathGenerator;
using FMOD.Studio;
using FMODUnity;
using Gossip.Utilitaries.Managers;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    protected Action _Action;
    [SerializeField] protected EntityDetection _EntityDetection;
    [SerializeField] protected PathFollower _PathFollower;
    [SerializeField] private GameObject _FloorCircle;
    [SerializeField] protected float _Speed;
    [SerializeField] protected bool _IsInRange;

    [Header("Audio Instances")]
    [SerializeField] protected EventReference _TalkingSoundReference;
    [SerializeField] protected EventReference _WalkingSoundReference;
    internal EventInstance _TalkingSoundInstance;
    internal EventInstance _WalkingSoundInstance;

    // Frequency of the sounds in seconds
    [SerializeField] internal float _WalkingSoundFrequency = 0.5f;
    [SerializeField] internal float _TalkingSoundFrequency = 1.0f;

    internal float _WalkingSoundTimer = 0f;
    internal float _TalkingSoundTimer = 0f;

    [Header("Particules")]
    [SerializeField] private GameObject _PossessedEntityParticuleGameObject;
    [SerializeField] private Transform _PossessedEntityParticulePosition;

    [Header("Animation")]
    [SerializeField] private Animator _Animator;

    internal const string ANIM_BOOL_IS_WALKING = "IsWalking";

    protected virtual void Awake()
    {
        _PathFollower = GetComponent<PathFollower>();
        _EntityDetection = GetComponentInChildren<EntityDetection>();
        SetModeVoid();
        _FloorCircle.SetActive(false);
    }

    protected virtual void Start()
    {
        // Initialize the sound instances
        _WalkingSoundInstance = RuntimeManager.CreateInstance(_WalkingSoundReference);
        _TalkingSoundInstance = RuntimeManager.CreateInstance(_TalkingSoundReference);

        if (_Animator == null)
        {
            _Animator.GetComponent<Animator>();
        }
    }

    protected virtual void Update()
    {
        _Action();

        UpdateSoundPlayback(ref _WalkingSoundTimer, _WalkingSoundInstance, _WalkingSoundFrequency, _PathFollower.IsMove);

        UpdateAnimation(ANIM_BOOL_IS_WALKING, _PathFollower.IsMove);

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetModeVoid();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetModeCurrentEntity();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SetModeInRange();
        }
    }

    protected virtual void UpdateAnimation(string pBoolName, bool pBoolValue)
    {
        if (_Animator != null)
        {
            _Animator.SetBool(pBoolName, pBoolValue);
        }
    }

    internal void UpdateSoundPlayback(ref float timer, EventInstance soundInstance, float frequency, bool condition)
    {
        if (condition)
        {
            timer += Time.deltaTime;
            if (timer >= frequency)
            {
                PlaySound(soundInstance);
                timer = 0f; // Reset the timer
            }
        }
        else
        {
            StopSound(soundInstance);
        }
    }

    private void PlaySound(EventInstance soundInstance)
    {
        if (soundInstance.isValid())
        {
            soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            soundInstance.start();
        }
    }

    private void StopSound(EventInstance soundInstance)
    {
        if (soundInstance.isValid())
        {
            soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public virtual void SetModeVoid()
    {
        _IsInRange = false;
        _PathFollower.IsMove = false;
        _EntityDetection.enabled = false;
        _FloorCircle.SetActive(false);
        _Action = DoActionVoid;
        StopSound(_WalkingSoundInstance);
        StopSound(_TalkingSoundInstance);
        UpdateAnimation(ANIM_BOOL_IS_WALKING, false);
    }

    protected virtual void DoActionVoid()
    {
        // Keep empty
    }

    public virtual void SetModeUsual()
    {
        _IsInRange = false;
        _EntityDetection.enabled = false;
        _FloorCircle.SetActive(false);
        StopSound(_TalkingSoundInstance);
    }

    public virtual void SetModeMove()
    {
        _PathFollower.enabled = true;
        _PathFollower.IsMove = true;
        _Speed = _PathFollower.Speed;
        _Action = DoActionMove;
        UpdateAnimation(ANIM_BOOL_IS_WALKING, true);
    }

    public virtual void SetModeInRange()
    {
        _IsInRange = true;
        _FloorCircle.SetActive(false);
    }

    public virtual void SetModeCurrentEntity()
    {
        _EntityDetection.enabled = true;
        _FloorCircle.SetActive(true);
        PlayParticule(_PossessedEntityParticuleGameObject, _PossessedEntityParticulePosition.position);
    }

    protected virtual void DoActionMove()
    {
        _PathFollower.Speed = _Speed * TimeManager.instance.TimeScale;
    }

    public virtual void SetModeTravelCompleted()
    {
        StopSound(_WalkingSoundInstance);
    }

    private void OnDestroy()
    {
        if (_WalkingSoundInstance.isValid())
        {
            _WalkingSoundInstance.release();
        }
        if (_TalkingSoundInstance.isValid())
        {
            _TalkingSoundInstance.release();
        }
    }

    public bool IsInRange
    {
        get { return _IsInRange; }
        set { _IsInRange = value; }
    }

    internal virtual void PlayParticule(GameObject pParticule, Vector3 pPos)
    {
        pParticule.transform.position = pPos;
        pParticule.GetComponent<ParticleSystem>().Play();
    }
}
