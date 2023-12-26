using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        foreach (var resourceTypeSO in resourceTypeListSO.resourceTypesList)
        {
            //same resourceAmountDictionary[resourceTypeSO] = 0;
            resourceAmountDictionary.Add(resourceTypeSO, 0);
        }
    }
    public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
    {
        resourceAmountDictionary[resourceTypeSO] += amount;
    }
}
