using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class JeepSteerScript : MonoBehaviour
{

    Rigidbody2D rb;
    Transform tr;
    private float turnSpeed = 1f;
    public float turnSpeedRatio = 1f;
    public float maxSpeed, acceleration, maxReverseSpeed;
    public float currentSpeed;
    public float pitchRatio;
    private AudioSource audio;
    private Animator anim;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        currentSpeed = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        tr = GetComponent<Transform>();

    }

    void FixedUpdate()
    {

        //     rb.gravityScale = 0;
        turnSpeed = currentSpeed / turnSpeedRatio;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.MoveRotation(rigidbody2D.rotation + turnSpeed * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.MoveRotation(rigidbody2D.rotation - turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration;

            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (currentSpeed > -maxReverseSpeed)
            {
                currentSpeed -= acceleration;

            }
        }
        else
        {
            if (currentSpeed < 0)
                currentSpeed += acceleration;
            else if (currentSpeed > 0)
                currentSpeed -= acceleration;
        }
        //audio.pitch = 
        Move(tr.TransformDirection(new Vector2(0, 1)), currentSpeed);
    }

    private void RotateTowards(Vector3 dir)
    {
        Quaternion rot = tr.rotation;
        Quaternion toTarget = Quaternion.LookRotation(dir);
        rot = Quaternion.Slerp(rot, toTarget, turnSpeed * Time.deltaTime);
        Vector3 euler = rot.eulerAngles;
        euler.z = 0;
        euler.x = 0;
        rot = Quaternion.Euler(euler);
        tr.rotation = rot;
    }

    private void Move(Vector3 dir, float currentSpeed)
    {
        dir *= currentSpeed * Time.deltaTime;
        rb.MovePosition(new Vector2(tr.position.x + dir.x, tr.position.y + dir.y));
    }
}