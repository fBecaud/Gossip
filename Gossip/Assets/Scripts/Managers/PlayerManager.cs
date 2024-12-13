using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Gossip.Utilitaries.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _CurrentEntity;
        [SerializeField] private GameObject _SelectedEntity;

        [SerializeField] private LayerMask _EntityLayerMask; // Layer mask to filter entity colliders
        [SerializeField] private LayerMask _IgnoreLayerMask; // Layer mask to ignore sphere colliders

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _SelectedEntity != null)
            {
                FindNewEntity();
            }
            ScanEntities();
        }

        private void FindNewEntity()
        {
            _CurrentEntity.GetComponent<Entity>().SetModeUsual();
            _CurrentEntity = _SelectedEntity; //Changing entity
            _SelectedEntity = null;
            _CurrentEntity.GetComponent<Entity>().SetModeCurrentEntity();
            EventManager.instance.EntityChanged(_CurrentEntity);
        }

        private void ScanEntities()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, _EntityLayerMask))
            {
                if (hit.transform.gameObject == _CurrentEntity)
                {
                    return;
                }
                if ((_IgnoreLayerMask & (1 << hit.transform.gameObject.layer)) == 0 && hit.transform.gameObject.GetComponent<Entity>().EntityInRange)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Entitée"))
                    {
                        _SelectedEntity = hit.transform.gameObject;
                        _SelectedEntity.GetComponent<Entity>().SetModeSelected();
                    }
                }
                else if (_SelectedEntity != null)
                {
                    _SelectedEntity.GetComponent<Entity>().SetModeInRange();
                    _SelectedEntity = null;
                }
            }
            else //if the raycast hits no entity
            {
                if (_SelectedEntity != null)
                {
                    _SelectedEntity.GetComponent<Entity>().SetModeInRange();
                    _SelectedEntity = null; 
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
