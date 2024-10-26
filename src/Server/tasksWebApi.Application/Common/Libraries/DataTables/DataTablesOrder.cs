namespace tasksWebApi.Application.Common.Libraries.DataTables
{
    /// <summary>
    /// Represents a column and it's order direction
    /// </summary>
    public sealed class DataTablesOrder
    {
        /// <summary>
        /// Column to which ordering should be applied. This is an index reference to the 
        /// columns array of information that is also submitted to the server
        /// </summary>
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
