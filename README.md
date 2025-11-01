<a href="https://www.nuget.org/packages/AnyResults/" target="_blank"><img src="https://img.shields.io/nuget/v/AnyResults.svg" alt="NuGet" /></a>
<a href="https://www.nuget.org/packages/AnyResults" target="_blank"><img src="https://img.shields.io/nuget/dt/AnyResults"/></a>
# AnyResults

Unified result handling & data paging for .NET projects. Fast, simple, and clean.

## Key Features

- **`Result` / `Result<T>`** – Represent success or failure of any operation. Avoid throwing exceptions unnecessarily.
- **`PagedResult<T>`** – Easily page your IQueryable data. Comes with extensions for LINQ.
- **`PaginationQuery`** – Specify page number, page size, and sorting for PagedResult.

```bash
dotnet add package AnyResults
```

## Examples
### 1. Basic Result:
```csharp
var result = Result.Ok().WithMessage("Operation successful");

if (result.IsSuccess)
{
    Console.Write(result.Data); // "Operation successful"
}
```
### 2. Generic Result:
```csharp
User user = new() { Name = "Tom" };

var result = Result.Ok(user);

if (result.IsSuccess)
{
    Console.Write(result.Data.Name); // Tom
}
```
- **`Result` / `Result<T>`** – Represent success or failure of any operation. Supports implicit conversion between `Result` and `Result<T>` to simplify handling generic and non-generic results.
```csharp
    public Result<Todo> Update(Guid id, UpdateTodoInputModel todoModel)
    {
        if (todoModel == null)
            return Result.Fail("Todo cannot be null."); // <-- Result

        Todo todo = FindById(id);
        try
        {
            // update database command.
        }
        catch (Exception ex)
        {
            return Result.Fail(ex); // <-- Result
        }

        return Result.Ok(todo).WithMessage(message: "Todo created successfully!"); // <-- Result
    }
```


### Fail:
```csharp
var result = Result.Fail("An error was occurred!");

if (result.IsSuccess == false)
{
   Console.WriteLine(result.Messages[0]); // An error was occurred!
}
```
