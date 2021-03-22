using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] boxPrefabs;
    public GameObject[] bombPrefabs;
    public GameObject rockPrefab;

    public float minTime;
    public float maxTime;

    void Start()
    {
        initializeFoothold();
        StartCoroutine(SpwanCoroutine());
    }

    void initializeFoothold()
    {
        
        
        int firstX = -2;
        int firstZ = -5;
        int randomValue;
        for (int i = 0; i < 10; i++)
        {
            randomValue = Random.Range(0, 4);
            GameObject spwanObject = boxPrefabs[randomValue];
            
            GameObject go = Instantiate(spwanObject, new Vector3(firstX, 0.3f, firstZ), Quaternion.identity, this.gameObject.transform);
            go.GetComponent<DisturbObjectCtrl>().enabled = false;
            State state = go.GetComponent<State>();
            state.colorState = (EColorState)randomValue;
            BlockSystem.Instance.Append(state);
            firstX++;

            if(firstX > 2)
            {
                firstX = -2;
                firstZ = -6;
            }

            switch(randomValue)
            {
                case 0:
                    Box.Pink++;
                    break;
                case 1:
                    Box.Orange++;
                    break;
                case 2:
                    Box.Blue++;
                    break;
                case 3:
                    Box.Green++;
                    break;
            }
        }

        int[] array = new int[]{Box.Pink, Box.Orange, Box.Blue, Box.Green};

        int max = -1;
        int index = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if(array[i] > max)
            {
                max = array[i];
                index = i;
            }
        }
        BlockSystem.Instance.PopulateState = (EColorState)index;
        BlockSystem.Instance.marbleSystem.SetMarble();
        
    }

    IEnumerator SpwanCoroutine()
    {
        while (!GameManager.Instance.bGameStart)
        {
                yield return null;
        }
        while(true)
        {
            GameObject spwanObject;
            int randomValue = Random.Range(0, 3);
            int randomValue2 = Random.Range(0, 4);
            
            if(randomValue == 0)
            {
                spwanObject = boxPrefabs[randomValue2];
            }
            else if(randomValue == 1)
            {
                spwanObject = rockPrefab;
            }
            else
            {
                spwanObject = bombPrefabs[randomValue2];
            }

            GameObject go = Instantiate(spwanObject, new Vector3(Random.Range(-9, 10), 0.3f, 7.39f), Quaternion.identity, this.gameObject.transform);
            State state = go.GetComponent<State>();
            if(state != null)
            {
                state.colorState = (EColorState)randomValue2;
            }

            
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
