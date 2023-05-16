using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVoulum : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer BGMMixer;
    public AudioMixer SPXMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider spxSlider;

    // Start is called before the first frame update
    public void MasterControl()
    {
        float sound = masterSlider.value;

        if (sound == -40f) masterMixer.SetFloat("Master", -80);
        else masterMixer.SetFloat("Master", sound);

    }
    public void BGMControl()
    {
        float sound = bgmSlider.value;

        if (sound == -40f) BGMMixer.SetFloat("BGM", -80);
        else BGMMixer.SetFloat("BGM", sound);

    }
    public void SPXControl()
    {
        float sound = spxSlider.value;

        if (sound == -40f) SPXMixer.SetFloat("SPX", -80);
        else SPXMixer.SetFloat("SPX", sound);

    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

}
