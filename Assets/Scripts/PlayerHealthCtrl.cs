using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HPUnit
{
    public Image mHealthImage = null;
    public float mHP = 100f;
    public float mMaxHP { get; set; }

    public void SetHealth(float amount) { mHP = amount; }
    public float GetHealth() { return mHP; }

    public void TakeDamage(float amount) { mHP -= amount; }
}

public class PlayerHealthCtrl : MonoBehaviour
{
    public HPUnit mHUD;

    public static System.Action OnPlayerDie = delegate { };

    private PlayerHealthSimulate mPlayerHealthSimulate;
    private PlayerHealthBehaviour mHealthBehavior;
    private void Awake()
    {
        mHUD.mMaxHP = mHUD.mHP;
        mPlayerHealthSimulate = new PlayerHealthSimulate();
        mHealthBehavior = new PlayerHealthBehaviour(mHUD);

        Debug.Assert(mHUD.mHealthImage != null, "NullReference");
    }
    private void OnEnable()
    {
        OnPlayerDie += () => {
            Debug.Log("Player is Dead");
            Box.DestoyCheck = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
    }
    private void OnDisable()
    {
        OnPlayerDie = delegate { };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mPlayerHealthSimulate.Hit(other))
        {
            OnPlayerDie();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (mPlayerHealthSimulate.Hit(collision))
        {
            OnPlayerDie();
        }
    }
    private void Update()
    {
        mHealthBehavior.Behave();
    }
}
public class PlayerHealthBehaviour
{
    private HPUnit mHUD = null;

    public PlayerHealthBehaviour(HPUnit mHUD)
    {
        this.mHUD = mHUD;
    }
    public bool IsPlayerDead()
    {
        if (mHUD.GetHealth() <= 0)
            return true;
        return false;
    }
    public bool IsDifferentEmotion()
    {
        //Debug.Log($"populate : {BlockSystem.Instance.PopulateState}, Main : {EmotionCtrl.CurMainEmotion}");
        return (int)BlockSystem.Instance.PopulateState != (int)EmotionCtrl.CurMainEmotion;
    }

    public void Behave()
    {
        if (IsPlayerDead())
            PlayerHealthCtrl.OnPlayerDie();
        if (IsDifferentEmotion())
        {
            if (GameManager.Instance.bGameStart)
                mHUD.TakeDamage(0.01f);
        }
//        Debug.Log($"PopulateColor : {BlockSystem.Instance.PopulateState}, MainColor : {EmotionCtrl.CurMainEmotion}, Result : {IsDifferentEmotion()}");


        mHUD.mHealthImage.fillAmount = mHUD.mHP / mHUD.mMaxHP;
    }
}

public class PlayerHealthSimulate
{
    public readonly string mDisturbTag = "Disturb";
    public readonly string mFloorTag = "Floor";

    private Transform mTargetTransfrom = null;
    public PlayerHealthSimulate()
    {

    }

    public bool Hit(Collision collision)
    {
        mTargetTransfrom = collision.transform;
        if (mTargetTransfrom.CompareTag(mDisturbTag) || mTargetTransfrom.CompareTag(mFloorTag))
        {
            return true;
        }
        return false;
    }
    public bool Hit(Collider collision)
    {
        mTargetTransfrom = collision.transform;
        if (mTargetTransfrom.CompareTag(mDisturbTag) || mTargetTransfrom.CompareTag(mFloorTag))
        {
            return true;
        }
        return false;
    }
}
