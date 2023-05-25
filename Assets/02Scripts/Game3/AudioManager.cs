using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        audioClip= Resources.Load<AudioClip>("PP_Collect_Item_1_1");
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public static void soundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
