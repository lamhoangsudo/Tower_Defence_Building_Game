using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearByOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private Transform icon;
    [SerializeField] private Transform text;
    private int percentage;
    private void Update()
    {
        percentage = Mathf.RoundToInt((float)ResourceGeneretor.GetNearByResourceNode(resourceGeneratorData, transform.position) / resourceGeneratorData.maxResourceAmount *  100);
        text.GetComponent<TextMeshPro>().text = percentage.ToString() + "%";
    }
    public void SetResourceGeneretor(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        icon.GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceTypeSO.sprite;
    }
}
