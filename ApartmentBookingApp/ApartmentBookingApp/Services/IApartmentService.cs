namespace ApartmentBookingApp;

public interface IApartmentService
{
    void AddApartmentToHost();
    void ShowApartmentsByHostId(int hostId);
}