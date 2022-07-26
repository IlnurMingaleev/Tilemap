using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance
    {
        get
        {
            if (instance == null) 
            { 
               instance = Instantiate(Resources.Load<GameAssets>("Prefabs/GameAssets")); 
            }
            return instance;
        }
    }

    public SoundAudioClip[] soundAudioClips;

    /*public SoundAudioClip[] SoundAudioClips 
    {
        get 
        {
            return soundAudioClips;
        }
    }*/

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

        /*public SoundManager.Sound Sound
        {
            get 
            {
                return sound;
            }
        }

        public AudioClip AudioClip 
        {
            get 
            {
                return audioClip;
            }
        }*/
    }
}
