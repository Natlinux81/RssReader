namespace Domain.Interface;

public interface IUnitOfWork
{
    void Commit();
    Task CommitAsync();
}