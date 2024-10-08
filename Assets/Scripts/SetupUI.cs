using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetupUI : MonoBehaviour
{

    public DisplaySetup displaySetup;
    public SetupUI setupUI;

    public InputField serialPortReceiver;
    public InputField serialPortSender;
    public InputField baudRate;
    public Dropdown masterOrSlave;
    public InputField masterExtraDelay;

    public InputField fileNameA;
    public InputField fileNameB;
    public InputField fileNameStatic;
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


    // Start is called before the first frame update
    private void OnEnable()
    {
        DisplaySetup loadedData = LoadFromJsonFile<DisplaySetup>("display_data.json");
        if (loadedData != null)
        {
            GetDataFromJson();
        }
    }

    private void GetDataFromJson()
    {
        DisplaySetup loadedData = LoadFromJsonFile<DisplaySetup>("display_data.json");

        serialPortReceiver.text = loadedData.NetworkDisplay.SerialPortReceiver;
        serialPortSender.text = loadedData.NetworkDisplay.SerialPortSender;

        baudRate.text = loadedData.NetworkDisplay.Baudrate.ToString();
        SetDropdownValueByName(masterOrSlave, loadedData.NetworkDisplay.MasterOrSlave);
        masterExtraDelay.text = loadedData.NetworkDisplay.MasterExtraDelay;

        fileNameA.text = loadedData.VideoSettings.FilenameA;
        fileNameB.text = loadedData.VideoSettings.FilenameB;
        fileNameStatic.text = loadedData.VideoSettings.FilenameStatic;
        displayQuantity.text = loadedData.VideoSettings.DisplayQuantity;
        SetDropdownValueByName(position, loadedData.VideoSettings.Position);

        videoSizeW.text = loadedData.VideoSettings.VideoSize[0];
        videoSizeH.text = loadedData.VideoSettings.VideoSize[1];

        pivotX.text = loadedData.VideoSettings.Pivot[0];
        pivotY.text = loadedData.VideoSettings.Pivot[1];

        soundControl.text = loadedData.VideoSettings.SoundControl;
        countPlayed.text = loadedData.VideoSettings.CountPlayed;

        idleTime.text = loadedData.VideoSettings.IdleTime;
        pauseTime.text = loadedData.VideoSettings.PauseTime;
    }

    private T LoadFromJsonFile<T>(string fileName)
    {
        // Define o caminho do arquivo onde queremos carregar
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonData); // Desserializa a string JSON para o objeto
        }
        else
        {
            Debug.LogWarning("Arquivo n�o encontrado: " + path);
            return default(T);
        }
    }

    private void SetDropdownValueByName(Dropdown dropdown, string optionName)
    {
        int optionIndex = -1;

        // Loop through the options to find the index with the specified name
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == optionName)
            {
                optionIndex = i;
                break;
            }
        }

        // Set the value of the Dropdown based on the found index
        if (optionIndex != -1)
        {
            dropdown.value = optionIndex;
        }
        else
        {
            Debug.LogError("Option name not found in the Dropdown!");
        }
    }

}
