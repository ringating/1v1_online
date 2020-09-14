using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionGUI : MonoBehaviour
{

	private string remoteIpString = "127.0.0.1";
	private string remotePortString = "0";

	private string hostPortString = "0";

	private void OnGUI()
	{
		remoteIpString = GUI.TextField(new Rect(10, 10, 200, 20), remoteIpString);
		remotePortString = GUI.TextField(new Rect(10, 35, 200, 20), remotePortString);

		if (GUI.Button(new Rect(10, 60, 200, 20), "Connect"))
		{

		}

		GUI.Label(new Rect(10, 85, 200, 20), "or");

		hostPortString = GUI.TextField(new Rect(10, 110, 200, 20), hostPortString);

		if (GUI.Button(new Rect(10, 135, 200, 20), "Host"))
		{

		}
	}
}
