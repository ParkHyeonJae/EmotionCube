using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 방해 오브젝트 컨트롤러
/// </summary>
/// 
[RequireComponent(typeof(Rigidbody))]
public class DisturbObjectCtrl : MonoBehaviour
{
    public float mSpeed = 1f;
    private readonly string mDeleteTag = "DeleteLine";

    void Update()
    {
        transform.localPosition += Vector3.back * mSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(mDeleteTag))
            Destroy(gameObject);
    }
}