using System.Collections.Concurrent;

namespace ConcurrentPagesAndDetails;

public class ConcurrentAsyncEnumerableProcessor
{
    private readonly FakeApi _api;
    private ConcurrentBag<ItemDetail> _detailsBag;
    private List<Task> _detailsTasks;
    private SemaphoreSlim _semaphore;

    public ConcurrentAsyncEnumerableProcessor(FakeApi api, int maxConcurrentDetailsRequests)
    {
        _api = api;
        _detailsBag = new();
        _detailsTasks = new();
        _semaphore = new(maxConcurrentDetailsRequests, maxConcurrentDetailsRequests);
    }

    public async Task ProcessAsync()
    {
        await StartDetailsTasks();
        await Task.WhenAll(_detailsTasks);
        Console.WriteLine($"{nameof(ConcurrentAsyncEnumerableProcessor)} finished storing {_detailsBag.Count()} detail objects");
    }

    private async Task StartDetailsTasks()
    {
        await foreach (var descriptor in _api.GetDescriptorAsyncEnumerable())
        {
            _detailsTasks.Add(GetItemDetailAsync(descriptor));
            // Console.WriteLine($"Got ItemDescriptor: {descriptor.name}");
        }
    }

    private async Task GetItemDetailAsync(ItemDescriptor descriptor)
    {
        await _semaphore.WaitAsync();
        try
        {
            var detail = await _api.GetDetail(descriptor.id);
            _detailsBag.Add(detail);
            // Console.WriteLine($"Got ItemDetail {detail.name}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}