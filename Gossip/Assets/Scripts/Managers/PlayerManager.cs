using System.Collections;
using UnityEngine;

namespace Gossip.Utilitaries.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        [SerializeField] private GameObject _CurrentEntity;
        [SerializeField] private GameObject _SelectedEntity;

        [SerializeField] private LayerMask _EntityLayerMask; // Layer mask to filter entity colliders
        [SerializeField] private LayerMask _IgnoreLayerMask; // Layer mask to ignore colliders

        private int _CombinedLayerMask;
        [SerializeField] private bool _CanSwape;
        [SerializeField] private bool _IsOnTransitioner = false;

        [SerializeField] private GameObject _TrailPrefab;
        [SerializeField] private Vector3 _TrailOffset;
        private GameObject _TrailInstance;

        private void Awake()
        {
            _CanSwape = false;
            if (instance != null)
            {
                Destroy(instance.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            _TrailInstance = Instantiate(_TrailPrefab);
            _TrailInstance.SetActive(false);
        }

        private void Start()
        {
            _CombinedLayerMask = LayerMask.GetMask("Entit�e", "Stopper", "Transitioner");
            _IsOnTransitioner = false;

            if (_CurrentEntity.GetComponentInChildren<Entity>() != null) _CurrentEntity.GetComponentInChildren<Entity>().IsCorrupted = true;
        }

        private void Update()
        {
            if (_CanSwape && !_IsOnTransitioner && PauseMenuManager.instance.isPaused == false)
            {
                if (Input.GetMouseButtonDown(0) && _SelectedEntity != null )
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
            AudioManager.instance.PlayTransitionSound();
            TimeManager.instance.TempFreezeTime();
            _CurrentEntity.GetComponentInChildren<Character>().SetModeUsual();

            bool lCanIncreaseScore = true;
            if (_SelectedEntity.GetComponentInChildren<Entity>() != null && _SelectedEntity.GetComponentInChildren<Entity>().IsCorrupted) lCanIncreaseScore = false;

            StartCoroutine(UpdateTrailPosition(_SelectedEntity, TimeManager.instance.FreezeTotalDuration, lCanIncreaseScore));

            if (_SelectedEntity.GetComponentInChildren<Entity>() != null) _SelectedEntity.GetComponentInChildren<Entity>().IsCorrupted = true;

            _CurrentEntity = _SelectedEntity; //Changing entity
            _SelectedEntity = null;
            _CurrentEntity.GetComponentInChildren<Character>().SetModeCurrentEntity();
            EventManager.instance.EntityChanged();
            EventManager.instance.EntityChanged(_CurrentEntity);
        }

        public void ChangeEntityAutomated(GameObject pNewEntity)
        {
            _SelectedEntity = pNewEntity;
            pNewEntity.transform.GetComponentInChildren<Entity>().SetModeSelected();
            FindNewEntity();
            _CanSwape = true;
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

                Entity lEntity = hit.transform.GetComponentInChildren<Entity>();
                Stopper lStopper = hit.transform.GetComponentInChildren<Stopper>();


                if ((_IgnoreLayerMask & (1 << hit.transform.gameObject.layer)) == 0)
                {
                    if (_SelectedEntity != null && hit.transform.gameObject != _SelectedEntity)
                    {
                        ResetSelectedEntity();
                    }
                    if (lEntity != null && lEntity.IsInRange)
                    {
                        _SelectedEntity = hit.transform.gameObject;
                        lEntity.SetModeSelected();
                    }
                    else if (lStopper != null && lStopper.IsInRange && !lStopper.IsAware)
                    {
                        _SelectedEntity = hit.transform.gameObject;
                    }
                    else if (_SelectedEntity != null)
                    {
                        // If no entity or stopper is selected, revert the selection
                        ResetSelectedEntity();
                    }
                }
            }
            else if (_SelectedEntity != null) //if the raycast hits nothing
            {
                ResetSelectedEntity();
            }
        }

        private void ResetSelectedEntity()
        {
            
            // Reset the selected entity to its in-range state if it was selected before
            if (_SelectedEntity != null)
            {
                Entity entity = _SelectedEntity.GetComponentInChildren<Entity>();
                //Stopper stopper = _SelectedEntity.GetComponentInChildren<Stopper>();

                if (entity != null)
                {
                    entity.CancelSelectedMode();
                }
                //else if (stopper != null)
                //{
                //    stopper.SetModeInRange();
                //}

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

        public GameObject CurrentCharacter
        {
            get { return _CurrentEntity; }
            set { _CurrentEntity = value; }
        }

        public bool IsOnTransitioner
        {
            get { return _IsOnTransitioner; }
            set { _IsOnTransitioner = value; }
        }
        private IEnumerator UpdateTrailPosition(GameObject pTarget, float pTravelTime, bool pCanIncreaseScore)
        {
            _TrailInstance.SetActive(true);
            _TrailInstance.transform.position = _CurrentEntity.transform.position;
            Vector3 lStartPosition = _TrailInstance.transform.position + _TrailOffset;
            Vector3 lEndPosition = pTarget.transform.position + _TrailOffset;

            float startTime = Time.time;

            while (Time.time < startTime + pTravelTime)
            {
                float elapsedTime = (Time.time - startTime) / pTravelTime;
                _TrailInstance.transform.position = Vector3.Lerp(lStartPosition, lEndPosition, elapsedTime);

                yield return null;
            }
            _TrailInstance.SetActive(false);

            if (pCanIncreaseScore) ScoreManager.instance.IncreaseCount();

            StopCoroutine(UpdateTrailPosition(pTarget, pTravelTime, pCanIncreaseScore));
        }

        public bool CanSwap
        {
            get { return _CanSwape; }
            set { _CanSwape = value; }
        }
    }
}
