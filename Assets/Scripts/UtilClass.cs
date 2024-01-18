using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilClass
{
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPositions()
    {
        if (mainCamera == null) { mainCamera = Camera.main; }
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degress = Mathf.Rad2Deg * radians;
        return degress;
    }
}
