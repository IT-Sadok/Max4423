namespace ApartmentBookingApp;

public interface IOutputWriter
{
    void ShowMenu();
    void ShowHostsList(List<Host> hosts);
    void ShowApartmentsList(Host host);
    void ShowMessage(string message);
    void ShowSuccessMessage(string message);
    void ShowErrorMessage(string message);
}