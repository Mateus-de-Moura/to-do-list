

namespace tasksWebApi.Application.Dtos
{
    public sealed record GetPagedTaskRequestDto(int PageNumber = 1, int PageSize = 10, string search = null);

}
