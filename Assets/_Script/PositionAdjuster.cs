using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionAdjuster : MonoBehaviour
{
    public GameObject tempGo;

    public bool isLifting;
    public bool IsBoxExist;

    public MeshRenderer meshRenderer;
    public Material Green;
    public Material Red;

    public PlayerController playerController;

    public AudioSource audioSource;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isLifting && !IsBoxExist)
            {
                playerController.animator.SetTrigger("DownBox");
                StopCoroutine("Lift");
                tempGo.transform.position = transform.position + new Vector3(0f, -0.54f, 0f);
                isLifting = false;
                audioSource.Play();
                return;
            }

            if(tempGo != null)
            {
                if(isLifting == false)
                {
                    playerController.animator.SetTrigger("Catch");
                    isLifting = true;
                    StartCoroutine("Lift");
                    audioSource.Play();
                }
            }
            
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box")) 
        {
            if(isLifting == false)
            {
                tempGo = other.gameObject;
            }
            else
            {
                IsBoxExist = true;
                meshRenderer.material = Red;
            }
        }  
    }

    void OnTriggerExit(Collider other)
    {
        if(isLifting == false)
        {
            tempGo = null;
        }
        else
        {
            IsBoxExist = false;
            meshRenderer.material = Green;
        }
    }

    IEnumerator Lift()
    {
        while(true)
        {
            tempGo.transform.position = playerController.transform.position + new Vector3(0f, 1.4f, 0f);
            yield return null;
        }
    }

}
