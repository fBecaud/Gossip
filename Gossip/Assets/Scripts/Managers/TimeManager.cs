using Gossip.Utilitaries.Managers;
using System.Collections;
using UnityEngine;

namespace Gossip.Utilitaries.Managers
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager instance;

        [SerializeField] private float _TimeScale = 0;
        [Header("Has to be at least twice the length of Lerp Freeze Ratio")]
        [SerializeField] private float _FreezeTotalDuration;

        [Space (10)]
        [SerializeField] private float _FreezeSingleDuration;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance);
                return;
            }
            instance = this;
            _TimeScale = 1;
        }


        public void FreezeTime()
        {
            EventManager.instance.TimeFreezeStarted();
            StartCoroutine(LerpTimeScale(1, 0, _FreezeTotalDuration));
        }

        public void TimeToNormal()
        {
            StartCoroutine(LerpTimeScale(0, 1, _FreezeTotalDuration, () => { EventManager.instance.TimeFreezeEnded(); }));
        }
        
        public void TempFreezeTime()
        {
            StartCoroutine(TempFreezeCoroutine());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(TempFreezeCoroutine());
            }
        }

        private IEnumerator TempFreezeCoroutine()
        {
            EventManager.instance.TimeFreezeStarted();
            float lWaitDuration = _FreezeTotalDuration - 2 * _FreezeSingleDuration;

            yield return StartCoroutine(LerpTimeScale(1, 0, _FreezeSingleDuration));

            yield return new WaitForSeconds(lWaitDuration);

            yield return StartCoroutine(LerpTimeScale(0, 1, _FreezeSingleDuration, () => { EventManager.instance.TimeFreezeEnded(); }));
        }

        private IEnumerator LerpTimeScale(float pFromTimeScale, float pToTimeScale, float pDuration, System.Action pOnComplete = null)
        {
            float lElapsed = 0f;

            while (lElapsed < pDuration)
            {
                lElapsed += Time.deltaTime;
                _TimeScale = Mathf.Lerp(pFromTimeScale, pToTimeScale, lElapsed / pDuration);
                yield return null;
            }

            _TimeScale = pToTimeScale;

            pOnComplete?.Invoke();
        }

        public float TimeScale => _TimeScale;
        public float FreezeTotalDuration => _FreezeTotalDuration;
    }
}