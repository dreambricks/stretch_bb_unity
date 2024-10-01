using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArduino : MonoBehaviour
{
    [SerializeField] private ArduinoCommunicationSender arduinoCommunicationSender;


    public void SendTest()
    {
        arduinoCommunicationSender.SendMessageToSlaves("1");
        Debug.Log("aa");
    }

}
