# GenericHierarchyService
## lists all parents and children of tree structures

In ASP .NET Core projects, it extracts all relationships of tree-structured lists with their up and down degrees.

- ASP.NET Core
## Features

- A recursive method was written to find the relationships of the sent list.
- For any list you can view the result as parent and child list

## To the developer
Example Usage


```sh
public async Task<List<HierarchyResult>> GetAllOrganizationWithParent()
{
    var organizations = await _context.Set<Organization>().ToListAsync();
    var organizationResults = HierarchyService<Organization>.GetHierarchyResults(organizations);
    return organizationResults;
}
```
You can use the GetHierarchyResults method on a generic type from the static HierarchyService class
Your generic class must inherit the IBaseHierarchyEntity definition
```
public interface IBaseHierarchyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
  The result returned will be the list of the following class
 public class HierarchyResult
 {
     public int Id { get; set; }
     public int EntityId { get; set; }
     public int? ParentEntityId { get; set; }
     public int Degree { get; set; }
     public string Name { get; set; } = string.Empty;
     public List<int> Parents { get; set; } = new List<int>();
     public List<int> SubEntities { get; set; } = new List<int>();
 }
```

```sh
