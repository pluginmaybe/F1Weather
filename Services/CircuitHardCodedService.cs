
namespace F1Weather.Services;
public class CircuitHardCodedService : ICircuitService
{
    // TODO alternative method of pulling track data into maui app. Other attempts work fine on windows but not android
    readonly List<Circuits> _circuits = [];
    readonly string _circuitText = "1,Bahrain International Circuit,Sakhir,Bahrain,20.0325,50.5106,bahrain,02/03/2024\r\n2,Jeddah Corniche Circuit,Jeddah,Saudi Arabia,21.6319,39.1044,saudi,09/03/2024\r\n3,Albert Park Circuit,Melbourne,Australia,-37.8497,144.9683,australia,24/03/2024\r\n4,Suzuka,Suzuka,Japan,34.8417,136.5389,japan,07/04/2024\r\n5,Shanghai International Circuit,Shanghai,China,31.3389,121.2197,china,21/04/2024\r\n6,Miami International Autodrome,Miami,USA,25.9581,-80.2389,miami,05/05/2024\r\n7,AI Enzo e Dino Ferrari,Imola,Italy,44.3411,11.7133,imola,19/05/2024\r\n8,Circuit de Monaco,Monte Carlo,Monaco,43.7347,7.4206,monaco,26/05/2024\r\n9,Circuit Gilles Villeneuve,Quebec,Canada,45.5006,-73.5225,canada,09/06/2024\r\n10,Circuit de Barcelona-Catalunya,Montmelo,Spain,41.57,2.2611,barcelona,23/06/2024\r\n11,Red Bull Ring,Spielberg,Austria,47.2197,14.7647,austria,30/06/2024\r\n12,Silverstone Circuit,Northamptonshire,Great Britain,52.075,-1.0167,silverstone,07/07/2024\r\n13,Hungaroring,Mogyorod,Hungary,47.5822,19.2511,hungary,21/07/2024\r\n14,Circuit de Spa-Francorchamps,Stavelot,Belgium,50.4372,5.9714,spa,28/07/2024\r\n15,Circuit Zandvoort,Zandvoort,Netherlands,52.3888,4.5409,zandvoort,25/08/2024\r\n16,A.N. de Monza,Monza,Italy,45.6206,9.2894,monza,01/09/2024\r\n17,Baku City Circuit,Baku,Azerbaijan,40.3725,49.8533,baku,15/09/2024\r\n18,Marina Bay Street Circuit,Marina Bay,Singapore,1.2915,103.8639,marinabay,22/09/2024\r\n19,Circuit of the Americas,Austin,USA,30.1328,-97.6411,cota,20/10/2024\r\n20,Autodromo Hermanos Rodriguez,Mexico City,Mexico,19.4061,-99.0925,mexico,27/10/2024\r\n21,Autodromo Jose Carlos Pace,Sao Paulo,Brazil,-23.7011,-46.6972,brazil,03/11/2024\r\n22,Las Vegas Strip Circuit,Las Vegas,USA,36.1099,-115.1622,vegas,23/11/2024\r\n23,Losail/Lusail International,Lusail,Qatar,25.49,51.4542,qatar,01/12/2024\r\n24,Yas Marina Circuit,Abu Dhabi,UAE,24.4672,54.6031,abudhabi,08/12/2024";
    public async Task<List<Circuits>> GetCircuits()
    {
        _circuits.Clear();

        string[] lines = _circuitText.Split("\r\n");
        foreach (string line in lines)
        {
            string[] properties = line.Split(',');
            Circuits circuit = new();

            if (int.TryParse(properties[0], out int n)) circuit.Id = n;
            else break;

            circuit.Name = properties[1];
            circuit.Region = properties[2];
            circuit.Country = properties[3];

            if (double.TryParse(properties[4], out double d)) circuit.Latitude = d;
            if (double.TryParse(properties[5], out double e)) circuit.Longitude = e;

            if (DateTime.TryParse(properties[7], out DateTime f)) circuit.StartTime = f;


            circuit.CircuitImagePath = properties[6] + ".jpg";
            _circuits.Add(circuit);
        }

        return _circuits;
    }
}
