using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;

public class SendLocalPlayerData : MonoBehaviour
{
    public Rigidbody rb;

    public string hostIP;
    public int hostPort;

    private UdpClient client;
    private Sendables data;
    private byte[] toSend;

    public const int SIO_UDP_CONNRESET = -1744830452;

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
        if (client == null) CreateSocketInstance();
        //client.Connect(IPAddress.Parse(ipAddressString), port);
        data = new Sendables(rb.position);
        if (client == null) print("socket is null");
    }

    void FixedUpdate()
    {
        data.SetData(rb.position);
        toSend = data.ToByteArray();
        int sentBytes = client.Send(toSend, toSend.Length);
        //print(sentBytes + " bytes sent");
    }

    private void CreateSocketInstance()
    {
        try
        {
            client = new UdpClient();
            client.Connect(IPAddress.Parse(hostIP), hostPort);
            //client.Client.IOControl((IOControlCode)SIO_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null); //https://stackoverflow.com/a/39440399
        }
        catch (Exception e)
        {
            print(e);
        }
    }

	public UdpClient getUdpClient()
	{
		if (client == null) CreateSocketInstance();
		return client;
	}
}
