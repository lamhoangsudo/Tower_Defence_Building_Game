using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private float volumeMusic;
    private AudioSource audioMusic;
    private void Awake()
    {
        Instance = this;
        audioMusic = GetComponent<AudioSource>();
        volumeMusic = PlayerPrefs.GetFloat("volumeMusic", 0.5f);
        audioMusic.volume = volumeMusic;
    }
    public void IncreaseMusic()
    {
        volumeMusic += .1f;
        volumeMusic = Mathf.Clamp01(volumeMusic);
        audioMusic.volume = volumeMusic;
    }
    public void DecreaseMusic()
    {
        volumeMusic -= .1f;
        volumeMusic = Mathf.Clamp01(volumeMusic);
        audioMusic.volume = volumeMusic;
    }
    public int volumeNormalize()
    {
        PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
        return Mathf.FloorToInt((volumeMusic / 1) * 10);
    }
}
