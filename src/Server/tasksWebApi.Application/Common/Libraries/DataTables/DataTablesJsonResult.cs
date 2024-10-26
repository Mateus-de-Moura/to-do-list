namespace tasksWebApi.Application.Common.Libraries.DataTables
{
    public sealed class DataTablesJsonResult<T>
    {
        public int Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public IEnumerable<T> Data { get; set; }

        public DataTablesJsonResult(int draw, int recordsFiltered, int recordsTotal, IEnumerable<T> data)
        {
            Draw = draw;
            RecordsFiltered = recordsFiltered;
            RecordsTotal = recordsTotal;
            Data = data;
        }
    }
}
