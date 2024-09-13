using Unity.Burst.CompilerServices;
using UnityEngine;

public class EntityDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _SelectedLayerMask;

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
        if (enabled && (other.gameObject.layer == LayerMask.NameToLayer("Entitée") || other.gameObject.layer == LayerMask.NameToLayer("Stopper") 
            || other.gameObject.layer == LayerMask.NameToLayer("Transitioner")))// Verification to prevent running when the script is disabled
        {
            print($"trigger detected with {other.gameObject.name}.");    
            other.GetComponent<Character>().SetModeInRange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enabled && (other.gameObject.layer == LayerMask.NameToLayer("Entitée") || other.gameObject.layer == LayerMask.NameToLayer("Stopper")
            || other.gameObject.layer == LayerMask.NameToLayer("Transitioner"))) // Verification to prevent running when the script is disabled
        {
            print($"trigger undetected with {other.gameObject.name}.");
            other.GetComponent<Character>().SetModeUsual();
        }
    }

    private void CleanEntities()
    {
        // Find all colliders within the radius and check against _SelectableLayers
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<CapsuleCollider>().radius, _SelectedLayerMask);

        foreach (var hitCollider in hitColliders)
        {
            Entity entity = hitCollider.GetComponentInChildren<Entity>();
            if (entity != null)
            {
                entity.SetModeUsual();
            }

            Stopper stopper = hitCollider.GetComponentInChildren<Stopper>();
            if (stopper != null)
            {
                stopper.SetModeUsual();
            }
        }
    }

    private void ScanForEntities()
    {
        // Find all colliders within the radius and check against _SelectableLayers
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponentInChildren<CapsuleCollider>().radius, _SelectedLayerMask);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != transform.parent.gameObject)
            {
                Entity entity = hitCollider.GetComponentInChildren<Entity>();
                if (entity != null)
                {
                    entity.SetModeInRange();
                }

                Stopper stopper = hitCollider.GetComponentInChildren<Stopper>();
                if (stopper != null)
                {
                    stopper.SetModeInRange();
                }
            }
        }
    }

    private bool IsSelectedLayer(int layer)
    {
        return (_SelectedLayerMask.value & (1 << layer)) != 0;
    }
}
