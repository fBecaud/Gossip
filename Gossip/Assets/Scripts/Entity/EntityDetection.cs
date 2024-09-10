using Gossip.Utilitaries.Managers;
using UnityEngine;

public class EntityDetection : MonoBehaviour
{
    private void Awake()
    {
        transform.parent.gameObject.GetComponent<Entity>().SetModeUsual();
    }

    private void OnEnable()
    {
        ScanForEntities();
    }

    private void OnDisable()
    {
        CleanEntities();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled) //Added because Unity still runs OnTrigger functions even when the script is disabled. Acts as a verification.
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Entitée"))
            {
                other.GetComponent<Entity>().SetModeInRange();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Stopper"))
            {
                other.GetComponent<Stopper>().SetModeInRange();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enabled)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Entitée"))
            {
                other.GetComponent<Entity>().SetModeUsual();
            }
        }
    }

    private void CleanEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Entity>().SetModeUsual();
        }
    }

    private void ScanForEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != transform.parent.gameObject)
            {
                hitCollider.gameObject.GetComponent<Entity>().SetModeInRange();
            }
        }
    }
}
