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

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    public void PlayOneShot(EventReference pSound, Vector3 pWorldPos)
    {
        RuntimeManager.PlayOneShot(pSound, pWorldPos);
    }

    public void SetParameterLabelName(string eventName, string parameterName, string label)
    {
        // Find the event instance by name (if you are not storing it like _currentMusicInstance)
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventName);

        // Set the parameter using its name and the desired label
        eventInstance.setParameterByNameWithLabel(parameterName, label);

        // Start or update the event if needed
        eventInstance.start();
        eventInstance.release();  // To ensure it gets cleaned up
    }

    private void ChangeTrack(string pTrackName)
    {
        SetParameterLabelName("music", "MusicSwitch", pTrackName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeTrack("track1");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeTrack("track2");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeTrack("track3");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeTrack("track4");
        }
    }
}
