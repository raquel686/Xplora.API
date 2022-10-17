namespace XploraAPI.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}