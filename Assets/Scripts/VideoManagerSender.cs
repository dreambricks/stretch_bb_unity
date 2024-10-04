using UnityEngine;
using UnityEngine.Video;
using System;


public class VideoManagerSender : MonoBehaviour
{
    public VideoPlayer player;
    public VideoPlayer playerB;
    public ArduinoCommunicationReceiver arduinoCommunicationReceiver;
    public ArduinoCommunicationSender arduinoCommunicationSender;
    public GameObject renderStatic;
    public GameObject renderVideoA;
    public GameObject renderVideoB;
    public DateTime lastPlayTime;
    private bool canPlay = true;
    private float masterDelay;
    public float idleTime = 60f;
    public float pauseTime = 20f;
    public char[] dataToSend = { '1', '2' };

    void OnEnable()
    {
        playerB.isLooping = false;
        player.isLooping = false;
        player.loopPointReached += OnVideoFinished;
        playerB.loopPointReached += onVideoBFinished;

        DisplaySetup loadedData = SaveManager.LoadFromJsonFile<DisplaySetup>("display_data.json");
        masterDelay = float.Parse(loadedData.NetworkDisplay.MasterExtraDelay);
        idleTime = float.Parse(loadedData.VideoSettings.IdleTime);
        pauseTime = float.Parse(loadedData.VideoSettings.PauseTime);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        renderStatic.gameObject.SetActive(true);
        renderVideoA.gameObject.SetActive(false);
        renderVideoB.gameObject.SetActive(false);
    }

    void onVideoBFinished(VideoPlayer vp)
    {
        renderStatic.gameObject.SetActive(true);
        renderVideoA.gameObject.SetActive(false);
        renderVideoB.gameObject.SetActive(false);
    }

    void Update()
    {
        PlayVideo();
        PlayVideo2();
        CheckPlay();
        CheckIdle();
    }

    void CheckPlay()
    {
        if ((DateTime.Now - lastPlayTime).TotalSeconds >= pauseTime)
        {
            canPlay = true;
        }
    }

    void CheckIdle()
    {
        if ((DateTime.Now - lastPlayTime).TotalSeconds >= idleTime)
        {
            if (player.isPlaying == false && playerB.isPlaying == false)
            {
                StartPlayVideo(player, "SendMessage1ToSlaves");
                Debug.Log("Playing by Idle time");
            }
        }
    }


    void PlayVideo()
    {
        string data = arduinoCommunicationReceiver.GetLastestNewData(1.0f);
        int value;
        if (int.TryParse(data, out value))
        {
            if (value == 1)
            {
                StartPlayVideo(player, "SendMessage1ToSlaves");
            }
            else if (value == 2)
            {
                StartPlayVideo(playerB, "SendMessage2ToSlaves");
            }
        }
    }

    void PlayVideo2()
    {
        string data = arduinoCommunicationSender.GetLastestNewData(1.0f);
        int value;

        if (int.TryParse(data, out value))
        {
            if (value == 1)
            {
                StartPlayVideo(player, "SendMessage1ToSlaves");
            }
            else if (value == 2)
            {
                StartPlayVideo(playerB, "SendMessage2ToSlaves");
            }
        }
    }

    void StartPlayVideo(VideoPlayer vp, string methodName)
    {
        if (player.isPlaying == false && playerB.isPlaying == false)
        {
            arduinoCommunicationReceiver.GetLastestData();
            if (canPlay)
            {
                vp.time = 0;
                vp.SetDirectAudioMute(0, false);
                if (methodName == "SendMessage1ToSlaves")
                {
                    renderVideoA.gameObject.SetActive(true);
                    renderVideoB.gameObject.SetActive(false);
                }
                else if (methodName == "SendMessage2ToSlaves")
                {
                    renderVideoA.gameObject.SetActive(false);
                    renderVideoB.gameObject.SetActive(true);
                }

                vp.Play();

                lastPlayTime = DateTime.Now;
                canPlay = false;
                Invoke(methodName, masterDelay);
                Debug.Log("Vídeo A reproduzido em: " + lastPlayTime);
            }
        }
    }

    public void SendMessage1ToSlaves()
    {
        arduinoCommunicationSender.SendMessageToSlaves(dataToSend, 0, 1);
        Debug.Log("Sending message to Slaves: 1");

    }

    public void SendMessage2ToSlaves()
    {
        arduinoCommunicationSender.SendMessageToSlaves(dataToSend, 1, 1);
        Debug.Log("Sending message to Slaves: 2");
    }
}
