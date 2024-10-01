using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoManagerSender : MonoBehaviour
{
    public VideoPlayer player;
    public VideoPlayer playerB;
    private float masterDelay;
    private bool isMessageSent;
    private bool isPlaying;
    private bool prevIsPlaying;

    DateTime timePassed;
    DateTime playStartTime;
    public ArduinoCommunicationSender arduinoCommunicationSender;
    public ArduinoCommunicationReceiver arduinoCommunicationReceiver;


    void OnEnable()
    {
        Debug.Log("OnEnable");
        DisplaySetup loadedData = SaveManager.LoadFromJsonFile<DisplaySetup>("display_data.json");
        masterDelay = float.Parse(loadedData.NetworkDisplay.MasterExtraDelay);

        isMessageSent = false;
        isPlaying = false;

        Debug.Log("Master Delay: " + masterDelay);

    }

    void Update()
    {
        PlayVideo();
    }

    void PlayVideo()
    {

        if (!isMessageSent && player.isPlaying == false && isPlaying == false)
        {
            arduinoCommunicationSender.SendMessageToSlaves("1");
            timePassed = DateTime.Now;
            isMessageSent = true;

            Debug.Log("Enviou a mensagem: " + player.isPlaying);
        }

        if ((DateTime.Now - timePassed).TotalSeconds >= masterDelay && isMessageSent && !isPlaying)
        {

            player.SetDirectAudioMute(0, true);
            player.Play();
            isMessageSent = false;
            isPlaying = true;
            Debug.Log("Play: " + player.isPlaying);
        }

        if (player.isPlaying == false && prevIsPlaying == true)
        {
            Debug.Log("Over");
            Debug.Log((DateTime.Now - playStartTime).TotalSeconds);
            if ((DateTime.Now - playStartTime).TotalSeconds > 2)
            {
                isPlaying = false;
                Debug.Log("isPlaying = false");
            }
        }

        if (player.isPlaying == true && prevIsPlaying == false)
        {
            playStartTime = DateTime.Now;
            Debug.Log("Started");
        }

        prevIsPlaying = player.isPlaying;
    }
}