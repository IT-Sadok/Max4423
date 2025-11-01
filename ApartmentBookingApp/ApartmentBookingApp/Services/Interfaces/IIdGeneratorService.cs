namespace ApartmentBookingApp;

public interface IIdGeneratorService
{ 
    int GetNextApartmentId(); 
    int GetNextHostId();
}