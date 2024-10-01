
[System.Serializable]
public class DisplaySetup
{
    private NetworkDisplay networkDisplay;
    private VideoSettings videoSettings;

    public DisplaySetup()
    {
    }

    public DisplaySetup(NetworkDisplay networkDisplay, VideoSettings videoSettings)
    {
        this.NetworkDisplay = networkDisplay;
        this.VideoSettings = videoSettings;
    }

    public NetworkDisplay NetworkDisplay { get => networkDisplay; set => networkDisplay = value; }
    public VideoSettings VideoSettings { get => videoSettings; set => videoSettings = value; }

}
