using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] musics;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("BGM");
        if(musics.Length >= 3 )
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
        
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
