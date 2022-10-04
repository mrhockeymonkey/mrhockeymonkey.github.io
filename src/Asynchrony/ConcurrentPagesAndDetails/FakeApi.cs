namespace ConcurrentPagesAndDetails;

public record ItemDescriptor(Guid id, string name);
public record ItemDetail(Guid id, string name, string foo, string bar);
public record PagedResult<T>(int Page, int Pages, int PageSize, bool IsLastPage, IEnumerable<T> Items) where T : class;

public class FakeApi
{
    private readonly int _delay;
    private readonly int _pageSize;
    private readonly List<ItemDetail> _items;
    
    public FakeApi(int objectCount, int delay, int pageSize)
    {
        _delay = delay;
        _pageSize = pageSize;
        _items = new();
        foreach (var i in Enumerable.Range(1, objectCount))
        {
            _items.Add(new ItemDetail(Guid.NewGuid(), $"name-{i}", "foo-{i}", "bar-{i}"));
        }
    }

    public async Task<PagedResult<ItemDescriptor>> GetDescriptorPaged(int page)
    {
        await Task.Delay(_delay);
        var pages = (int)Math.Ceiling((double)_items.Count / _pageSize);
        var items = _items
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .Select(o => new ItemDescriptor(o.id, o.name))
            .ToList();
        
        return new PagedResult<ItemDescriptor>(page, pages, items.Count(), page == pages, items);
    }

    public async IAsyncEnumerable<ItemDescriptor> GetDescriptorAsyncEnumerable()
    {
        int page = 1;
        PagedResult<ItemDescriptor> pagedResult;
        do
        {
            pagedResult = await GetDescriptorPaged(page);
            foreach (var itemDescriptor in pagedResult.Items)
            {
                yield return itemDescriptor;
            }

            page++;
        } while (!pagedResult.IsLastPage);
    }

    public async Task<ItemDetail> GetDetail(Guid id)
    {
        await Task.Delay(_delay);
        return _items.Single(o => o.id == id);
    }
}
