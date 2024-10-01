[System.Serializable]
public class NetworkDisplay
{
    private string masterOrSlave;
    private string masterExtraDelay;
    private string serialPortSender;
    private string serialPortReceiver;
    private int baudrate;

    public NetworkDisplay(string masterOrSlave, string masterExtraDelay, string serialPortSender, string serialPortReceiver, int baudrate)
    {
        this.masterOrSlave = masterOrSlave;
        this.masterExtraDelay = masterExtraDelay;
        this.serialPortSender = serialPortSender;
        this.serialPortReceiver = serialPortReceiver;
        this.baudrate = baudrate;
    }

    public NetworkDisplay() { }

    public string MasterOrSlave
    {
        get { return masterOrSlave; }
        set { masterOrSlave = value; }
    }

    public string MasterExtraDelay
    {
        get { return masterExtraDelay; }
        set { masterExtraDelay = value; }
    }

    public string SerialPortSender
    {
        get { return serialPortSender; }
        set { serialPortSender = value; }
    }

    public string SerialPortReceiver
    {
        get { return serialPortReceiver; }
        set { serialPortReceiver = value; }
    }

    public int Baudrate
    {
        get { return baudrate; }
        set { baudrate = value; }
    }
}
