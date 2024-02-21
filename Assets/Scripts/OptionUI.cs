using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI soundVolume;
    [SerializeField]
    private TextMeshProUGUI musicVolume;
    private void Awake()
    {
        transform.Find("BtnIncreSound").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseSound();
            UpdateSoundText();
        });
        transform.Find("BtnDecreSound").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.DecreaseSound();
            UpdateSoundText();
        });
        transform.Find("BtnIncreMusic").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseMusic();
            UpdateMusicText();
        });
        transform.Find("BtnDecreMusic").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseMusic();
            UpdateMusicText();
        });
        transform.Find("BtnMainMenu").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        transform.Find("ToggleEdgeScrolling").GetComponent<Toggle>().onValueChanged.AddListener((bool set) =>
        {
            CameraHandler.instance.setScrolling(set); 
        });
        Time.timeScale = 1f;
    }
    private void Start()
    {
        UpdateSoundText();
        UpdateMusicText();
        this.gameObject.SetActive(false);
        transform.Find("ToggleEdgeScrolling").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.instance.setEdgeScrolling);
    }
    private void UpdateSoundText()
    {
        soundVolume.SetText(SoundManager.Instance.volumeNormalize().ToString());
    }
    private void UpdateMusicText()
    {
        musicVolume.SetText(MusicManager.Instance.volumeNormalize().ToString());
    }
    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
