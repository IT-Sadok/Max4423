namespace ApartmentBookingApp;

public interface IIdGeneratorService
{ 
    int GetNextApartamentId(); 
    int GetNextHostId();
}