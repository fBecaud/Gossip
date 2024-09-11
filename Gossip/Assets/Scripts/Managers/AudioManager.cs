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

    private Bus _MasterBus;
    private Bus _MusicBus;
    private Bus _SFXBus;

    public static float masterVolume;
    public static float musicVolume = 1;
    public static float SFXVolume = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        _MasterBus = RuntimeManager.GetBus("bus:/Master Bus");
        _MusicBus = RuntimeManager.GetBus("bus:/Music Bus");
        _SFXBus = RuntimeManager.GetBus("bus:/SFX Bus");
    }

    public void UpdateVolume()
    {
        _MasterBus.setVolume(masterVolume);
        _SFXBus.setVolume(SFXVolume);
        _MusicBus.setVolume(musicVolume);
    }

    public void PlayOneShot(EventReference pSound, Vector3 pWorldPos)
    {
        RuntimeManager.PlayOneShot(pSound, pWorldPos);
    }
}
