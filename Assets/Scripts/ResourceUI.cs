using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] Transform resourceTemplate;
    private ResourceTypeListSO resourceTypeListSO;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
    private void Awake()
    {      
        resourceTemplate.gameObject.SetActive(false);
        resourceTypeListSO = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        int index = 0;
        foreach (var resourceType in resourceTypeListSO.resourceTypesList)
        {
            resourceTemplate.gameObject.SetActive(true);
            resourceTemplate = Instantiate(resourceTemplate, transform);
            resourceTemplate.name = "resource"+ resourceType.resourceTypeName +"Template";
            float offsetAmount = -160f;
            resourceTypeTransformDictionary.Add(resourceType, resourceTemplate);
            resourceTypeTransformDictionary[resourceType].GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            resourceTypeTransformDictionary[resourceType].Find("Image").GetComponent<Image>().sprite = resourceType.sprite;         
            index++;
        }       
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += Instance_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void Instance_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (var resourceType in resourceTypeListSO.resourceTypesList)
        {
            int amount = ResourceManager.Instance.GetResourceTypeAmount(resourceType);
            resourceTypeTransformDictionary[resourceType].Find("Text").GetComponent<TextMeshProUGUI>().SetText(amount.ToString());
        }
    }
}
