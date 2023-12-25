using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform PfWoodHarvester;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Instantiate(PfWoodHarvester, GetMouseWorldPositions(), Quaternion.identity);
        }
    }
    private Vector3 GetMouseWorldPositions()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
