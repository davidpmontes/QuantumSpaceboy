public interface ITowable
{
    bool Tractored { get; }
    void StartTractor();
    void StartTow();
    void StopTow();
}
