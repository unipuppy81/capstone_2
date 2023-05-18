using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolum;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolum;
    public int channels;
    AudioSource[] sfxPlayer;
    int channelIndex;

    public enum Sfx { Jump, Down, UI_select1 = 3, UI_select2, GameOver = 5, Start}
    void Awake()
    {
        instance = this;
        Init();

    }

    void Init()
    {
        //배경음 플레이어 
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClip;

        //효과음 플레이어
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];

        for(int index = 0; index < sfxPlayer.Length; index++) 
        {
            sfxPlayer[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayer[index].playOnAwake =false;
            sfxPlayer[index].loop = false;
            sfxPlayer[index].volume = sfxVolum;

        }

    }

    public void PlayBgm(bool isplay)
    {
        if(isplay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for( int index = 0; index < sfxPlayer.Length; index++ ) 
        {
           int loopIndex = (index + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying) continue;

            channelIndex = loopIndex;
            sfxPlayer[0].clip = sfxClip[(int)sfx];
            sfxPlayer[0].Play();

            break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
