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
    public static Vector3 GetRamdomVector()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
    public static Transform LookForTargets(Transform pointCenter, float radius, string componentType)
    {
        Collider2D[] targetEnemyFound = Physics2D.OverlapCircleAll(pointCenter.position, radius);
        Transform target = null;
        foreach (Collider2D targetEnemy in targetEnemyFound)
        {
            if (targetEnemy.GetComponent(componentType) != null)
            {
                if (target != null)
                {
                    if (Vector3.Distance(pointCenter.position, targetEnemy.transform.position) < Vector3.Distance(pointCenter.position, target.position))
                    {
                        target = targetEnemy.transform;
                    }
                }
                else
                {
                    target = targetEnemy.transform;
                }
            }
        }
        return target;
    }
}
