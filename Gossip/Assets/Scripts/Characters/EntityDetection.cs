using UnityEngine;

public class EntityDetection : MonoBehaviour
{
    private void Awake()
    {
        transform.parent.gameObject.GetComponentInChildren<Character>().SetModeUsual();
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
        if (enabled) // Verification to prevent running when the script is disabled
        {
            other.GetComponent<Character>().SetModeInRange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enabled) // Verification to prevent running when the script is disabled
        {
            other.GetComponent<Character>().SetModeUsual();
        }
    }

    private void CleanEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<CapsuleCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponentInChildren<Entity>().SetModeUsual();
        }
    }

    private void ScanForEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<CapsuleCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != transform.parent.gameObject)
            {
                hitCollider.gameObject.GetComponentInChildren<Entity>().SetModeInRange();
            }
        }
    }
}
