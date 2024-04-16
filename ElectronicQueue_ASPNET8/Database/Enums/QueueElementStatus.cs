namespace ElectronicQueue.Database.Models.Enums
{
    public enum QueueElementStatus : short
    {
        None = 0,
        Called = 10,
        Processing = 20,
        Processed = 30,
        Canceled = 40,
    }
}