using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public Vector2 newPivot;
    RectTransform rectTransform;


    private void OnEnable()
    {

        rectTransform = GetComponent<RectTransform>();

        DisplaySetup loadedData = LoadFromJsonFile<DisplaySetup>("display_data.json");
        newPivot = new Vector2(float.Parse(loadedData.VideoSettings.Pivot[0]), float.Parse(loadedData.VideoSettings.Pivot[1]));
        rectTransform.pivot = newPivot;

    }

    private T LoadFromJsonFile<T>(string fileName)
    {

        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            Debug.LogWarning("Arquivo n�o encontrado: " + path);
            return default(T);
        }
    }
}
