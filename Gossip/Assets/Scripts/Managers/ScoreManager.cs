using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI _Text;
    public uint _SpreadCount { get; private set; }

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
    // Start is called before the first frame update
    void Start()
    {
        _SpreadCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void IncreaseCount()
    {
        _SpreadCount++;
        _Text.text = "Spread: " + _SpreadCount;
    }
}
