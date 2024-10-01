
[System.Serializable]
public class VideoSettings
{
    private string filenameA;
    private string filenameB;
    private string filenameStatic;
    private string displayQuantity;
    private string position;
    private string[] videoSize;
    private string[] pivot;
    private string soundControl;
    private string countPlayed;

    public VideoSettings()
    {
    }

    public VideoSettings(string filenameA, string filenameB, string filenameStatic, string position, string[] videoSize, string[] pivot, string displayQuantity, string soundControl, string countPlayed)
    {
        this.filenameA = filenameA;
        this.filenameB = filenameB;
        this.filenameStatic = filenameStatic;
        this.position = position;
        this.videoSize = videoSize;
        this.pivot = pivot;
        this.displayQuantity = displayQuantity;
        this.soundControl = soundControl;
        this.countPlayed = countPlayed;
    }

    public string FilenameA { get => filenameA; set => filenameA = value; }
    public string FilenameB { get => filenameB; set => filenameB = value; }
    public string FilenameStatic { get => filenameStatic; set => filenameStatic = value; }
    public string Position { get => position; set => position = value; }
    public string[] VideoSize { get => videoSize; set => videoSize = value; }
    public string[] Pivot { get => pivot; set => pivot = value; }
    public string DisplayQuantity { get => displayQuantity; set => displayQuantity = value; }
    public string SoundControl { get => soundControl; set => soundControl = value; }
    public string CountPlayed { get => countPlayed; set => countPlayed = value; }
}
