using System;
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

        public event Action<GameObject> OnEntityChanged;

        public void EntityChanged(GameObject pEntity) => OnEntityChanged?.Invoke(pEntity);
    }
}