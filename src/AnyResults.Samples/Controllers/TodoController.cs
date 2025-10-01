using AnyResults.Pagination;
using AnyResults.Results;

namespace AnyResults.Samples.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TodoViewModel>), 200)]
    public async Task<IActionResult> Get([FromQuery] PaginationQuery page, CancellationToken ct)
    {
        try
        {
            var todoList = await context.TodoList
                .ToPagedResultAsync(
                    page: page,
                    selector: t =>
                    new TodoViewModel(t.Id,
                        t.Title,
                        t.CompletedOn,
                        t.CreatedOn), ct);

            return Ok(todoList);
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<CreateTodoResponseModel>), 200)]
    public async Task<IActionResult> Create([FromBody] CreateTodoInputModel todoModel, CancellationToken ct)
    {
        try
        {
            // create new todo.
            var todo = new TodoEntity
            {
                Title = todoModel.Title
            };

            // store new todo into db.
            await context.TodoList.AddAsync(todo, ct);
            await context.SaveChangesAsync(ct);

            // prepare response model.
            var response = new CreateTodoResponseModel(todo.Id, todo.Title);

            return Ok(Result.Ok(response));
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }
}
