using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private static Sound _instance;
    public static Sound Instance
    {
        get {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(Sound)) as Sound;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public AudioSource audioSource;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    

    public void PlayRockSound()
    {
        audioSource.Play();
    }
}
