namespace ApartmentBookingApp;

public class IdGeneratorService : IIdGeneratorService
{
    private int _nextHostId = 1;
    private int _nextApartmentId = 1;

    public int GetNextApartmentId() => _nextApartmentId++;
    public int GetNextHostId() => _nextHostId++;
}