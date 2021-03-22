using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource mBackgroundsound;

    public List<AudioSource> SoundEffectList = new List<AudioSource>();

    public Slider BackgroundsoundSlider;
    public Slider SoundEffectSlider;

    public void PlayButtonSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void ChangeBackgroundVolume()
    {
        mBackgroundsound.volume = BackgroundsoundSlider.value;
    }

    public void ChangeSoundEffectsVolume()
    {
        for (int i = 0; i < SoundEffectList.Count; i++)
        {
            SoundEffectList[i].volume = SoundEffectSlider.value;
        }
    }
}
