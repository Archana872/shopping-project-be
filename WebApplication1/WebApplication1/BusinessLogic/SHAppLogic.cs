using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.BusinessLogic;

public interface ISHAppLogic
{
    Task<IReadOnlyList<DummyItem>> GetDummyItemsAsync();
    int Add(int a, int b);
}

public class SHAppLogic : ISHAppLogic
{
    private readonly ISHAppRepositry _repository;

    public SHAppLogic(ISHAppRepositry repository)
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
}
