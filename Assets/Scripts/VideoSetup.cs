using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSetup : MonoBehaviour
{
    public VideoPlayer playerA;
    public VideoPlayer playerB;
    public RawImage rawImage;

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
            LoadImageFromLocalPath(loadedData.VideoSettings.FilenameStatic);
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
            videoManagerReceiver.enabled = true;
            arduinoCommunicationReceiver.gameObject.SetActive(true);
            videoManagerSender.enabled = true;
            arduinoCommunicationSender.gameObject.SetActive(true);
        }
    }

    public void LoadImageFromLocalPath(string path)
    {
        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(2, 2);

            if (texture.LoadImage(fileData))
            {
                rawImage.texture = texture;
            }
            else
            {
                Debug.LogError("Falha ao carregar a imagem em uma textura.");
            }
        }
        else
        {
            Debug.LogError("O caminho da imagem não foi encontrado: " + path);
        }
    }
}
