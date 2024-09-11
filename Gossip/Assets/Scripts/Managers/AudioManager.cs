using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio")]
    [SerializeField] EventReference _RingbellSound;
    [SerializeField] EventReference _Music;
    [SerializeField] EventReference _AmbiantSchoolSound;

    private EventInstance _CurrentMusicInstance;
    private StudioEventEmitter _musicEmitter; // Reference to the emitter in your scene

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
        // Find the FMOD Studio Event Emitter attached to the GameObject that plays your music
        _musicEmitter = FindObjectOfType<StudioEventEmitter>();

        if (_musicEmitter != null && _musicEmitter.EventInstance.isValid())
        {
            _CurrentMusicInstance = _musicEmitter.EventInstance; // Use the existing event instance from the emitter
        }
        else
        {
            Debug.LogError("FMOD Studio Event Emitter for music not found or not valid!");
        }
    }

    public void PlayOneShot(EventReference pSound, Vector3 pWorldPos)
    {
        RuntimeManager.PlayOneShot(pSound, pWorldPos);
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
    }
}
