using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.BusinessLogic;

public class SHAppLogic
{
    private readonly SHAppRepositry _repository;

    public SHAppLogic(SHAppRepositry repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<DummyItem>> GetDummyItemsAsync()
    {
        return _repository.GetDummyItemsAsync();
    }

    public int Add(int a, int b)
    {
        return _repository.Add(a, b);
    }
    public int multiple(int a, int b)
    {
        return a * b;
    }
}
