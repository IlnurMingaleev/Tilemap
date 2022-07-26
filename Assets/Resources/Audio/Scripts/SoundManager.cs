using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound 
    {
        playerMove,
        playerAttack,
        playerDeath,
        playerPickUpCoins,
        playerPickUp,
        playerUseBottle,
        monsterRoar,
        monsterAttack,
        monsterDeath,
        buttonOnClick,
        battle,
        mainTheme
    
    }
    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    

    public static void InitializeDictionary() 
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.playerMove] = 0f;
    } 
    private static bool CanPlaySound(Sound sound) 
    {
        switch (sound) 
        {
            default:
                return true;
            case Sound.playerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.5f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }

        }
    }
    public static void PlayAudioClip(Sound sound) 
    {
        if (CanPlaySound(sound))
        {
            if(oneShotGameObject == null) {
                oneShotGameObject = new GameObject();
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.volume = 0.1f;

            }
            
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));

        }

    }
    public static void PlayAudioClip(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject();
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = 0.1f;
            audioSource.Play();
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }

    }
    public static AudioClip GetAudioClip(Sound sound) 
    {
        foreach (GameAssets.SoundAudioClip clip in GameAssets.Instance.soundAudioClips) 
        {
            if (clip.sound == sound) return clip.audioClip;
        }
        Debug.LogError(sound + " not found.");
        return null;
    }
}
