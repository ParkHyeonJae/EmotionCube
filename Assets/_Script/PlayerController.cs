using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PositionAdjuster outLine;
    [SerializeField] float mSpeed;
    Rigidbody mRigidbody;
    Vector3 mVelocity;
    public Animator animator;

    public ParticleSystem foot;

    public float tempf;

    void Start()
    {
        mSpeed /= 3.1f;
        outLine.SetPosition(new Vector3(transform.position.x, outLine.transform.position.y, transform.position.z + 1));
        mRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        if (moveDirX != 0)
        {
            if (moveDirX > 0)
            {
                outLine.SetPosition(new Vector3(transform.position.x + 1, outLine.transform.position.y, transform.position.z));
                transform.GetChild(0).transform.rotation =  Quaternion.Euler(0f, 90f, 0f);
            }
            else
            {
                outLine.SetPosition(new Vector3(transform.position.x - 1, outLine.transform.position.y, transform.position.z));
                transform.GetChild(0).transform.rotation =  Quaternion.Euler(0f, -90f, 0f);
            }
            moveDirZ = 0;
            
        }
        else
        {
            if (moveDirZ == 0)
            {

            }
            else if (moveDirZ > 0)
            {
                outLine.SetPosition(new Vector3(transform.position.x, outLine.transform.position.y, transform.position.z + 1));
                transform.GetChild(0).transform.rotation =  Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                outLine.SetPosition(new Vector3(transform.position.x, outLine.transform.position.y, transform.position.z - 1));
                transform.GetChild(0).transform.rotation =  Quaternion.Euler(0f, 180f, 0f);
            }
            moveDirX = 0;
        }

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        mVelocity = (moveHorizontal + moveVertical).normalized;

        if (transform.position.y < 0.7f)
        {
            mVelocity *= 0.6f;
        }
        mRigidbody.MovePosition(transform.position + mVelocity * Time.deltaTime * mSpeed);

        float temp = Mathf.Abs(moveDirX)  + Mathf.Abs(moveDirZ);
        animator.SetFloat("Move", temp);
        if(temp > 0)
        {
            tempf -= Time.deltaTime;
            if(tempf < 0)
            {
                tempf = 0.1f;
                foot.Play();
            }
            
        }

        
    }

}
