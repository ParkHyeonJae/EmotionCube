using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.bGameStart = true;
        }
    }
}
