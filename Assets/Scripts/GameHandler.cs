using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private MonsterCharacterBehavior monster;
    private void Awake()
    {
        SoundManager.InitializeDictionary();
        audioSource = GetComponent<AudioSource>();
       
    }
    private void Update()
    {
        monster.OnIsInBattle_True += OnIsInBattle_True;
        monster.OnIsInBattle_False += OnIsInBattle_False;

    }

    public void OnIsInBattle_True(object sender, System.EventArgs e) 
    {
        audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.battle);
        audioSource.Play();
    }
    public void OnIsInBattle_False(object sender, System.EventArgs e)
    {
        audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.mainTheme);
        audioSource.Play();
    }
}



