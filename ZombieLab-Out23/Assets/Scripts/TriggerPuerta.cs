using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
	public Animator Puerta;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player"){
			Puerta.SetBool ("Entrando", true);
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player"){
			Puerta.SetBool ("Entrando", false);
		}
	}
}
