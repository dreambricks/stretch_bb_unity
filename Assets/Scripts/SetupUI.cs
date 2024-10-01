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

    public InputField serialPort;
    public InputField baudRate;
    public Dropdown masterOrSlave;
    public InputField masterExtraDelay;

    public InputField fileName;
    public InputField displayQuantity;
    public Dropdown position;
    public InputField videoSizeW;
    public InputField videoSizeH;
    public InputField pivotX;
    public InputField pivotY;
    public InputField soundControl;
    public InputField countPlayed;


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

        serialPort.text = loadedData.NetworkDisplay.SerialPortReceiver;
        
        baudRate.text = loadedData.NetworkDisplay.Baudrate.ToString();
        SetDropdownValueByName(masterOrSlave, loadedData.NetworkDisplay.MasterOrSlave);
        masterExtraDelay.text = loadedData.NetworkDisplay.MasterExtraDelay;

        fileName.text = loadedData.VideoSettings.Filename;
        displayQuantity.text = loadedData.VideoSettings.DisplayQuantity;
        SetDropdownValueByName(position, loadedData.VideoSettings.Position);

        videoSizeW.text = loadedData.VideoSettings.VideoSize[0];
        videoSizeH.text = loadedData.VideoSettings.VideoSize[1];

        pivotX.text = loadedData.VideoSettings.Pivot[0];
        pivotY.text = loadedData.VideoSettings.Pivot[1];

        soundControl.text = loadedData.VideoSettings.SoundControl;
        countPlayed.text = loadedData.VideoSettings.CountPlayed;
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
