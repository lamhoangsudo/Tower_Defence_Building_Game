using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneretor : MonoBehaviour
{
    private float timer;
    private float timerMax;
    public ResourceGeneratorData resourceGeneratorData { get; private set; }
    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingTypeSO.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerGenerator;
    }
    private void Start()
    {
        int nearbyResourceNode = GetNearByResourceNode(resourceGeneratorData, transform.position);
        if (nearbyResourceNode == 0)
        {
            enabled = false;
        }
        else
        {
            timerMax += resourceGeneratorData.timerGenerator * (1 - (float)nearbyResourceNode / resourceGeneratorData.maxResourceAmount);
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceTypeSO, 1);
        }
    }
    public float GetTimeNormalize() 
    { 
        return timer / timerMax; 
    }
    public float GetResourceGeneratorPerSecond()
    {
        return 1 / timerMax;
    }
    public static int GetNearByResourceNode(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceNode = 0;
        foreach (Collider2D collider in collider2DArray)
        {
            if (collider.GetComponent<ResourceNode>() != null && collider.GetComponent<ResourceNode>().ResourceTypeSO.Equals(resourceGeneratorData.resourceTypeSO))
            {
                nearbyResourceNode++;
            }
        }
        nearbyResourceNode = Mathf.Clamp(nearbyResourceNode, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceNode;
    }
}
