using System.IO;
using System.Data;
using ExcelDataReader;
using System.Text;

public class LoadSpreadsheet
{
    public DataTableCollection Load(string path)
    {
        ExcelReaderConfiguration conf = new ExcelReaderConfiguration();
        conf.FallbackEncoding = Encoding.UTF8;

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream, conf))
            {
                var result = reader.AsDataSet();

                return result.Tables;
            }
        }
    }
}

