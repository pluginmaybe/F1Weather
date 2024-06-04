namespace F1Weather.Services;
public class CircuitCSVService : ICircuitService
{
    readonly List<Circuits> _circuitsList = [];
    readonly string _circuitCSVfile = "circuits.txt";

    public async Task<List<Circuits>> GetCircuits()
    {
        // Attempted to find and use csv/text file in Resources/Raw
        // May be a Maui issue ?
        _circuitsList.Clear();

        string _ = await LoadCircuitCSV();


        return _circuitsList;
    }

    async Task<string> LoadCircuitCSV()
    {
        using Stream fs = await FileSystem.OpenAppPackageFileAsync(_circuitCSVfile);
        using StreamReader sr = new(fs);
        return await sr.ReadToEndAsync();
    }


    void MapToCircuit(string line)
    {
        string[] props = line.Split([',']);

        Circuits circuit = new();

        if (int.TryParse(props[0], out int n)) circuit.Id = n;
        else return;

        circuit.Name = props[1];
        circuit.Region = props[2];
        circuit.Country = props[3];

        if (double.TryParse(props[4], out double d)) circuit.Latitude = d;
        if (double.TryParse(props[5], out double e)) circuit.Longitude = e;

        if (DateTime.TryParse(props[6], out DateTime f)) circuit.StartTime = f;

        circuit.CircuitImagePath = props[7] + ".jpg";
        _circuitsList.Add(circuit);
    }


}
