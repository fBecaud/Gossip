using Gossip.Utilitaries.Managers;
using UnityEngine;

public class TrackChanger : MonoBehaviour
{
    [SerializeField] private string _TrackName; 
    [SerializeField] private LayerMask _DetectionZoneLayerMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((_DetectionZoneLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            return;
        }

        if (!string.IsNullOrEmpty(_TrackName))
        {
            Character character = other.GetComponentInChildren<Character>();
            if (character != null && character.gameObject == PlayerManager.instance.CurrentCharacter)
            {
                AudioManager.instance.ChangeMusicTrack(_TrackName);
            }
        }
        else
        {
            Debug.LogWarning("Track name is empty.");
        }
    }
}
