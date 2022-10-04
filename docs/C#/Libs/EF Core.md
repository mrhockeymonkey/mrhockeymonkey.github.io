# EF Core

```c#
var documents = context.LatestStates
    .AsNoTracking() // perf++ readonly query
    .Select(ls => new QueryResult() // perf++ project only required properties
    {
        Name = ls.Asset.Name, // creates JOIN to asset table
        Type = ls.Asset.Subtype.Type.Name, // creates two JOINs to related tables
        Subtype = ls.Asset.Subtype.Name,
        JsonData = ls.AssetState.JsonData // creates JOIN to assetState table
    })
    .AsEnumerable() // streaming (not buffering)
    .Select(NewElasticDocument) // client side transformation
    .ToList();
```


# Explicit tracking
The following avoids the need to query the db and pull back all data before an update 
but does some with the risk of Exceptions if the entity does not exist in the db. 

```c#

var entityToUpdate = new MyEntity()
{
    Id = "1234",
};

_context.Attach(entityToUpdate);
entityToUpdate.PublishedDate = "2012";

await _context.SaveChangesAsync();
```

# Viewing Changes
```c#
_context.ChangeTracker.DetectChanges();
Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
```
