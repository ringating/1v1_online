using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
	public bool grounded = false;

	private void OnTriggerStay(Collider other)
	{
		grounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		grounded = false;
	}
}
