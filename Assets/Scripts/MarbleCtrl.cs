using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleCtrl : MonoBehaviour
{
    public float radius = 5f;
    public float speed = 2f;
    public float height = 1f;
    public float theta = 0;

    

    private void Update()
    {
        theta += speed * Time.deltaTime;

        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);

        transform.localPosition = new Vector3(x, height, y);
    }

}
