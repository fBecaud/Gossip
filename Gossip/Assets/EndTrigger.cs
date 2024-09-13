using Gossip.Utilitaries.Managers;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _DetectionZoneLayerMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((_DetectionZoneLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            return;
        }

        Character character = other.GetComponentInChildren<Character>();

        if (GameManager.instance.StopperActivated) //Good Ending
        {
            PlayerManager.instance.CanSwap = false;
        }
    }
}
