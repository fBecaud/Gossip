using UnityEngine;

namespace Gossip.Utilitaries.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _CurrentEntity;

        [SerializeField] private LayerMask entityLayerMask; // Layer mask to filter entity colliders
        [SerializeField] private LayerMask ignoreLayerMask; // Layer mask to ignore sphere colliders

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _CurrentEntity != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 30f, entityLayerMask))
                {
                    if ((ignoreLayerMask & (1 << hit.transform.gameObject.layer)) == 0)
                    {
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Entitée"))
                        {
                            _CurrentEntity = hit.transform.gameObject;
                            EventManager.instance.EntityChanged(_CurrentEntity);
                        }
                    }
                }
            }
        }

        public GameObject CurrentEntity
        {
            get { return _CurrentEntity; }
            set { _CurrentEntity = value; }
        }
    }
}
