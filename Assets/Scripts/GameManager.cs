using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }
    private static GameManager instance;

    public bool bGameStart = false;

    public ScrollSystem scrollSystem;
    public List<GameObject> StopObjects;
    public GameObject bridge = null;
    public GameObject popup = null;

    public void Start()
    {
        Debug.Assert(scrollSystem != null, "NullReference");
        Once = true;
    }
    private static bool Once = true;
    float scrollSpeed = 0f;
    public void OnEnable()
    {
        popup.SetActive(true);
        scrollSpeed = scrollSystem.ScrollSpeed;
    }
    public void Update()
    {
        if (!bGameStart)
        {
            
            scrollSystem.ScrollSpeed = 0f;
            StopObjects.ForEach(e => e.SetActive(false));
        }
        else
        {
            if (Once)
            {
                scrollSystem.ScrollSpeed = scrollSpeed;
                StopObjects.ForEach(e => e.SetActive(false));
                bridge.AddComponent<DisturbObjectCtrl>();
                Once = false;
            }
        }

    }
}
