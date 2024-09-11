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

    private EventInstance _MusicInstance;
    private EventInstance _AmbianceInstance;
    [SerializeField] private StudioEventEmitter _MusicEmitter;
    [SerializeField] private StudioEventEmitter _AmbianceEmitter;

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
    }

    public void PlayOneShot(EventReference pSound, Vector3 pWorldPos)
    {
        RuntimeManager.PlayOneShot(pSound, pWorldPos);
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
    }

    public void ChangeMusicTrack(string pParameter, string pLabel)
    {
        print("changing music");
        SetParameterLabelName(_MusicInstance, pParameter, pLabel);
    }

    public void ChangeAmbiantTrack(string pParameter, string pLabel)
    {
        print("changing ambiance");
        SetParameterLabelName(_AmbianceInstance, pParameter, pLabel);
    }

}
