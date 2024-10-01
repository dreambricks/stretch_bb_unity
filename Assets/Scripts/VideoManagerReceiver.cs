using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

public class VideoManagerReceiver : MonoBehaviour
{
    public VideoPlayer player;
    public VideoPlayer playerB;
    public ArduinoCommunicationReceiver arduinoCommunicationReceiver;
    public GameObject renderStatic;
    public GameObject renderVideoA;
    public GameObject renderVideoB;
    private DateTime lastPlayTime;
    private bool canPlay = true;

    void OnEnable()
    {
        playerB.isLooping = false;
        player.isLooping = false;
        player.loopPointReached += OnVideoFinished;
        playerB.loopPointReached += onVideoBFinished;
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

        if ((DateTime.Now - lastPlayTime).TotalSeconds >= 20)
        {
            canPlay = true;
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
                if (player.isPlaying == false && playerB.isPlaying == false)
                {
                    arduinoCommunicationReceiver.GetLastestData();
                    if (canPlay)
                    {
                        player.time = 0;
                        player.SetDirectAudioMute(0, false);
                        renderVideoA.gameObject.SetActive(true);
                        renderVideoB.gameObject.SetActive(false);
                        player.Play();

                        lastPlayTime = DateTime.Now;
                        canPlay = false;
                        Debug.Log("Vídeo A reproduzido em: " + lastPlayTime);
                    }

                }
            }
            else if (value == 2)
            {
                if (player.isPlaying == false && playerB.isPlaying == false)
                {
                    arduinoCommunicationReceiver.GetLastestData();
                    if (canPlay)
                    {
                        playerB.time = 0;
                        playerB.SetDirectAudioMute(0, false);
                        renderVideoA.gameObject.SetActive(false);
                        renderVideoB.gameObject.SetActive(true);
                        playerB.Play();

                        lastPlayTime = DateTime.Now;
                        canPlay = false;
                        Debug.Log("Vídeo A reproduzido em: " + lastPlayTime);
                    }

                }
            }
        }
    }
}
