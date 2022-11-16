namespace BlazorWasmSpa.Data;

public interface IDbContextInitializer
{
    Task InitializeAsync();
}