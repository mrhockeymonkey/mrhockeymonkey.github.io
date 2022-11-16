namespace BlazorWasmSpa.Data;

public interface IDataSynchronizer
{
    Task SynchronizeAsync();
    
    public event Action<int>? OnProgress;
    public event Action? OnCompleted;
}