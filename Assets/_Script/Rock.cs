using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    bool check;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            BlockSystem.Instance.Remove(other.GetComponent<State>());
            Sound.Instance.PlayRockSound();
            Destroy(other.gameObject);
            if(check)
            {
                Destroy(this.gameObject);
            }
            check = true;
        }
    }
}
