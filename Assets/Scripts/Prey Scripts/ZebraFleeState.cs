using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZebraFleeState : AIBase
{
    private List<GameObject> nearbyPredators;
    public float fleeSpeed = 5f;
    public float fleeDuration = 300f;
    FSMBrain brain;
    Transform tr;
    Rigidbody2D rb;
    private AudioSource[] audio;
    bool isFleeing;
    Vector3 threatPos;
    public float fadeRate = 0.2f;
    public GameObject blood;
    private bool isDying;



    void Awake()
    {
        nearbyPredators = new List<GameObject>();
        brain = GetComponent<FSMBrain>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponents<AudioSource>();

    }


    void MoveFrom(Vector3 position, float speed)
    {
        if (!isDying)
        {
            Vector2 pos = new Vector2(tr.position.x, tr.position.y);
            Vector2 dirToMove = pos - new Vector2(position.x, position.y);

            dirToMove.Normalize();

            rb.MovePosition(pos + dirToMove * speed * Time.deltaTime);
            var targetAngle = Mathf.Atan2(dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
            tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.Euler(0, 0, targetAngle), 2f * Time.deltaTime);
        }
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


    void OnEnable()
    {
        nearbyPredators = new List<GameObject>();


        //				tr = GetComponent<Transform> ();
        //				brain = GetComponent<FSMBrain> ();
        //				rb = GetComponent<Rigidbody2D> ();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Predator")
        {
            audio[Random.Range(0, 2)].Play();
            Vector2 pos = new Vector2(tr.position.x, tr.position.y);
            Vector2 predPos = new Vector2(other.transform.position.x, other.transform.position.y);
            isFleeing = true;
            threatPos = other.transform.position;
            StartCoroutine(Flee(other.transform.position));
        }
        else if (other.tag == "Player")
        {
            audio[Random.Range(3, 4)].Play();
        }
    }

    private IEnumerator Flee(Vector3 predPos)
    {
        float timer = fleeDuration;
        while (true)
        {
            MoveFrom(threatPos, fleeSpeed);
            timer -= Time.deltaTime;
            if (timer < 0)
                yield break;
            yield return null;
        }

        brain.SwitchState(brain.states[0]);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Predator" && nearbyPredators.Contains(other.gameObject))
        {
            nearbyPredators.Remove(other.gameObject);
        }
    }

    public void Die()
    {
        if (!isDying)
            StartCoroutine(DieCo());
    }

    private IEnumerator DieCo()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        isDying = true;
        rb.mass = 100000;
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetInteger("AnimState", 1);
        Instantiate(blood, tr.position, tr.rotation);
        Vector3 pos = tr.position;
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        Color col = sprite.material.color;
        float r = col.r;
        float g = col.g;
        float b = col.b;
        float a = col.a;
        while (a >= 0)
        {
            a -= fadeRate * Time.deltaTime;
            sprite.material.color = new Color(r, g, b, a);

            tr.position = pos;
            yield return null;
        }
        GameDataScript.totalSheep--;
        if (GameDataScript.totalSheep <= 0)
            Application.LoadLevel("Game Over Level");

        Destroy(gameObject);

    }

    void OnDisable()
    {
    }
}
//	void FixedUpdate ()
//		{
//				if (nearbyPredators.Count > 0) {
//						Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
//						Vector3 averagePredatorPosition = Vector3.zero;
//
//						for (int i = 0; i < nearbyPredators.Count; i++) {
//
//								Vector2 predPos = new Vector2 (nearbyPredators [i].transform.position.x, nearbyPredators [i].transform.position.y);
//
//								if (Physics2D.Raycast (pos, predPos, 50f)) {
//										averagePredatorPosition += nearbyPredators [i].transform.position;
//								}
//						}
//						if (nearbyPredators.Count > 1)
//								averagePredatorPosition /= nearbyPredators.Count;
//
//						Vector2 dirToMove = pos - new Vector2 (averagePredatorPosition.x, averagePredatorPosition.y);
//		
//						dirToMove.Normalize ();
//		
//						rb.MovePosition (pos + dirToMove * fleeSpeed * Time.deltaTime);
//						var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
//						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), 2f * Time.deltaTime);
//				}

//	brain.SwitchState (brain.states [0]);
//	}}