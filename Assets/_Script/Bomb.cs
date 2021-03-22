using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] BoxCollider eventCollider;
    public ParticleSystem particle;
    bool isCheck;
    [SerializeField] float fristPostionZ;

    public AudioSource audioSource;
    public AudioSource boxDestorySource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            if(other.GetComponent<DisturbObjectCtrl>().enabled == false)
            {
                if(isCheck == false)
                {
                    isCheck = true;
                    Invoke("disableCollider", 0.2f);
                    if(Mathf.Abs(other.transform.position.x - transform.position.x) < 0.5f || other.GetComponent<State>().colorState != GetComponent<State>().colorState)
                    {
                        GameObject go = Instantiate(particle, transform.position, Quaternion.identity).gameObject;
                        audioSource.Play();
                        Destroy(go, 3f);
                    }
                }

                if(Mathf.Abs(other.transform.position.x - transform.position.x) < 0.5f || other.GetComponent<State>().colorState != GetComponent<State>().colorState)
                {
                    boxDestorySource.Play();
                    if(other.gameObject.GetComponent<DisturbObjectCtrl>().enabled == false)
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
    }

    void disableCollider()
    {
        eventCollider.enabled = false;
    }
}
