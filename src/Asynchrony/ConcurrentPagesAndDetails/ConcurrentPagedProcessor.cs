using System.Collections.Concurrent;

namespace ConcurrentPagesAndDetails;

public class ConcurrentPagedProcessor
{
    private readonly FakeApi _api;
    private ConcurrentBag<ItemDetail> _detailsBag;
    private List<Task> _detailsTasks;
    private SemaphoreSlim _semaphore;

    public ConcurrentPagedProcessor(FakeApi api, int maxConcurrentDetailsRequests)
    {
        _api = api;
        _detailsBag = new();
        _detailsTasks = new();
        _semaphore = new(maxConcurrentDetailsRequests, maxConcurrentDetailsRequests);
    }

    public async Task ProcessAsync()
    {
        await GetAllPagesAsync();
        await Task.WhenAll(_detailsTasks);
        Console.WriteLine($"{nameof(ConcurrentPagedProcessor)} finished storing {_detailsBag.Count()} detail objects");
    }

    private async Task GetAllPagesAsync()
    {
        PagedResult<ItemDescriptor> pagedResult;
        int page = 1;
        do
        {
            pagedResult = await _api.GetDescriptorPaged(page);
            // Console.WriteLine($"Got page {pagedResult.Page}/{pagedResult.Pages} with {pagedResult.PageSize} items");
            _detailsTasks.AddRange(pagedResult.Items.Select(GetItemDetailAsync));
            page++;
        } while (!pagedResult.IsLastPage);
    }

    private async Task GetItemDetailAsync(ItemDescriptor descriptor)
    {
        await _semaphore.WaitAsync();
        try
        {
            var detail = await _api.GetDetail(descriptor.id);
            _detailsBag.Add(detail);
            // Console.WriteLine($"Added detail {detail.name}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}