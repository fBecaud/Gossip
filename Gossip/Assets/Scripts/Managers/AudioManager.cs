using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Gossip.Utilitaries.Managers;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("UI Audio")]
    [SerializeField] private EventReference _ClickUI;

    [Space(10)]
    [Header("Ambiant Audio")]
    [SerializeField] private EventReference _RingbellSound;
    [SerializeField] private EventReference _Music;
    [SerializeField] private EventReference _CharacterMusic;
    [SerializeField] private EventReference _AmbiantSchoolSound;
    [SerializeField] private EventReference _TransitionSound;

    private EventInstance _MusicInstance;
    private EventInstance _AmbianceInstance;
    private EventInstance _ClickUIInstance;
    private EventInstance _TransitionInstance;
    [SerializeField] private StudioEventEmitter _MusicEmitter;
    [SerializeField] private StudioEventEmitter _AmbianceEmitter;

    private Bus _MasterBus;

    public static float masterVolume;
    public static float musicVolume = 1;
    public static float SFXVolume = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        _MusicInstance = _MusicEmitter.EventInstance;
        _AmbianceInstance = _AmbianceEmitter.EventInstance;

        _MasterBus = RuntimeManager.GetBus("bus:/");
    }

    public void UpdateVolume()
    {
        _MasterBus.setVolume(masterVolume);
    }

    public void PlayOneShot(EventReference pSound, Vector3 pWorldPos)
    {
        RuntimeManager.PlayOneShot(pSound, pWorldPos);
    }

    public void PlayOneShot(EventReference pSound)
    {
        RuntimeManager.PlayOneShot(pSound);
    }

    public void PlayOneShot(EventInstance pSoundInstance)
    {
        pSoundInstance.start();
    }

    public void SetParameterLabelName(EventInstance pMusicInstance, string parameterName, string label)
    {
        pMusicInstance.setParameterByNameWithLabel(parameterName, label);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetParameterLabelName(_MusicInstance, "MusicSwitch", "track1");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SetParameterLabelName(_MusicInstance, "MusicSwitch", "track2");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SetParameterLabelName(_MusicInstance, "MusicSwitch", "track3");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SetParameterLabelName(_MusicInstance, "MusicSwitch", "track4");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetParameterLabelName(_AmbianceInstance, "MusicSwitch", "track3");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            SetParameterLabelName(_AmbianceInstance, "MusicSwitch", "track4");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            PlayClickSound();
        }
    }

    public void ChangeMusicTrack(string pParameter, string pLabel)
    {
        SetParameterLabelName(_MusicInstance, pParameter, pLabel);
    }

    public void ChangeAmbiantTrack(string pParameter, string pLabel)
    {
        SetParameterLabelName(_AmbianceInstance, pParameter, pLabel);
    }

    public void PlayClickSound()
    {
        PlayOneShot(_ClickUI);
    }

    public void PlayTransitionSound()
    {
        PlayOneShot(_TransitionSound);
    }

    public void PlayRingbellSounf()
    {
        PlayOneShot(_RingbellSound);
    }
}
