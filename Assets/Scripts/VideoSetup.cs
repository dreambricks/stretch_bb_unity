using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoSetup : MonoBehaviour
{
    public VideoPlayer playerA;
    public VideoPlayer playerB;

    public VideoManagerSender videoManagerSender;
    public VideoManagerReceiver videoManagerReceiver;

    public ArduinoCommunicationReceiver arduinoCommunicationReceiver;
    public ArduinoCommunicationSender arduinoCommunicationSender;

    private void OnEnable()
    {
        DisplaySetup loadedData = SaveManager.LoadFromJsonFile<DisplaySetup>("display_data.json");
        if (loadedData == null || loadedData.VideoSettings.FilenameA == "" || loadedData.VideoSettings.FilenameA == null)
        {
            string videoFile = Path.Combine(Application.streamingAssetsPath, "Video/default.mp4");
            playerA.url = videoFile;
        }
        else
        {
            playerA.url = loadedData.VideoSettings.FilenameA;
            playerB.url = loadedData.VideoSettings.FilenameB;
            playerA.gameObject.SetActive(true);
            playerB.gameObject.SetActive(true);
        }



        string masterOrSlave = loadedData.NetworkDisplay.MasterOrSlave;
        if (masterOrSlave == "Slave")
        {
            videoManagerSender.enabled = false;
            arduinoCommunicationSender.gameObject.SetActive(false);
            videoManagerReceiver.enabled = true;
            arduinoCommunicationReceiver.gameObject.SetActive(true);
        }
        else if (masterOrSlave == "Master")
        {
            videoManagerReceiver.enabled = false;
            arduinoCommunicationReceiver.gameObject.SetActive(false);
            videoManagerSender.enabled = true;
            arduinoCommunicationSender.gameObject.SetActive(true);
        }
    }
}
