using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

public class Server : MonoBehaviour {
    public Thread mThread;
    String command; 
    // Use this for initialization
    void Start () {
        Debug.Log("HELLO0");

        ThreadStart ts = new ThreadStart(Update1);
        mThread = new Thread(ts);
        mThread.Start();
    }

    // Update is called once per frame
    void Update () {
        command = Input.inputString;
      
      
    }

    void Update1 ()
    {
        Debug.Log("HELLO");
        TcpListener server = null;
        try
        {

            Debug.Log("HELLO2");

            // Set the TcpListener on port 13000.
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("192.168.0.155"); //CHANGE THIS 

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Thread.Sleep(10); 
                Debug.Log("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Debug.Log("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Debug.Log("Received: " + data);

                    // Process the data sent by the client.
                    data = data.ToUpper();


                    
                    while(command.Length == 0)
                    {
                        //wait ...  
                    }
                    string text = command;
                    Debug.Log("Sent: " + command);
                    command = ""; 



                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(text);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                }
                

                // Shutdown and end connection
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }

    }
}
