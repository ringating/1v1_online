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
    private SendLocalPlayerData.Sendables receiveObject;

    void Start()
    {
        socket = connection.getSocket();
        receiveBuffer = new byte[SendLocalPlayerData.Sendables.bytesLength];
        receiveObject = new SendLocalPlayerData.Sendables(new Vector3());
    }

    void FixedUpdate()
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
