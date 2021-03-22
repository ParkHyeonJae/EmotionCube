using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;


    private float mCurTime = 0f;

    private void Awake()
    {
        Debug.Assert(TimerText != null, "NullReference");
        mCurTime = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.bGameStart)
            mCurTime += Time.deltaTime;
        TimerText.text = this.ToString();
    }

    public override string ToString()
    {
        return string.Format("{0}", ((int)mCurTime).ToString());
    }
}
