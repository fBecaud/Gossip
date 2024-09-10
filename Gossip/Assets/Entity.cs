using UnityEngine;
using FMODUnity;

public class Entity : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private EventReference _TalkingSound;
    [SerializeField] private EventReference _CorruptedTalkingSound;
    [SerializeField] private EventReference _WalkingSound;

    [Space(10)]
    public bool entityInRange;
    public bool isCorrupted;

    private void OnMouseOver()
    {
        //Changer outline shader
    }

    public void UpdateCorrupted()
    {
        isCorrupted = true;

    }
}
