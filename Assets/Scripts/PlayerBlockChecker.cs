using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockChecker : MonoBehaviour
{
    private RaycastHit hit;
    private Box curBox  = null;
    private Box prevBox = null;
    void Update()
    {
        Debug.DrawRay(transform.localPosition, Vector3.down);
        if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 1f))
        {
            if (hit.collider.tag == "Box")
            {
                curBox = hit.collider.transform.GetComponent<Box>();
                if (curBox != prevBox)
                {
                    StartCoroutine(DetectLoop());
                    prevBox = curBox;
                }
            }
        }
    }

    IEnumerator DetectLoop()
    {
        while (gameObject.activeInHierarchy)
        {
            List<GameObject> lists = curBox.GetCollAllDetect();

            for (int i = 0; i < lists.Count; i++)
            {
                lists[i].GetComponent<Box>().IsRecursive = true;
            }
            yield return null;
        }
    }
}
