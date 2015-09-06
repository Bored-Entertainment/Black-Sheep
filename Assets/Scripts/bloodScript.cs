using UnityEngine;
using System.Collections;

public class bloodScript : MonoBehaviour
{
		public float spreadSpeed = 0.5f;

		void Start ()
		{
				GetComponent<Animator> ().speed = spreadSpeed;
		}
}