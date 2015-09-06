using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FarmhouseScript : MonoBehaviour
{

		public Text scoreText;
		AudioSource audio;

		void Start ()
		{
				audio = GetComponent<AudioSource> ();
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "PreySafeTrigger") {
						if (other.GetComponentInParent<SheepColorScript> ().isBlack)
								GameDataScript.score *= 2;
						else
								GameDataScript.score++;
						audio.Play ();
						Destroy (other.transform.parent.gameObject);
						scoreText.text = "Score: " + GameDataScript.score.ToString ();
						GameDataScript.totalSheep--;
						if (GameDataScript.totalSheep <= 0)
								Application.LoadLevel ("Game Over Level");
				}
		}
}