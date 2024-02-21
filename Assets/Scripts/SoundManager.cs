using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        BuildingDamaged,
        BuildingDestroyed,
        BuildingPlaced,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
        GameOver,
    }
    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> audioClipDictionary;
    private float volumeSound;
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach(Sound sound in Enum.GetValues(typeof(Sound)))
        {
            audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        volumeSound = PlayerPrefs.GetFloat("volumeSound", 0.5f);
    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(audioClipDictionary[sound], volumeSound);
    }
    public void IncreaseSound()
    {
        volumeSound += .1f;
        volumeSound = Mathf.Clamp01(volumeSound);
    }
    public void DecreaseSound()
    {
        volumeSound -= .1f;
        volumeSound = Mathf.Clamp01(volumeSound);
    }
    public int volumeNormalize()
    {
        PlayerPrefs.SetFloat("volumeSound", volumeSound);
        return Mathf.FloorToInt(volumeSound * 10);
    }
}
