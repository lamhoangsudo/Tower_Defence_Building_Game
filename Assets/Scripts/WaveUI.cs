using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveNumber;
    [SerializeField] private TextMeshProUGUI nextWaveTime;
    [SerializeField] private RectTransform waveEnenmyPointer;
    [SerializeField] private RectTransform enemyPointer;
    private Vector3 movedir;
    private new Camera camera;
    private Vector3 spawnPositionTransform;
    private void Start()
    {
        EnemyWaveManager.Instance.OnNumberWaveChange += Instance_OnNumberWaveChange;
        camera = Camera.main;
    }

    private void Instance_OnNumberWaveChange(object sender, System.EventArgs e)
    {
        spawnPositionTransform = EnemyWaveManager.Instance.spawnPositionTransform.position;
        SetTextWaveNumber();
    }

    private void Update()
    {
        SetTextNextWaveTime();
        HanderWaveEnenmyPointer();
        HanderEnemyPointer();
    }
    private void HanderWaveEnenmyPointer()
    {
        if (EnemyWaveManager.Instance.timeToSpawn > 0)
        {
            waveEnenmyPointer.gameObject.SetActive(false);
        }
        else
        {
            PointToPosition(spawnPositionTransform, 400f, waveEnenmyPointer);
        }
    }
    private void HanderEnemyPointer()
    {
        Transform target = UtilClass.LookForTargets<EnemySystem>(camera.transform, 900f);
        if (target != null)
        {
            PointToPosition(target.position, 250f, enemyPointer);
            enemyPointer.gameObject.SetActive(true);
        }
        else
        {
            enemyPointer.gameObject.SetActive(false);
        }
    }
    private void SetTextWaveNumber()
    {
        waveNumber.SetText("Wave " + EnemyWaveManager.Instance.wave.ToString());
    }
    private void SetTextNextWaveTime()
    {
        if (EnemyWaveManager.Instance.timeToSpawn > 0)
        {
            nextWaveTime.SetText("Next Wave In " + EnemyWaveManager.Instance.timeToSpawn.ToString("F1") + "s");
            nextWaveTime.gameObject.SetActive(true);
        }
        else
        {
            nextWaveTime.gameObject.SetActive(false);
        }
    }
    private void PointToPosition(Vector3 position, float distanceUI, RectTransform pointer)
    {
        movedir = (position - camera.transform.position).normalized;
        pointer.anchoredPosition = movedir * distanceUI;
        pointer.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(movedir));
        float distance = Vector3.Distance(position, camera.transform.position);
        pointer.gameObject.SetActive(distance > camera.orthographicSize * 1.5f);
    }
}
