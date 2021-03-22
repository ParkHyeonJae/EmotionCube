using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleEmotion : MonoBehaviour
{
    public EEmotion Emotion = EEmotion.Joy;
    public void Start()
    {
        Emotion = EmotionCtrl.CurMainEmotion;
    }

    
}
