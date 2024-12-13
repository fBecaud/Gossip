using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gossip.Menus
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected Image m_TransitionScreen;
        [SerializeField] protected float m_TransitionSpeed;

        protected float m_ElapsedTime;

        protected const string TITLE_CARD_SCENE = "TitleCard";
        protected const string GAME_SCENE = "SCN_GOSSIP";
        protected const string CREDIT_SCENE = "Credit";

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            TransitionOut();
        }

        protected void TransitionIn(string pSceneName)
        {
            m_ElapsedTime = 0;
            StartCoroutine(TransitionInCoroutine(pSceneName));
        }

        protected IEnumerator TransitionInCoroutine(string pSceneName)
        {
            m_TransitionScreen.color = Color.clear;
            m_TransitionScreen.gameObject.SetActive(true);

            while (m_TransitionScreen.color != Color.black)
            {
                m_ElapsedTime += Time.deltaTime;
                m_TransitionScreen.color = Color.Lerp(Color.clear, Color.black, m_ElapsedTime * m_TransitionSpeed);
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(pSceneName);

            StopCoroutine(TransitionInCoroutine(pSceneName));
        }

        protected void TransitionOut()
        {
            m_ElapsedTime = 0;
            StartCoroutine(TransitionOutCoroutine());
        }

        protected virtual IEnumerator TransitionOutCoroutine()
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

            StopCoroutine(TransitionOutCoroutine());
        }
    }
}

