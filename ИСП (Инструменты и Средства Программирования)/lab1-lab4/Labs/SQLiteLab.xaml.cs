using Labs.Entities;
using Labs.Services;
namespace Labs;

public partial class SQLiteLab : ContentPage
{
    IDbService db;
    IEnumerable<HospitalRoom> hospitalRooms;
    private int selectedHospitalRoomId;
    public SQLiteLab(IDbService dbService)
    {
        InitializeComponent();
        db = dbService;
        db.Init();
        hospitalRooms = db.GetHospitalRooms();
        LoadGroupNames();
    }

    private void groupPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedHospitalRoomId = hospitalRooms.ElementAtOrDefault(groupPicker.SelectedIndex)?.Id ?? 0;
        IEnumerable<Patient> patients = db.GetPatients(selectedHospitalRoomId);

        collectionView.ItemsSource = patients; // Data source for CollectionView
    }

    private void LoadGroupNames()
    {

        IEnumerable<string> rooms = hospitalRooms.Select(room => room.Name).ToList();
        groupPicker.ItemsSource = rooms.ToList();
    }
}