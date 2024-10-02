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
    private string idleTime;
    private string pauseTime;

    public VideoSettings()
    {

    }

    public VideoSettings(string filenameA, string filenameB, string filenameStatic, string displayQuantity,
                         string position, string[] videoSize, string[] pivot, string soundControl,
                         string countPlayed, string idleTime, string pauseTime)
    {
        this.filenameA = filenameA;
        this.filenameB = filenameB;
        this.filenameStatic = filenameStatic;
        this.displayQuantity = displayQuantity;
        this.position = position;
        this.videoSize = videoSize;
        this.pivot = pivot;
        this.soundControl = soundControl;
        this.countPlayed = countPlayed;
        this.idleTime = idleTime;
        this.pauseTime = pauseTime;
    }

    // Getters and Setters
    public string FilenameA
    {
        get { return filenameA; }
        set { filenameA = value; }
    }

    public string FilenameB
    {
        get { return filenameB; }
        set { filenameB = value; }
    }

    public string FilenameStatic
    {
        get { return filenameStatic; }
        set { filenameStatic = value; }
    }

    public string DisplayQuantity
    {
        get { return displayQuantity; }
        set { displayQuantity = value; }
    }

    public string Position
    {
        get { return position; }
        set { position = value; }
    }

    public string[] VideoSize
    {
        get { return videoSize; }
        set { videoSize = value; }
    }

    public string[] Pivot
    {
        get { return pivot; }
        set { pivot = value; }
    }

    public string SoundControl
    {
        get { return soundControl; }
        set { soundControl = value; }
    }

    public string CountPlayed
    {
        get { return countPlayed; }
        set { countPlayed = value; }
    }

    public string IdleTime
    {
        get { return idleTime; }
        set { idleTime = value; }
    }

    public string PauseTime
    {
        get { return pauseTime; }
        set { pauseTime = value; }
    }
}
