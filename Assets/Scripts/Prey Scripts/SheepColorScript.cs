using UnityEngine;
using System.Collections;

public class SheepColorScript : MonoBehaviour
{
		internal bool isBlack;

		void Start ()
		{
				if (isBlack) {
						GetComponentInChildren<SpriteRenderer> ().color = Color.black;
				}
		}
}
