using WebApplication1.DataModel;

namespace WebApplication1.RepositryLayer;

public class SHAppRepositry
{
    public Task<IReadOnlyList<DummyItem>> GetDummyItemsAsync()
    {
        IReadOnlyList<DummyItem> items =
        [
            new DummyItem { Id = 1, Name = "Dummy Item A" },
            new DummyItem { Id = 2, Name = "Dummy Item B" },
            new DummyItem { Id = 3, Name = "Dummy Item C" }
        ];

        return Task.FromResult(items);
    }

    public int Add(int a, int b) => a + b;
}
