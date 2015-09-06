using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
//		protected FSMBrain brain;
//		protected Transform tr;
//		protected Rigidbody2D rb;
//
//		void Awake ()
//		{
//				brain = GetComponent<FSMBrain> ();
//				tr = GetComponent<Transform> ();
//				rb = GetComponent<Rigidbody2D> ();
//		}

//		protected void MoveFrom (Vector3 position, float speed)
//		{
//				Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
//				Vector2 dirToMove = pos - new Vector2 (position.x, position.y);
//		
//				dirToMove.Normalize ();
//		
//				rb.MovePosition (pos + dirToMove * speed * Time.deltaTime);
//				var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
//				tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.Euler (0, 0, targetAngle), 2f * Time.deltaTime);
//		}
//
//		protected void MoveTo (Vector3 position, float speed)
//		{
//				Vector2 pos = new Vector2 (position.x, position.y);
//				Vector2 dirToMove = new Vector2 (pos.x, pos.y) - pos;
//		
//				dirToMove.Normalize ();
//		
//				rb.MovePosition (pos + dirToMove * speed * Time.deltaTime);
//				var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
//				tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.Euler (0, 0, targetAngle), 2f * Time.deltaTime);
//		}

}

		