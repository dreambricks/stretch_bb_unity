using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

public class ArduinoCommunicationSender : MonoBehaviour
{
    public SerialPort serialPort;
    public string port;
    public int baudrate;
    Thread receiverThread;
    public static LockFreeQueue<string> myQueue;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    void OnEnable()
    {
        DisplaySetup setup = SaveManager.LoadFromJsonFile<DisplaySetup>("display_data.json");

        port = setup.NetworkDisplay.SerialPortSender;
        baudrate = setup.NetworkDisplay.Baudrate;

        serialPort = new SerialPort(port, baudrate);
        serialPort.Open();

        myQueue = new LockFreeQueue<string>();
        receiverThread = new Thread(
            new ThreadStart(ReceiveData));
        receiverThread.IsBackground = true;
        receiverThread.Start();
    }

    private void ReceiveData()
    {
        while (startReceiving)
        {

            if (serialPort.IsOpen)
            {
                try
                {
                    data = serialPort.ReadLine();

                    myQueue.Enqueue(data);

                    if (printToConsole) { print(data); }

                }
                catch (Exception err)
                {
                    print(err.ToString());
                }
            }
        }
    }

    public string GetData()
    {
        if (myQueue.Empty()) return "";

        return myQueue.Dequeue();
    }

    public string GetLastestData()
    {
        string result = "";
        string data = "";
        while ((data = GetData()) != "")
        {
            result = data;
        }

        return result;
    }

    public string GetLastestNewData(float maxAge)
    {
        return GetLastestData();
    }

    public void SendMessageToSlaves(char[] message, int offSet, int count)
    {
        if (serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(message, offSet, count);
                Debug.Log("Sending message to Slaves");
                string smessage = message.ToString();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
