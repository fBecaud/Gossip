using System.Collections;
using UnityEngine;
using FMODUnity;

namespace Gossip.Utilitaries.Managers
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;

        [SerializeField] public float mouseSensitivity = 1f;
        [SerializeField] public float keyboardSensitivity = .2f;
        [SerializeField] public float touchSensitivity = .2f;
        [SerializeField] public GameObject target;
        [SerializeField] private float _CurrentTargetDistance;
        [SerializeField] private float _MinXAngleCamLock = -30;
        [SerializeField] private float _MaxXAngleCamLock = 90;

        [SerializeField] public float cameraAngleSpeed = 10f;
        public bool cameraMoving;

        public float cameraTravelTime = 1.5f;

        private float _YAxis;
        private float _XAxis;

        private float _RotationYAxis;
        private float _RotationXAxis;
        private bool _IsMoving;
        public bool onSlider;
        [SerializeField] private bool _IsInWall;

        //String Settings
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const string MOUSE_Y = "Mouse Y";
        private const string MOUSE_X = "Mouse X";

        [SerializeField] private EventReference _EntityTransitionSound;

        private void Awake()
        {
            if (instance != null)
            {
                return;
            }
            instance = this;
        }

        private void Start()
        {
            if (target != null)
            {
                target.GetComponent<Entity>().SetModeCurrentEntity();
            }
        }

        private void OnEnable()
        {
            EventManager.instance.OnEntityChangedGameObject += ChangeTarget;
        }

        private void OnDisable()
        {
            EventManager.instance.OnEntityChangedGameObject -= ChangeTarget;
        }

        public void ChangeTarget(GameObject pTarget/*, float pTargetDistance*/)
        {
            AudioManager.instance.PlayOneShot(_EntityTransitionSound, transform.position);
            StartCoroutine(UpdateCameraPosition(pTarget, cameraTravelTime/*, pTargetDistance*/));
        }

        public bool IsInCoroutine()
        {
            if (_IsMoving)
            {
                return true;
            }
            else return false;
        }

        public void StopCurrentCoroutine()
        {
            StopCoroutine(UpdateCameraPosition(target, cameraTravelTime/*, _CurrentTargetDistance*/));
        }

        private void UpdateRotation()
        {
            _RotationYAxis += _XAxis;
            _RotationXAxis -= _YAxis;
        }

        void LateUpdate()
        {
            if (GameManager.instance._isPaused)
                return;
            if (Input.GetMouseButton(1))
            {
                cameraMoving = true;
                _XAxis = Input.GetAxis(MOUSE_X) * mouseSensitivity;
                _YAxis = Input.GetAxis(MOUSE_Y) * mouseSensitivity;
                UpdateRotation();
            }

            _RotationXAxis = Mathf.Clamp(_RotationXAxis, _MinXAngleCamLock, _MaxXAngleCamLock);


            transform.localEulerAngles = new Vector3(_RotationXAxis, _RotationYAxis, 0);
            transform.position = target.transform.position - transform.forward * _CurrentTargetDistance;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTarget">The object the camera will look at.</param>
        /// <param name="pTravelTime">The time in seconds used for the camera to move to the target.</param>
        /// <param name="pTargetDistance">The distance between the camera and the target.</param>
        /// <returns></returns>
        private IEnumerator UpdateCameraPosition(GameObject pTarget, float pTravelTime/*, float pTargetDistance*/)
        {
            _IsMoving = true;
            Vector3 lStartPosition = transform.position;
            Vector3 lEndPosition = pTarget.transform.position - transform.forward * /*pTargetDistance*/_CurrentTargetDistance;
            float startTime = Time.time;

            while (Time.time < startTime + pTravelTime)
            {
                float elapsedTime = (Time.time - startTime) / pTravelTime;
                transform.position = Vector3.Lerp(lStartPosition, lEndPosition, elapsedTime);
                yield return null;
            }
            target = pTarget;
            //_CurrentTargetDistance = pTargetDistance;

            transform.position = lEndPosition;

            _IsMoving = false;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag(TagManager.WALL_TAG))
        //    {
        //        _IsInWall = true;
        //    }
        //}

        public void UpdateTargetDistance(float pTargetDistance)
        {
            _CurrentTargetDistance = pTargetDistance;
        }
    }
}
