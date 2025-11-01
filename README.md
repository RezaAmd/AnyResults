# AnyResults

Unified result handling & data paging for .NET projects. Fast, simple, and clean.

## Key Features

- **`Result` / `Result<T>`** – Represent success or failure of any operation. Avoid throwing exceptions unnecessarily.
- **PagedResult<T>** – Easily page your IQueryable data. Comes with extensions for LINQ.
- **PaginationQuery** – Specify page number, page size, and sorting for PagedResult.

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
    public Result<int> Create(CreateTodoInputModel todoModel)
    {
        if (todoModel == null)
            return Result.Fail(error: "Todo cannot be null.", data: 400);

        // Store to db.

        return Result.Ok(data: 200).WithMessage(message: "Todo created successfully!");
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
