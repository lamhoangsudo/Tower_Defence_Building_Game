using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private TextMeshProUGUI NumberWaveSurival;
    public static GameOverUI Instance;
    private void Awake()
    {
        Instance = this;
        Hide();
    }
    private void Start()
    {
        btnMainMenu.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        btnRetry.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
    }
    public void Show()
    {
        NumberWaveSurival.SetText("You Survived " + EnemyWaveManager.Instance.wave + " Waves!");
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
