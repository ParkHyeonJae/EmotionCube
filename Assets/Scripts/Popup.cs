using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public static bool bIsFinishPopup = false;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        StartCoroutine(Loop());
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    IEnumerator Loop()
    {
        while (gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
