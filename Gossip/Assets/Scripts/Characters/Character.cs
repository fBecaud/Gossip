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
    [SerializeField] protected float _Speed;
    [SerializeField] protected bool _IsInRange;

    [Header("Audio Instances")]
    [SerializeField] protected EventReference _TalkingSoundReference;
    [SerializeField] protected EventReference _WalkingSoundReference;
    private EventInstance _TalkingSoundInstance;
    private EventInstance _WalkingSoundInstance;

    // Frequency of the sounds in seconds
    [SerializeField] private float _WalkingSoundFrequency = 0.5f; // Example: play sound every 0.5 seconds
    [SerializeField] private float _TalkingSoundFrequency = 1.0f; // Example: play sound every 1 second

    private float _walkingSoundTimer = 0f; // Timer to keep track of elapsed time for walking sound
    private float _talkingSoundTimer = 0f; // Timer to keep track of elapsed time for talking sound

    protected virtual void Awake()
    {
        _PathFollower = GetComponent<PathFollower>();
        _EntityDetection = GetComponentInChildren<EntityDetection>();
        SetModeVoid();
    }

    protected virtual void Start()
    {
        // Initialize the sound instances
        _WalkingSoundInstance = RuntimeManager.CreateInstance(_WalkingSoundReference);
        if (_WalkingSoundInstance.isValid())
        {
            Debug.Log($"Walking sound instance created. {gameObject.name}");
        }
        else
        {
            Debug.Log($"Walking sound instance is not valid. {gameObject.name}");
        }

        _TalkingSoundInstance = RuntimeManager.CreateInstance(_TalkingSoundReference);
        if (_TalkingSoundInstance.isValid())
        {
            Debug.Log($"Talking sound instance created. {gameObject.name}");
        }
        else
        {
            Debug.Log($"Talking sound instance is not valid. {gameObject.name}");
        }
    }

    protected virtual void Update()
    {
        _Action();

        // Update timers and play sounds if needed
        UpdateSoundPlayback(ref _walkingSoundTimer, _WalkingSoundInstance, _WalkingSoundFrequency, _PathFollower.IsMove);
        UpdateSoundPlayback(ref _talkingSoundTimer, _TalkingSoundInstance, _TalkingSoundFrequency, _EntityDetection.enabled);
    }

    private void UpdateSoundPlayback(ref float timer, EventInstance soundInstance, float frequency, bool condition)
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
            print("playing sound : " + soundInstance);
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
        _Action = DoActionVoid;
        StopSound(_WalkingSoundInstance);
        StopSound(_TalkingSoundInstance);
    }

    protected virtual void DoActionVoid()
    {
        // Keep empty
    }

    public virtual void SetModeUsual()
    {
        _IsInRange = false;
        _EntityDetection.enabled = false;
        StopSound(_WalkingSoundInstance);
        StopSound(_TalkingSoundInstance);
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
}
