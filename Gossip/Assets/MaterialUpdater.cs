using System.Collections;
using UnityEngine;

public class MaterialUpdater : MonoBehaviour
{
    [SerializeField] private Material _Material;
    [SerializeField] private string _VariableName;
    [SerializeField] private float _LerpDuration;
    [SerializeField] private float _TargetValue;

    private Material _InstanceMaterial;

    private void Awake()
    {
        // Create an instance of the material so changes only affect this object
        _InstanceMaterial = new Material(_Material);

        // Apply the new instance material to this object's renderer
        GetComponent<Renderer>().material = _InstanceMaterial;
    }

    public void UpdateMaterial()
    {
        if (_InstanceMaterial && _VariableName != string.Empty)
        {
            StartCoroutine(MaterialLerpUpdater(_InstanceMaterial, _VariableName, _LerpDuration, _TargetValue));
        }
    }

    private IEnumerator MaterialLerpUpdater(Material pMaterial, string pVariableName, float pLerpDuration, float pTargetValue)
    {
        float startValue = pMaterial.GetFloat(pVariableName);
        float elapsedTime = 0f;

        while (elapsedTime < pLerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.Lerp(startValue, pTargetValue, elapsedTime / pLerpDuration);

            pMaterial.SetFloat(pVariableName, lerpValue);

            yield return null; // Wait for the next frame
        }

        // Ensure the material is set to the exact target value at the end
        pMaterial.SetFloat(pVariableName, pTargetValue);
    }
}
