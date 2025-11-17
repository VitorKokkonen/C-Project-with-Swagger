// ProductDomain/Interfaces/IUnitOfWork.cs
namespace ProductDomain.Interfaces;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}