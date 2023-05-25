using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip;   // 코인 먹는 소리
    public static AudioClip audioClip1;  // 표창 맞는 소리


    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("PP_Collect_Item_1_1");
        audioClip1 = Resources.Load<AudioClip>("PP_Small_Impact_1_5");
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public static void soundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }
    public static void soundPlay1()
    {
        audioSource.PlayOneShot(audioClip1);
    }
}
