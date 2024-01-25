using OfficeOpenXml;

namespace F1Weather.Services;

public class CircuitService
{
    List<Circuits> _circuitsList = [];
    // ToDo change from local Excel file
    readonly string _file = "C:\\Users\\markl\\Source\\BoardGame\\F1Weather\\Resources\\Raw\\CircuitLocations.xlsx"; 
    FileInfo FileInfo { get; set; }

    public CircuitService()
    {
        FileInfo = new FileInfo(_file);
    }
    
    public async Task<List<Circuits>> GetCircuits()
    {
        if (_circuitsList?.Count > 0)
        {
            return _circuitsList;
        }
        if (!File.Exists(_file))
        {
            return [];
        }

        _circuitsList = await ExcelHelper.LoadExcelFile(FileInfo);
        return _circuitsList;
    }
}
public static class ExcelHelper
{
    public static async Task<List<Circuits>> LoadExcelFile(FileInfo filename)
    {
        List<Circuits> output = [];
        using var package = new ExcelPackage(filename);

        // check for valid file
        await package.LoadAsync(filename);
        var ws = package.Workbook.Worksheets[0];

        // check row is correct to start from data
        int row = 2;
        int col = 1;

        while (string.IsNullOrWhiteSpace(ws.Cells[row,col].Value?.ToString()) == false)
        {
            Circuits c = new();
            if (int.TryParse(ws.Cells[row, col].Value.ToString(), out int n)) c.Id = n; 
            else continue;
            
            c.Name = ws.Cells[row, col + 1].Value.ToString() ?? "";
            c.Region = ws.Cells[row, col + 2].Value.ToString() ?? "";
            c.Country = ws.Cells[row, col + 3].Value.ToString() ?? "";
            
            if (double.TryParse(ws.Cells[row, col + 4].Value.ToString(), out double d)) c.Latitude = d;
            else c.Latitude = 0;
            
            if (double.TryParse(ws.Cells[row, col + 5].Value.ToString(), out d)) c.Longitude = d;
            else c.Longitude = 0;
            
            string circuitImageName = ws.Cells[row, col + 6].Value.ToString() ?? "";
            c.CircuitImagePath = circuitImageName + ".jpg";
            
            if (double.TryParse(ws.Cells[row, col + 7].Value.ToString(), out d)) c.StartTime = DateTime.FromOADate(d);
            else c.StartTime = DateTime.Today;
            
            output.Add(c);
            row++;
        }

        return output;
    }
}

