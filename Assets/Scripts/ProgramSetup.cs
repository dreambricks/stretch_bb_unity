using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ProgramSetup : MonoBehaviour
{

    public ArduinoCommunicationReceiver arduinoCommunicationReceiver;
    public ArduinoCommunicationSender arduinoCommunicationSender;

    public DisplaySetup displaySetup;
    public GameObject player;
    public SetupUI setupUI;
    [SerializeField] private GameObject render;

    public InputField serialPortReceiver;
    public InputField serialPortSender;
    public InputField baudRate;
    public Dropdown masterOrSlave;
    public InputField masterExtraDelay;

    public InputField fileNameA;
    public InputField fileNameB;
    public InputField filenameStatic;
    public InputField displayQuantity;
    public Dropdown position;
    public InputField videoSizeW;
    public InputField videoSizeH;
    public InputField pivotX;
    public InputField pivotY;
    public InputField soundControl;
    public InputField countPlayed;
    public InputField idleTime;
    public InputField pauseTime;


    public void SaveSettings()
    {
        DisplaySetup displaySetup = new DisplaySetup();
        NetworkDisplay networkDisplay = new NetworkDisplay();
        VideoSettings videoSettings = new VideoSettings();


        networkDisplay.SerialPortReceiver = (serialPortReceiver.text == "") ? "COM4" : serialPortReceiver.text;
        networkDisplay.SerialPortSender = (serialPortSender.text == "") ? "COM7" : serialPortSender.text;
        networkDisplay.Baudrate = (baudRate.text == "") ? 9600 : int.Parse(baudRate.text);
        int selectedIndex = masterOrSlave.value;
        networkDisplay.MasterOrSlave = masterOrSlave.options[selectedIndex].text;
        networkDisplay.MasterExtraDelay = (masterExtraDelay.text == "") ? "0" : masterExtraDelay.text;
        displaySetup.NetworkDisplay = networkDisplay;

        videoSettings.FilenameB = fileNameB.text;
        videoSettings.FilenameA = fileNameA.text;
        videoSettings.FilenameStatic = filenameStatic.text;
        videoSettings.DisplayQuantity = (displayQuantity.text == "") ? "0" : displayQuantity.text;
        int positionIndex = position.value;
        videoSettings.Position = position.options[positionIndex].text;
        videoSettings.VideoSize = new string[2];
        videoSettings.VideoSize[0] = (videoSizeW.text == "") ? "1080" : videoSizeW.text;
        videoSettings.VideoSize[1] = (videoSizeH.text == "") ? "1920" : videoSizeH.text;
        videoSettings.Pivot = new string[2];
        videoSettings.Pivot[0] = (pivotX.text == "") ? "0" : pivotX.text;
        videoSettings.Pivot[1] = (pivotY.text == "") ? "0" : pivotY.text;
        videoSettings.SoundControl = (soundControl.text == "") ? "false" : soundControl.text;
        videoSettings.CountPlayed = (countPlayed.text == "") ? "3" : countPlayed.text;
        videoSettings.IdleTime = (idleTime.text == "") ? "300" : idleTime.text;
        videoSettings.PauseTime = (pauseTime.text == "") ? "20" : pauseTime.text;
        displaySetup.VideoSettings = videoSettings;


        if (arduinoCommunicationReceiver.serialPort != null)
        {
            if (arduinoCommunicationReceiver.serialPort.IsOpen)
            {
                Debug.Log("Desabilitando Receiver");
                arduinoCommunicationReceiver.serialPort.Close();
            }
        }

        if (arduinoCommunicationSender.serialPort != null)
        {
            if (arduinoCommunicationSender.serialPort.IsOpen)
            {
                Debug.Log("Desabilitando Sender");
                arduinoCommunicationSender.serialPort.Close();
            }
        }


        SaveToJsonFile(displaySetup, "display_data.json");

        player.SetActive(true);
        render.SetActive(true);

        SceneManager.LoadScene("QuadSceneTests");


    }


    private void SaveToJsonFile<T>(T data, string fileName)
    {
        string jsonData = JsonConvert.SerializeObject(data); // Converte o objeto para uma string JSON

        // Define o caminho do arquivo onde queremos salvar
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        // Salva o arquivo em disco
        File.WriteAllText(path, jsonData);

        Debug.Log("Objeto salvo como JSON em: " + path);
    }


}
