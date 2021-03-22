using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSystem : MonoBehaviour
{
    public List<GameObject> ScrollMaps;

    private List<Scroll> ScrollPool = new List<Scroll>();
    private List<float> dists = new List<float>();

    public Transform StartScrollTransform;
    public Transform LimitScrollTransform;

    public float ScrollSpeed = 1f;
    public int ScrollCount = 2;
    private void OnEnable()
    {
        if (ScrollMaps.Count < 2)
            Debug.LogError("Scroll Map Element is small");
        if (ScrollCount > ScrollMaps.Count)
            Debug.LogError("Scroll Map Index is exceed");


        float _scrollDistance = LimitScrollTransform.localPosition.z + Vector3.Distance(StartScrollTransform.localPosition, LimitScrollTransform.localPosition);

        float _dist = 0f;
        float _maxDist = 0f;
        for (int i = 0; i < ScrollCount - 1; i++)
            _maxDist += _scrollDistance;

        foreach (var map in ScrollMaps)
        {
            GameObject _inst = Instantiate(map, transform, true);
            Scroll scroll = _inst.GetComponent<Scroll>();
            scroll.SetProperty(_maxDist, LimitScrollTransform.localPosition.z);
            scroll.SetScrollSpeed(ref ScrollSpeed);

            _inst.SetActive(false);

            ScrollPool.Add(scroll);
        }


        
        dists.Add(0);
        GetScroll();
        for (int i = 0; i < ScrollCount - 1; i++)
        {
            _dist += _scrollDistance;
            dists.Add(_dist);
            GameObject scrollObject = GetScroll();
            scrollObject.transform.localPosition = Vector3.forward * (scrollObject.transform.localPosition.z + _dist);
        }

    }

    public void Update()
    {
        ScrollPool.ForEach(e =>
        {
            e.SetScrollSpeed(ref ScrollSpeed);
        });
    }
    public GameObject GetScroll()
    {
        GameObject _gameObject = null;
        for (int i = 0; i < ScrollPool.Count; i++)
        {
            if (ScrollPool[i].gameObject.activeInHierarchy == false)
            {
                _gameObject = ScrollPool[i].gameObject;

                _gameObject.SetActive(true);
                return _gameObject;
            }
        }
        return _gameObject;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < dists.Count; i++)
        {
            Gizmos.DrawCube(Vector3.forward * dists[i], Vector3.one);
        }
        
    }
#endif
}
