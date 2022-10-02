using System.Data;
using System.Text;

namespace Shirehorse.Core.Extensions
{
    public static class DataTableExtensions
    {
        public static void ExportToCSV(this DataTable table, string path)
        {
            StringBuilder sb = new StringBuilder();

            var headers = table
                .Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName);

            sb.AppendLine(string.Join(",", headers));

            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray.Select(value => value.ToString());
                sb.AppendLine(string.Join(",", values));
            }

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, sb.ToString());
        }
    }
}
