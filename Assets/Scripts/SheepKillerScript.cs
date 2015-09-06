using UnityEngine;
using System.Collections;

public class SheepKillerScript : MonoBehaviour
{
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "PreySafeTrigger") {
						other.GetComponentInParent<ZebraFleeState> ().Die ();
				}
		}
}