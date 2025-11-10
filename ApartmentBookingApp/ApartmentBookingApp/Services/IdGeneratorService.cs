namespace ApartmentBookingApp;

public class IdGeneratorService : IIdGeneratorService
{
    private int _nextHostId = 1;
    private int _nextApartmentId = 1;

    public void Initialize(int maxHostId, int maxApartmentId)
    {
        _nextHostId = maxHostId + 1;

        _nextApartmentId = maxApartmentId + 1;
    }
    public int GetNextApartmentId() => _nextApartmentId++;
    public int GetNextHostId() => _nextHostId++;
}