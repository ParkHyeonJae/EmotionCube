using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    bool isPause;

    public SoundManager soundManager;

    public GameObject settingGo;

    public void Bakc()
    {
        soundManager.PlayButtonSound();
        settingGo.SetActive(!settingGo.activeSelf);
        Pause();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundManager.PlayButtonSound();
            settingGo.SetActive(!settingGo.activeSelf);
            Pause();
        }
    }
    void Pause()
    {
        soundManager.PlayButtonSound();
        Time.timeScale = isPause == false ? 0 : 1;
        isPause = !isPause;
    }
}
