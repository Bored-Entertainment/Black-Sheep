using UnityEngine;
using System.Collections;

public class ZebraHerdedAI : AIBase
{

    Transform jeep;

    public float herdSpeed = 5f;
    FSMBrain brain;
    Transform tr;
    Rigidbody2D rb;

    void MoveFrom(Vector3 position, float speed)
    {
        Vector2 pos = new Vector2(tr.position.x, tr.position.y);
        Vector2 dirToMove = pos - new Vector2(position.x, position.y);

        dirToMove.Normalize();

        rb.MovePosition(pos + dirToMove * speed * Time.deltaTime);
        var targetAngle = Mathf.Atan2(dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.Euler(0, 0, targetAngle), 2f * Time.deltaTime);
    }

    void MoveTo(Vector3 position, float speed)
    {
        Vector2 pos = new Vector2(position.x, position.y);
        Vector2 dirToMove = new Vector2(pos.x, pos.y) - pos;

        dirToMove.Normalize();

        rb.MovePosition(pos + dirToMove * speed * Time.deltaTime);
        var targetAngle = Mathf.Atan2(dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.Euler(0, 0, targetAngle), 2f * Time.deltaTime);
    }

    void Awake()
    {
        jeep = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        brain = GetComponent<FSMBrain>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Predator")
        {
            brain.SwitchState(brain.states[1]);
        }
    }

    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            yield return new WaitForSeconds(3f);
            brain.SwitchState(brain.states[0]);
        }
    }

    void OnEnable()
    {
        //		brain = GetComponentInParent<FSMBrain> ();
        jeep = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
    }

    void FixedUpdate()
    {
        MoveFrom(jeep.position, herdSpeed);
    }
}