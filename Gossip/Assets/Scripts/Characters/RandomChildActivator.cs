using UnityEngine;

public class RandomChildActivator : MonoBehaviour
{
    public GameObject parentObject;

    private void DeactivateAllChildren()
    {
        int childCount = parentObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            parentObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ActivateRandomChild()
    {
        int childCount = parentObject.transform.childCount;

        DeactivateAllChildren();

        int randomIndex = Random.Range(0, childCount);

        GameObject selectedChild = parentObject.transform.GetChild(randomIndex).gameObject;
        selectedChild.SetActive(true);
    }

    private void Start()
    {
        DeactivateAllChildren(); 
        ActivateRandomChild();   
    }
}
