using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace tasksWebApi.Application.Common.Libraries.DataTables
{
    /// <summary>
    /// Model Binder for DataTablesParameterModel (DataTables)
    /// </summary>
    public class DataTablesModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;

            // Retrieve request data
            var draw = Convert.ToInt32(request.Form["draw"]);
            var start = Convert.ToInt32(request.Form["start"]);
            var length = Convert.ToInt32(request.Form["length"]);
            // Search
            var search = new DataTablesSearch
            {
                Value = request.Form["search[value]"],
                Regex = Convert.ToBoolean(request.Form["search[regex]"])
            };
            // Order
            var orderIndex = 0;
            var order = new List<DataTablesOrder>();

            while (request.Form["order[" + orderIndex + "][column]"].FirstOrDefault() != null)
            {
                order.Add(new DataTablesOrder
                {
                    Column = Convert.ToInt32(request.Form["order[" + orderIndex + "][column]"]),
                    Dir = request.Form["order[" + orderIndex + "][dir]"]
                });

                orderIndex++;
            }

            // Columns
            var columnIndex = 0;
            var columns = new List<DataTablesColumn>();

            while (request.Form["columns[" + columnIndex + "][name]"].FirstOrDefault() != null)
            {
                columns.Add(new DataTablesColumn
                {
                    Data = request.Form["columns[" + columnIndex + "][data]"],
                    Name = request.Form["columns[" + columnIndex + "][name]"],
                    Orderable = Convert.ToBoolean(request.Form["columns[" + columnIndex + "][orderable]"]),
                    Searchable = Convert.ToBoolean(request.Form["columns[" + columnIndex + "][searchable]"]),
                    Search = new DataTablesSearch
                    {
                        Value = request.Form["columns[" + columnIndex + "][search][value]"],
                        Regex = Convert.ToBoolean(request.Form["columns[" + columnIndex + "][search][regex]"])
                    }
                });

                columnIndex++;
            }

            var model = new DataTablesParameterModel
            {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order,
                Columns = columns
            };

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
