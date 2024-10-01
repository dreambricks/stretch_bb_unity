using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class GameHandler : MonoBehaviour
{

    public SetupUI setupUI;
    public VideoPlayer player;
    public VideoPlayer playerB;

    public GameObject renderVideo;
    public GameObject renderStatic;


    private bool ativo = false;
    void Awake()
    {
        player.gameObject.SetActive(false);
        playerB.gameObject.SetActive(false);
        renderVideo.SetActive(false);
        renderStatic.SetActive(false);
        setupUI.gameObject.SetActive(false);

        DisplaySetup loadedData = LoadFromJsonFile<DisplaySetup>("display_data.json");

        if (loadedData == null)
        {
            setupUI.gameObject.SetActive(true);
        }
        else
        {
            renderStatic.SetActive(true);
        }

    }

    void Update()
    {
        OpenMenuSettings();
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
            return default(T);
        }
    }

    private void OpenMenuSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ativo = !ativo;
            setupUI.gameObject.SetActive(ativo);
            renderStatic.SetActive(!ativo);
            renderVideo.SetActive(!ativo);
            player.gameObject.SetActive(!ativo);
        }
    }

}
