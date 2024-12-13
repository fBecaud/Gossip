using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gossip.Menus
{
    public class Credit : Menu
    {
        [SerializeField] private float _Speed;

        [SerializeField] private Transform _Names;
        [SerializeField] private Transform _FinalPos;

        [SerializeField] private Button _QuitButton;

        protected override void Init()
        {
            _QuitButton.onClick.AddListener(Quit);

            base.Init();
        }

        private IEnumerator CreditCoroutine()
        {
            while (_Names.position != _FinalPos.position)
            {
                _Names.position = Vector3.MoveTowards(_Names.position, _FinalPos.position, _Speed);
                yield return new WaitForEndOfFrame();
            }

            TransitionIn(TITLE_CARD_SCENE);

            StopCoroutine(CreditCoroutine());
        }

        private void Quit()
        {
            AudioManager.instance.PlayClickSound();

            StopCoroutine(CreditCoroutine());
            TransitionIn(TITLE_CARD_SCENE);
        }

        protected override IEnumerator TransitionOutCoroutine()
        {
            m_TransitionScreen.color = Color.black;
            m_TransitionScreen.gameObject.SetActive(true);

            while (m_TransitionScreen.color != Color.clear)
            {
                m_ElapsedTime += Time.deltaTime;
                m_TransitionScreen.color = Color.Lerp(Color.black, Color.clear, m_ElapsedTime * m_TransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            m_TransitionScreen.gameObject.SetActive(false);

            StartCoroutine(CreditCoroutine());

            StopCoroutine(TransitionOutCoroutine());
        }
    }
}

