using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ReceiveRemotePlayerData : MonoBehaviour
{
    public Rigidbody rb;

    public SendLocalPlayerData sendingScript;

    private UdpClient client;
    private byte[] receiveBytes;
    private IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
    private SendLocalPlayerData.Sendables receiveObject;

    void Start()
    {
        client = new UdpClient();
        client.Connect(IPAddress.Parse(sendingScript.ipAddressString), sendingScript.port);
        client.Client.Blocking = false;
        //receiveBytes = new byte[SendLocalPlayerData.Sendables.bytesLength];
        receiveObject = new SendLocalPlayerData.Sendables(new Vector3());
    }

    void FixedUpdate()
    {
        //print("client.Available = " + client.Available);
        if (false)
        {
            try
            {
                receiveBytes = client.Receive(ref remoteIpEndPoint);
            }
            catch (SocketException e)
            {
                print(e);
            }
            receiveObject.SetData(receiveBytes);
            rb.MovePosition(receiveObject.GetPosition());
        }
        else
        {
            try { receiveBytes = client.Receive(ref remoteIpEndPoint); } catch (SocketException e) { }
            if (receiveBytes != null) { print(receiveBytes); } 
        }
    }
}
