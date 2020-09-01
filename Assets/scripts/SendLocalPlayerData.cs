using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;

public class SendLocalPlayerData : MonoBehaviour
{
    public Rigidbody rb;

    public string remoteAddress = "0.0.0.0";
    public int remotePort = 6969;

    private Socket socket;
    private Sendables data;

    public class Sendables 
    {
        private Vector3 pos;

        private byte[] sendableBytes;
        public static int bytesLength = sizeof(float) * 3;

        public Sendables(Vector3 pos) 
        {
            sendableBytes = new byte[bytesLength];
            SetData(pos);
        }

        public Sendables(byte[] bytes)
        {
            sendableBytes = new byte[bytesLength];
            SetData(bytes);
        }

        public void SetData(Vector3 pos) 
        {
            this.pos.x = pos.x;
            this.pos.y = pos.y;
            this.pos.z = pos.z;
        }

        public void SetData(byte[] bytes)
        {
            pos.x = BitConverter.ToSingle(bytes, 0);
            pos.y = BitConverter.ToSingle(bytes, sizeof(float));
            pos.z = BitConverter.ToSingle(bytes, sizeof(float) * 2);
        }

        public byte[] ToByteArray() 
        {
            CopyBytesToLargerArray(BitConverter.GetBytes(pos.x), sendableBytes, 0);
            CopyBytesToLargerArray(BitConverter.GetBytes(pos.y), sendableBytes, sizeof(float));
            CopyBytesToLargerArray(BitConverter.GetBytes(pos.z), sendableBytes, sizeof(float)*2);

            return sendableBytes;
        }

        private void CopyBytesToLargerArray(byte[] fromArr, byte[] toArr, int copyStartIndex)
        {
            for (int i = 0; i < fromArr.Length; ++i) 
            {
                toArr[copyStartIndex + i] = fromArr[i];
            }
        }

        public int GetLengthOfSendableBytes()
        {
            return sendableBytes.Length;
        }

        public Vector3 GetPosition()
        {
            return pos;
        }
    }

    void Start()
    {
        if (socket == null) CreateSocketInstance();
        socket.Connect(remoteAddress, remotePort);
        data = new Sendables(rb.position);
        if (socket == null) print("socket is null");
    }

    void FixedUpdate()
    {
        data.SetData(rb.position);
        int sentBytes = socket.Send(data.ToByteArray());
        print(sentBytes + " bytes sent");
    }

    private void CreateSocketInstance()
    {
        try
        {
			socket = new Socket(SocketType.Dgram, ProtocolType.IP)
			{
				Blocking = false
			};
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
            //socket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
		}
        catch (SocketException e)
        {
            print(e);
        }
        
    }

	public Socket getSocket()
	{
		if (socket == null) CreateSocketInstance();
		return socket;
	}
}
