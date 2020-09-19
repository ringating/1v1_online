using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;

public class SendLocalPlayerData : MonoBehaviour
{
    public Rigidbody rb;
    public GroundedCheck groundedScript;

    public string remoteAddress = "0.0.0.0";
    public int remotePort = 6969;

    private Socket socket;
    private Sendables data;

    void Start()
    {
        data = new Sendables(rb.position, groundedScript.grounded);
    }

    void FixedUpdate()
    {
        data.SetData(rb.position, groundedScript.grounded);
        if (socket != null)
        {
            socket.Send(data.ToByteArray());
        }
    }

    public void UseThisSocket(Socket sock)
    {
        socket = sock;
    }
}
