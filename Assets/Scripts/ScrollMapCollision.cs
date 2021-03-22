using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMapCollision : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Disturb") || other.CompareTag("Untagged"))
            Destroy(other.gameObject);
    }
}
