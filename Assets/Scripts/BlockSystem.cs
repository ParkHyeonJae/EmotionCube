using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockSystem : MonoBehaviour
{
    public List<State> BoxStates;
    public List<GameObject> Groups;

    public Dictionary<EColorState, int> stateCount = new Dictionary<EColorState, int>();


    public MarbleSystem marbleSystem;

    public static BlockSystem Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<BlockSystem>();
            return instance;
        }
    }
    private static BlockSystem instance;

    public EColorState PopulateState { get; set; }

    public bool AppendGroup(GameObject obj)
    {
        if (Groups.Contains(obj))
        {
            Groups.Add(obj);
            return true;
        }
        return false;
    }

    public void Append(State state)
    {
        Debug.Log("추가됨 : " + state.colorState);
        BoxStates.Add(state);

        switch (state.colorState)
        {
            case EColorState.Pink:
                stateCount[state.colorState]++;
                break;
            case EColorState.Orange:
                stateCount[state.colorState]++;
                break;
            case EColorState.Blue:
                stateCount[state.colorState]++;
                break;
            case EColorState.Green:
                stateCount[state.colorState]++;
                break;
            default:
                break;
        }
        state.gameObject.GetComponent<Box>().IsAppend = true;
    }
    public void Remove(State state)
    {
        if (BoxStates.Contains(state))
        {
            Debug.Log("삭제됨 : " + state.colorState);
            BoxStates.Remove(state);

            switch (state.colorState)
            {
                case EColorState.Pink:
                    stateCount[state.colorState]--;
                    break;
                case EColorState.Orange:
                    stateCount[state.colorState]--;
                    break;
                case EColorState.Blue:
                    stateCount[state.colorState]--;
                    break;
                case EColorState.Green:
                    stateCount[state.colorState]--;
                    break;
                default:
                    break;
            }
            state.gameObject.GetComponent<Box>().IsAppend = false;
        }
    }
    private void OnEnable()
    {
        stateCount.Add(EColorState.Pink, 0);
        stateCount.Add(EColorState.Orange, 0);
        stateCount.Add(EColorState.Blue, 0);
        stateCount.Add(EColorState.Green, 0);
        StartCoroutine(BlockLoop());
    }

    IEnumerator BlockLoop()
    {
        while (gameObject.activeInHierarchy)
        {
            var query = stateCount.OrderByDescending(x => x.Value);
            KeyValuePair<EColorState, int> l;
            foreach (var v in query)
            {
                l = v;
                break;
            }
            PopulateState = l.Key;
            yield return null;
        }
    }
}
