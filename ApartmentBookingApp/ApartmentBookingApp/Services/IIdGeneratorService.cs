namespace ApartmentBookingApp;

public interface IIdGeneratorService
{
    void Initialize(int maxHostId, int maxApartmentId);
    int GetNextApartmentId(); 
    int GetNextHostId();
    
}