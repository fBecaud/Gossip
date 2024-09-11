using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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

    private EventInstance _CurrentMusicInstance;
    private EventInstance _ClickUIInstance;
    [SerializeField] private StudioEventEmitter _MusicEmitter;

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
        _CurrentMusicInstance = _MusicEmitter.EventInstance;
        _ClickUIInstance = RuntimeManager.CreateInstance(_ClickUI); 
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

    public void SetParameterLabelName(string eventName, string parameterName, string label)
    {
        _CurrentMusicInstance.setParameterByNameWithLabel(parameterName, label);
    }


    public void ChangeMusicTrack(string pTrackName)
    {
        SetParameterLabelName(_Music.ToString(), "MusicSwitch", pTrackName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeMusicTrack("track1");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeMusicTrack("track2");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeMusicTrack("track3");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeMusicTrack("track4");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayOneShot(_ClickUIInstance);
        }
    }
}
