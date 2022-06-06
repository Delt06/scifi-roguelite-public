namespace Maps
{
    public interface IGateService
    {
        void Open(Gate gate);
        void Refresh(Gate gate, string guid);
    }
}