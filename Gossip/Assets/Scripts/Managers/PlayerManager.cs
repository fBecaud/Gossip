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

        private int _CombinedLayerMask;
        [SerializeField] private bool _CanSwape = true;

        private void Start()
        {
            _CombinedLayerMask = LayerMask.GetMask("Entitée", "Stopper");
            
        }
        private void Update()
        {
            if (_CanSwape)
            {
                if (Input.GetMouseButtonDown(0) && _SelectedEntity != null)
                {
                    FindNewEntity();
                }
                ScanEntities();
            }
        }

        private void OnEnable()
        {
            EventManager.instance.OnTimeFreezeStarted += DisableSwapping;
            EventManager.instance.OnTimeFreezeEnded += EnableSwapping;
        }

        private void OnDisable()
        {
            EventManager.instance.OnTimeFreezeStarted -= DisableSwapping;
            EventManager.instance.OnTimeFreezeEnded -= EnableSwapping;
        }

        private void FindNewEntity()
        {
            TimeManager.instance.TempFreezeTime();
            if (_CurrentEntity.layer == LayerMask.NameToLayer("Entitée"))
            {
                _CurrentEntity.GetComponent<Entity>().SetModeUsual();
            }
            else if (_CurrentEntity.layer == LayerMask.NameToLayer("Stopper"))
            {
                _CurrentEntity.GetComponent<Stopper>().SetModeMoving();
            }
            _CurrentEntity = _SelectedEntity; //Changing entity
            _SelectedEntity = null;
            if (_CurrentEntity.layer == LayerMask.NameToLayer("Entitée"))
            {
                _CurrentEntity.GetComponent<Entity>().SetModeCurrentEntity();
            }
            else if (_CurrentEntity.layer == LayerMask.NameToLayer("Stopper"))
            {
                _CurrentEntity.GetComponent<Stopper>().SetModeCurrentEntity();
            }
            EventManager.instance.EntityChanged(_CurrentEntity);
        }

        private void ScanEntities()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 100f, _CombinedLayerMask))
            {
                if (hit.transform.gameObject == _CurrentEntity)
                {
                    return;
                }

                if ((_IgnoreLayerMask & (1 << hit.transform.gameObject.layer)) == 0 && hit.transform.gameObject.GetComponent<Entity>() != null &&
                    hit.transform.gameObject.GetComponent<Entity>().EntityInRange)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Entitée"))
                    {
                        _SelectedEntity = hit.transform.gameObject;
                        _SelectedEntity.GetComponent<Entity>().SetModeSelected();
                    }
                }
                else if ((_IgnoreLayerMask & (1 << hit.transform.gameObject.layer)) == 0 && hit.transform.gameObject.GetComponent<Stopper>() != null 
                    &&hit.transform.gameObject.GetComponent<Stopper>().stopperInRange && !hit.transform.gameObject.GetComponent<Stopper>().IsAware)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Stopper"))
                    {
                        _SelectedEntity = hit.transform.gameObject;
                    }
                }
                else if (_SelectedEntity != null)
                {
                    if (_SelectedEntity.layer == LayerMask.NameToLayer("Entitée"))
                    {
                        _SelectedEntity.GetComponent<Entity>().SetModeInRange();
                    }
                    else if (_SelectedEntity.layer == LayerMask.NameToLayer("Stopper"))
                    {
                        _SelectedEntity.GetComponent<Stopper>().SetModeInRange();
                    }
                    _SelectedEntity = null;
                }
            }
            else if (_SelectedEntity != null) //if the raycast hits nothing
            {
                if (_SelectedEntity.layer == LayerMask.NameToLayer("Entitée"))
                {
                    _SelectedEntity.GetComponent<Entity>().SetModeInRange();
                }
                else if (_SelectedEntity.layer == LayerMask.NameToLayer("Stopper"))
                {
                    _SelectedEntity.GetComponent<Stopper>().SetModeInRange();
                }
                _SelectedEntity = null;
            }
        }

        private void DisableSwapping()
        {
            _CanSwape = false;
        }

        private void EnableSwapping()
        {
            _CanSwape = true;
        }

        public GameObject CurrentEntity
        {
            get { return _CurrentEntity; }
            set { _CurrentEntity = value; }
        }
    }
}
