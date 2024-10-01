using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoCommunicationSender : MonoBehaviour
{
    public SerialPort serialPort;
    public string port;
    public int baudrate;

    void OnEnable()
    {
        DisplaySetup setup = SaveManager.LoadFromJsonFile<DisplaySetup>("display_data.json");

        port = setup.NetworkDisplay.SerialPortSender;
        baudrate = setup.NetworkDisplay.Baudrate;

        serialPort = new SerialPort(port, baudrate);
        serialPort.Open();
    }

    public void SendMessageToSlaves(string message)
    {
        if (serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(message);

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
