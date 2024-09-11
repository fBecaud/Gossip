using System.Collections;
using UnityEngine;
using FMODUnity;

namespace Gossip.Utilitaries.Managers
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;

        [Header("Camera Settings")]
        [SerializeField] public float mouseSensitivity = 1f;
        [SerializeField] public float keyboardSensitivity = .2f;
        [SerializeField] public float touchSensitivity = .2f;
        [SerializeField] public float cameraAngleSpeed = 10f;
        private float _CameraTravelTime;

        [Header("Target Settings")]
        [SerializeField] public GameObject target;
        [SerializeField] private float _CurrentTargetDistance = 5f;
        [SerializeField] private float minDistanceFromWall = 0.5f;
        [SerializeField] private float maxCameraDistance = 10f;

        [Header("Camera Angle Constraints")]
        [SerializeField] private float _MinXAngleCamLock = -30f;
        [SerializeField] private float _MaxXAngleCamLock = 90f;

        [Header("Collision Settings")]
        [SerializeField] private LayerMask collisionLayer;  // Layers to detect collisions (e.g., walls)
        [SerializeField] private float collisionRadius = 0.5f;  // Radius for SphereCast
        [SerializeField] private float positionSmoothSpeed = 10f;  // Speed for smoothing camera position transitions

        [Header("Sound Settings")]
        [SerializeField] private EventReference _EntityTransitionSound;

        private SphereCollider cameraCollider;
        private Vector3 currentCameraPosition;

        private float _YAxis;
        private float _XAxis;
        private float _RotationYAxis;
        private float _RotationXAxis;
        private bool _IsTransitioning;

        // String Settings
        private const string MOUSE_Y = "Mouse Y";
        private const string MOUSE_X = "Mouse X";

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            // Ensure the camera has a SphereCollider
            cameraCollider = GetComponent<SphereCollider>();
            if (cameraCollider == null)
            {
                Debug.LogError("SphereCollider is missing on the Camera!");
            }
            else
            {
                cameraCollider.isTrigger = true; // Make sure it's a trigger
                cameraCollider.radius = collisionRadius;
            }

        }

        private void Start()
        {
            if (target != null)
            {
                target.GetComponentInChildren<Entity>().SetModeCurrentEntity();
            }
            _CameraTravelTime = TimeManager.instance.FreezeTotalDuration;
            currentCameraPosition = transform.position;
            _IsTransitioning = false;
        }

        private void Update()
        {
            _CameraTravelTime = TimeManager.instance.FreezeTotalDuration; //To remove after tests
        }

        private void OnEnable()
        {
            EventManager.instance.OnEntityChangedGameObject += ChangeTarget;
        }

        private void OnDisable()
        {
            EventManager.instance.OnEntityChangedGameObject -= ChangeTarget;
        }

        public void ChangeTarget(GameObject pTarget)
        {
            AudioManager.instance.PlayOneShot(_EntityTransitionSound, transform.position);
            StartCoroutine(UpdateCameraPosition(pTarget, _CameraTravelTime));
        }

        public void StopCurrentCoroutine()
        {
            StopCoroutine(UpdateCameraPosition(target, _CameraTravelTime));
        }

        private void UpdateRotation()
        {
            _RotationYAxis += _XAxis;
            _RotationXAxis -= _YAxis;
        }

        private IEnumerator UpdateCameraPosition(GameObject pTarget, float pTravelTime)
        {
            _IsTransitioning = true;
            Vector3 lStartPosition = transform.position;
            Vector3 lEndPosition = pTarget.transform.position - transform.forward * _CurrentTargetDistance;
            float startTime = Time.time;

            while (Time.time < startTime + pTravelTime)
            {
                float elapsedTime = (Time.time - startTime) / pTravelTime;
                transform.position = Vector3.Lerp(lStartPosition, lEndPosition, elapsedTime);
                currentCameraPosition = transform.position; // Update smoothed position
                yield return null;
            }
            target = pTarget;
            transform.position = lEndPosition;
            currentCameraPosition = lEndPosition;
            _IsTransitioning = false;
        }

        public void UpdateTargetDistance(float pTargetDistance)
        {
            _CurrentTargetDistance = Mathf.Clamp(pTargetDistance, minDistanceFromWall, maxCameraDistance);
        }

        void LateUpdate()
        {   
            if (!_IsTransitioning) 
            { 
                HandleInput();
                UpdateCameraRotation();
                HandleCameraCollision();
            }
        }

        private void HandleInput()
        {
            if (Input.GetMouseButton(1))
            {
                _XAxis = Input.GetAxis(MOUSE_X) * mouseSensitivity;
                _YAxis = Input.GetAxis(MOUSE_Y) * mouseSensitivity;
                UpdateRotation();
            }
        }

        private void UpdateCameraRotation()
        {
            _RotationXAxis = Mathf.Clamp(_RotationXAxis, _MinXAngleCamLock, _MaxXAngleCamLock);
            transform.localEulerAngles = new Vector3(_RotationXAxis, _RotationYAxis, 0);
        }

        private Vector3 velocity = Vector3.zero;

        private void HandleCameraCollision()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = -transform.forward;
            Vector3 desiredPosition = target.transform.position + direction * _CurrentTargetDistance;

            Vector3 directionToCamera = desiredPosition - target.transform.position;
            float desiredDistance = _CurrentTargetDistance;

            RaycastHit hit;
            if (Physics.SphereCast(target.transform.position, collisionRadius, directionToCamera.normalized, out hit, desiredDistance, collisionLayer))
            {
                float adjustedDistance = Mathf.Clamp(hit.distance - minDistanceFromWall, 0.0f, desiredDistance);
                desiredPosition = target.transform.position + directionToCamera.normalized * adjustedDistance;
            }

            // Smoothly interpolate using SmoothDamp
            currentCameraPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1 / positionSmoothSpeed);
            transform.position = currentCameraPosition;
        }

    }
}
