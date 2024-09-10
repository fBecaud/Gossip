using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI _Text;
    public uint _SpreadCount { get;  private set; }

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
