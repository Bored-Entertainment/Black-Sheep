using UnityEngine;
using System.Collections;

public class ZebraIdleAI : AIBase
{
		FSMBrain brain;
		Transform tr;
		Rigidbody2D rb;
		private Vector3 barn;
		public float wanderSpeed = 4f;
		public float counter;
		public Vector3 wanderPoint;
		public float wanderRange = 2f;
		public float rotationSpeed = 0.5f;
		private Animator anim;
		private bool wanderPointIsValid;

		void Start ()
		{
				barn = GameObject.FindGameObjectWithTag ("Barn").transform.position;
				brain = GetComponent<FSMBrain> ();
				tr = GetComponent<Transform> ();
				rb = GetComponent<Rigidbody2D> ();
				counter = Random.Range (2f, 7f);
				anim = GetComponentInChildren<Animator> ();

		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Player") {

						Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
						Vector2 playerPos = new Vector2 (other.transform.position.x, other.transform.position.y);
						RaycastHit2D hit = Physics2D.Raycast (pos, playerPos, 20f);
						if (hit.transform.tag == "Player")
								Debug.Log ("ray to player");
						brain.SwitchState (brain.states [2]);

				} else if (other.tag == "Predator") {

						Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
						Vector2 predPos = new Vector2 (other.transform.position.x, other.transform.position.y);
						RaycastHit2D hit = Physics2D.Raycast (pos, predPos, 20f);
						if (hit.transform.tag == "Predator")
								brain.SwitchState (brain.states [1]);
				}
		}

		void MoveTo (Vector3 position, float speed)
		{
				Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
				Vector2 dirToMove = new Vector2 (position.x, position.y) - pos;
		
				dirToMove.Normalize ();
		
				rb.MovePosition (pos + dirToMove * speed * Time.deltaTime);
				var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
				tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.Euler (0, 0, targetAngle), rotationSpeed * Time.deltaTime);
			
		}

		void FixedUpdate ()
		{
				//	MoveTo (barn, wanderSpeed);
				Wander ();
				anim.speed = 0.5f;
		}

		void OnEnable ()
		{
				tr = GetComponent<Transform> ();
				brain = GetComponentInParent<FSMBrain> ();
		}

		private void Wander ()
		{
				counter -= Time.deltaTime;
				if (counter <= 0) {
						counter = Random.Range (2f, 7f);

						//	while (!wanderPointIsValid) {

						wanderPoint = new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), 0);
						wanderPoint = tr.position + (wanderPoint - tr.position).normalized * wanderRange;

						//		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (wanderPoint), Vector2.zero);
						//		if (hit.collider.tag == "Ground") {
						//				wanderPointIsValid = true;
						//				Debug.Log ("Tag: " + hit.collider.tag);
						//		} else {
						//				wanderPointIsValid = false;
						//				Debug.Log ("Tag: " + hit.collider.tag);
						//		}		
						//	}
						
				}

				MoveTo (wanderPoint, wanderSpeed);

		}

		void OnDisable ()
		{
		}
}