namespace Shirehorse.Core.Extensions
{
    public static class DataGridViewExtensions
    {
        public static DataGridViewRow CloneWithValues(this DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (int index = 0; index < row.Cells.Count; index++)
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            return clonedRow;
        }

    }
}
