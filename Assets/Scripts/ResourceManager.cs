using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    public event EventHandler OnResourceAmountChanged;
    public static ResourceManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        foreach (var resourceTypeSO in resourceTypeListSO.resourceTypesList)
        {
            resourceAmountDictionary.Add(resourceTypeSO, 100);
        }
    }
    public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
    {
        resourceAmountDictionary[resourceTypeSO] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetResourceTypeAmount(ResourceTypeSO resourceTypeSO)
    {
        return resourceAmountDictionary[resourceTypeSO];
    }
    public bool CanAffort(BuildResourceAmount[] buildResourceAmounts)
    {
        foreach(var amount in buildResourceAmounts)
        {
            if(GetResourceTypeAmount(amount.resource) < amount.amount)
            {
                return false;
            }
        }
        return true;
    }
    public void SetResourceAfterBuild(BuildResourceAmount[] buildResourceAmounts)
    {
        foreach (var amount in buildResourceAmounts)
        {
            resourceAmountDictionary[amount.resource] -= amount.amount;
        }
    }
}
