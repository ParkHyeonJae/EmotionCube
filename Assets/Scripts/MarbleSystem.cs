using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSystem : MonoBehaviour
{
    public List<GameObject> MarblesPrefabs;
    public List<GameObject> Marbles;

    private void OnEnable()
    {
        foreach (var marble in MarblesPrefabs)
        {
            Marbles.Add(Instantiate(marble, transform));
        }
    }

    public void SetMarble()
    {
        Debug.Log(BlockSystem.Instance.PopulateState);
        for (int i = 0; i < Marbles.Count; i++)
        {
            if ((int)BlockSystem.Instance.PopulateState == i)
                Marbles[i].SetActive(true);
            else Marbles[i].SetActive(false);
        }
    }
}
