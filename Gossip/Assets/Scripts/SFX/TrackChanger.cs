using Gossip.Utilitaries.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackChanger : MonoBehaviour
{
    [SerializeField] private LayerMask _DetectionZoneLayerMask;

    [SerializeField] private List<TrackAttributes> TracksList = new();

    private void OnTriggerEnter(Collider other)
    {
        if ((_DetectionZoneLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            return;
        }

        for (int i = 0; i < TracksList.Count; i++)
        {
            TrackAttributes trackAttributes = TracksList[i];

            if (!string.IsNullOrEmpty(trackAttributes.trackName))
            {
                Character character = other.GetComponentInChildren<Character>();
                if (character != null && character.gameObject == PlayerManager.instance.CurrentCharacter)
                {
                    if (trackAttributes.changeMusic)
                    {
                        AudioManager.instance.ChangeMusicTrack("MusicSwitch", trackAttributes.trackName);
                    }

                    if (trackAttributes.changeAmbiance)
                    {
                        AudioManager.instance.ChangeAmbiantTrack("MusicSwitch", trackAttributes.trackName);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Track name or ambiance name is empty.");
            }
        }
    }


    [Serializable]
    public class TrackAttributes
    {
        public string eventName;
        public string trackName;  // For music
        public bool changeMusic;  // Whether to change music
        public bool changeAmbiance;  // Whether to change ambiance
    }

}
