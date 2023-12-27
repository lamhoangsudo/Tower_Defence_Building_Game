using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneretor : MonoBehaviour
{
    private float timer;
    private float timerMax = 1f;
    private BuildingTypeSO buildingType;
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingTypeSO;
        timerMax = buildingType.resourceGeneratorData.timerGenerator;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceTypeSO, 1);
        }
    }
}
