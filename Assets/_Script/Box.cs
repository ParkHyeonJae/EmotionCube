using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

public class Box : MonoBehaviour
{
    public bool IsAppend = false;
    public bool IsRecursive = false;
    public ParticleSystem[] particleSystems;
    public CameraShake cameraShake;

    public static bool DestoyCheck;
    private BoxDetection boxDetection;
    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        boxDetection = new BoxDetection();
    }

    
    [ShowInInspector] public static int Pink;
    [ShowInInspector] public static int Orange;
    [ShowInInspector] public static int Blue;
    [ShowInInspector] public static int Green;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            if(GetComponent<DisturbObjectCtrl>().enabled)
            {
                GetComponent<DisturbObjectCtrl>().enabled = false;
                transform.position = other.transform.position + new Vector3(0, 0, 1);
                BlockSystem.Instance.Append(GetComponent<State>());

                switch(GetComponent<State>().colorState)
                {
                    case EColorState.Pink:
                        Pink++;
                        break;
                    case EColorState.Orange:
                        Orange++;
                        break;
                    case EColorState.Blue:
                        Blue++;
                        break;
                    case EColorState.Green:
                        Green++;
                        break;
                }
                Debug.Log($"Pink : {Pink}, Orange : {Orange}, Blue : {Blue}, Green : {Green}");

                int[] array = new int[]{Pink, Orange, Blue, Green};

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
        }
    }

    void OnDestroy()
    {
        if(transform.position.z > -13f && DestoyCheck == false)
        {
            if(cameraShake != null)
            {
                cameraShake.enabled = true;
            }
            EColorState colorState = GetComponent<State>().colorState;
            GameObject go = Instantiate(particleSystems[(int)colorState], transform.position, Quaternion.identity).gameObject;
            Destroy(go, 3f);

            switch(GetComponent<State>().colorState)
            {
                case EColorState.Pink:
                    Pink--;
                    break;
                case EColorState.Orange:
                    Orange--;
                    break;
                case EColorState.Blue:
                    Blue--;
                    break;
                case EColorState.Green:
                    Green--;
                    break;
            }

            int[] array = new int[]{Pink, Orange, Blue, Green};

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
    }

    void Update()
    {
        IsAppend = false;
        if (boxDetection.IsDetect(transform))
            IsAppend = true;
    }
    
    public List<GameObject> GetCollAllDetect()
    {
        return boxDetection.CollAllDetect(transform);
    }

    public void CallRecursive()
    {
        Debug.Log("CallRecursive");
        List<GameObject> detects = boxDetection.CollAllDetect(transform);

        foreach (var e in detects)
        {
            BlockSystem.Instance.AppendGroup(e);
            IsRecursive = true;


            Debug.Log("detect: " + e.name);
            
        }
    }
}

public class BoxDetection
{
    public bool IsDetect(Transform transform)
    {
        Debug.DrawRay(transform.localPosition, Vector3.forward, Color.green);
        Debug.DrawRay(transform.localPosition, Vector3.left, Color.green);
        Debug.DrawRay(transform.localPosition, Vector3.right, Color.green);
        Debug.DrawRay(transform.localPosition, Vector3.back, Color.green);

        
        RaycastHit hit;
        if (Physics.Raycast(transform.localPosition, Vector3.forward, out hit, 1f)
            || Physics.Raycast(transform.localPosition, Vector3.left, out hit, 1f)
            || Physics.Raycast(transform.localPosition, Vector3.right, out hit, 1f)
            || Physics.Raycast(transform.localPosition, Vector3.back, out hit, 1f)
            || Physics.Raycast(transform.localPosition, Vector3.down, out hit, 1f))
        {
            if (hit.collider.tag == "Box" || hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    public List<GameObject> CollAllDetect(Transform transform)
    {
        List<GameObject> detectList = new List<GameObject>();

        Vector3[] vectors =
        {
             Vector3.forward,
             Vector3.left,
             Vector3.right,
             Vector3.back,
             Vector3.down,
        };
        RaycastHit hit;
        for (int i = 0; i < vectors.Length; i++)
        {
            Debug.DrawRay(transform.localPosition, vectors[i], Color.green);

            if (Physics.Raycast(transform.localPosition, vectors[i], out hit, 1f))
            {
                if (hit.collider.tag == "Box" || hit.collider.tag == "Player")
                {
                    detectList.Add(hit.collider.gameObject);
                }
            }
        }
        return detectList;
    }
}