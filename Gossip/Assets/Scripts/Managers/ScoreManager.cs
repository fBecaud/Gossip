using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static TextMeshProUGUI _Text;
    static public uint _SpreadCount { get;  private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            return;
        }
        instance = this;
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
    static public void IncreaseCount()
    {
        _SpreadCount++;
        _Text.text = "Spread: " + _SpreadCount;
    }
}
