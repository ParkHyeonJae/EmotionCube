using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float mSpeed = 1f;

    private float mStartPositionZ;
    private float mEndLimitPositionZ;

    private void OnEnable()
    {
        StartCoroutine(ScrollLoop());
    }

    public void SetProperty(float mStartPosition, float mEndLimitPosition)
    {
        this.mStartPositionZ = mStartPosition;
        this.mEndLimitPositionZ = mEndLimitPosition;
    }

    public void SetScrollSpeed(ref float mSpeed)
    {
        this.mSpeed = mSpeed;
    }
    public bool IsOverScroll()
    {
        if (transform.localPosition.z < mEndLimitPositionZ)
            return true;
        return false;
    }
    IEnumerator ScrollLoop()
    {
        yield return new WaitForEndOfFrame();
        while (gameObject.activeInHierarchy)
        {
            if (IsOverScroll())
                transform.localPosition = mStartPositionZ * Vector3.forward;
            transform.localPosition += Vector3.back * mSpeed * Time.deltaTime;
            yield return null;
        }
    }


}
