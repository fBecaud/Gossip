using Gossip.Utilitaries.Managers;
using UnityEngine;

public class EntityDetection : MonoBehaviour
{
    private void Awake()
    {
        if (CameraManager.instance.target == transform.parent.gameObject)
        {
            enabled = true;
            SetColor(transform.parent.gameObject, Color.green);

        }
        else
        {
            enabled = false;
        }
    }

    private void OnEnable()
    {
        EventManager.instance.OnEntityChangedGameObject += CleanEntitiesAndSetNew;
        ScanForEntities();
    }

    private void OnDisable()
    {
        CleanEntities();
        EventManager.instance.OnEntityChangedGameObject -= CleanEntitiesAndSetNew;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled) //Added because Unity still runs OnTrigger functions even when the script is disabled. Acts as a verification.
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Entitée"))
            {
                SetColor(other.gameObject, Color.white);
                other.GetComponent<Entity>().entityInRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enabled)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Entitée"))
            {
                SetColor(other.gameObject, Color.grey);
                other.GetComponent<Entity>().entityInRange = false;
            }
        }
    }

    private void CleanEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            SetColor(hitCollider.gameObject, Color.grey);
            hitCollider.GetComponent<Entity>().entityInRange = false;
        }
    }

    private void CleanEntitiesAndSetNew(GameObject pNewEntity)
    {
        SetColor(pNewEntity, Color.green);
    }

    private void ScanForEntities()
    {
        int lEntityLayerMask = 1 << LayerMask.NameToLayer("Entitée");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, lEntityLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != transform.parent.gameObject)
            {
                SetColor(hitCollider.gameObject, Color.white);
                hitCollider.GetComponent<Entity>().entityInRange = true;
            }
        }
    }

    private void SetColor(GameObject pGameObject, Color pColor)
    {
        Material lCubeMat = pGameObject.GetComponent<Renderer>().material; //To be replaced with the shader
        lCubeMat.SetColor("_BaseColor", pColor);
        print("Setting " + pGameObject + "color  to " + pColor);
     }
}
