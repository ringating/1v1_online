using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ReceiveRemotePlayerData : MonoBehaviour
{
    public Rigidbody rb;

    public SendLocalPlayerData connection;

    private Socket socket;
    private byte[] receiveBuffer;
    private Sendables receiveObject;

    void Start()
    {
        receiveBuffer = new byte[Sendables.bytesLength];
        receiveObject = new Sendables(new Vector3());
    }

    void FixedUpdate()
    {
        if (socket != null)
        {
            if (socket.Available > 0)
            {
                try
                {
                    socket.Receive(receiveBuffer);
                }
                catch (SocketException e)
                {
                    print(e);
                }
                receiveObject.SetData(receiveBuffer);
                rb.MovePosition(receiveObject.GetPosition());
            }
        }
    }

    public void UseThisSocket(Socket sock)
    {
        socket = sock;
    }
}
