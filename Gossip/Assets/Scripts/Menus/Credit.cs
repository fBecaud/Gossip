using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gossip.Menus
{
    public class Credit : MonoBehaviour
    {
        [SerializeField] private float _Speed;

        [SerializeField] private Transform _Names;
        [SerializeField] private Transform _FinalPos;
        private const string TITLE_CARD_SCENE = "TitleCard";

        void Start()
        {
            StartCoroutine(CreditCoroutine());
        }

        private IEnumerator CreditCoroutine()
        {
            while (_Names.position != _FinalPos.position)
            {
                _Names.position = Vector3.MoveTowards(_Names.position, _FinalPos.position, _Speed);
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(TITLE_CARD_SCENE);

            StopCoroutine(CreditCoroutine());
        }
    }
}

