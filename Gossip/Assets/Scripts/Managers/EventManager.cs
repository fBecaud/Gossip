using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace Gossip.Utilitaries.Managers
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public event Action<GameObject> OnEntityChangedGameObject;
        public event Action OnEntityChanged;
        public event Action OnTimeFreezeStarted;
        public event Action OnTimeFreezeEnded;

        public void EntityChanged() => OnEntityChanged?.Invoke();
        public void EntityChanged(GameObject pEntity)
        {
            OnEntityChangedGameObject?.Invoke(pEntity);
        }

        public void TimeFreezeStarted() => OnTimeFreezeStarted?.Invoke();
        public void TimeFreezeEnded() => OnTimeFreezeEnded?.Invoke();
    }
}