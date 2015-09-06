using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntSensorScript : MonoBehaviour
{
		internal List<GameObject> prey;

		void Awake ()
		{
				//	prey = GetComponentInParent<PredatorHuntScript> ().prey;
				prey = new List<GameObject> ();
		}


		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Prey" && !prey.Contains (other.gameObject)) {
						Debug.Log ("found sheep");
						prey.Add (other.gameObject);
				}
		}
	
		void OnTriggerExit2D (Collider2D other)
		{
				if (prey.Contains (other.gameObject)) {
						Debug.Log ("lost sheep");
						prey.Remove (other.gameObject);
				}
		}
}