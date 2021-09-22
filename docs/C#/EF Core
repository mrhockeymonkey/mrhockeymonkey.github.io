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
