using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class ConnectionGUI : MonoBehaviour
{
	private bool submitted = false;
	private bool awaitingConnection = false;

	private string remoteIpString = "127.0.0.1";
	private string remotePortString = "0";

	private string hostPortString = "0";

	private Socket socket;
	private EndPoint remoteEndpoint;

	public ReceiveRemotePlayerData receiveScript;
	public SendLocalPlayerData sendScript;

	private void Start()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
		remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
	}

	private void OnGUI()
	{
		if (!submitted) 
		{
			remoteIpString = GUI.TextField(new Rect(10, 10, 200, 20), remoteIpString);
			remotePortString = GUI.TextField(new Rect(10, 35, 200, 20), remotePortString);

			if (GUI.Button(new Rect(10, 60, 200, 20), "Connect"))
			{
				submitted = true;
				socket.Connect(IPAddress.Parse(remoteIpString), int.Parse(remotePortString));
				PassCompletedSocketToScripts();
			}

			GUI.Label(new Rect(10, 85, 200, 20), "or");

			hostPortString = GUI.TextField(new Rect(10, 110, 200, 20), hostPortString);

			if (GUI.Button(new Rect(10, 135, 200, 20), "Host"))
			{
				submitted = true;
				socket.Bind(new IPEndPoint(IPAddress.Any, int.Parse(hostPortString)));
				awaitingConnection = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (awaitingConnection)
		{
			if (socket.Available > 0)
			{
				byte[] receiveBuffer = new byte[1024];
				socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
				socket.Connect(remoteEndpoint);
				PassCompletedSocketToScripts();
			}
		}
	}

	private void PassCompletedSocketToScripts() 
	{
		sendScript.UseThisSocket(socket);
		receiveScript.UseThisSocket(socket);
	}
}
