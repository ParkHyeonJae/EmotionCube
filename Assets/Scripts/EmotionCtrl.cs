using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum EEmotion
{
    /// <summary>
    /// 희 : 기쁨 : 핑크
    /// </summary>
    Joy,
    /// <summary>
    /// 노 : 노여움 : 주황
    /// </summary>
    Anger,
    /// <summary>
    /// 애 : 슬픔 : 파랑
    /// </summary>
    Sadness,
    /// <summary>
    /// 락 : 즐거움 : 초록
    /// </summary>
    Pleasure,
}
[System.Serializable]
public struct EmotionColor
{
    public Color Joy;
    public Color Anger;
    public Color Sadness;
    public Color Pleasure;
}

[System.Serializable]
public struct EmotionUI
{
    public Image EmotionalChange;
    public Sprite Joy;
    public Sprite Anger;
    public Sprite Sadness;
    public Sprite Pleasure;
}

public class EmotionCtrl : MonoBehaviour
{
    public static EEmotion CurMainEmotion = EEmotion.Joy;
    [Range(1, 120)]
    public float ChangeEmotionTime = 60f;
    public EmotionColor emotionColor;
    public EmotionUI emotionUI;

    public Renderer waterRenderer;

    private void Awake()
    {
        Debug.Assert(waterRenderer != null, "NullReference");
        ChangeRandomEmotion();
    }
    private void OnEnable()
    {
        StartCoroutine(ChangedEmotionLoop());
    }
    public void ChangeRandomEmotion()
    {
        CurMainEmotion = (EEmotion)Random.Range(0, 4);
        Change();
    }
    IEnumerator ChangedEmotionLoop()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(ChangeEmotionTime);
            ChangeRandomEmotion();
        }
    }
    Color _stateColor;
    Sprite _stateSprite;

    Color temp;

    bool check;
    void Change()
    {
        switch (CurMainEmotion)
        {
            case EEmotion.Joy:
                _stateColor = emotionColor.Joy;
                _stateSprite = emotionUI.Joy;
                break;
            case EEmotion.Anger:
                _stateColor = emotionColor.Anger;
                _stateSprite = emotionUI.Anger;
                break;
            case EEmotion.Sadness:
                _stateColor = emotionColor.Sadness;
                _stateSprite = emotionUI.Sadness;
                break;
            case EEmotion.Pleasure:
                _stateColor = emotionColor.Pleasure;
                _stateSprite = emotionUI.Pleasure;
                break;
            default:
                break;
        }
        if (waterRenderer)
        {

            if(check == false)
            {
                waterRenderer.material.SetColor("Color_73393C31", _stateColor);
                temp = _stateColor;
                check = true;
            }
            else
            {
                StartCoroutine(ChangeColor());
            }

        }
        if (emotionUI.EmotionalChange)
            emotionUI.EmotionalChange.sprite = _stateSprite;
    }

    IEnumerator ChangeColor()
    {
        var time = new WaitForSeconds(0.01f);
        int num = 100;
        while(num > 0)
        {
            waterRenderer.material.SetColor("Color_73393C31", Color.Lerp(temp, _stateColor, 1 - num * 0.01f));
            num--;
            yield return time;
        }
        temp = _stateColor;
        
    }

}
