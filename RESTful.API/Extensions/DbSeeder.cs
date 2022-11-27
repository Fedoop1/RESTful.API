using RESTful.API.Data;

namespace RESTful.API.Extensions;

internal static class DbSeeder
{
    private static IEnumerable<CategoryEntity> Categories
    {
        get
        {
            yield return new CategoryEntity { Id = 1, Name = "Category1" };
            yield return new CategoryEntity { Id = 2, Name = "Category2" };
            yield return new CategoryEntity { Id = 3, Name = "Category3" };
            yield return new CategoryEntity { Id = 4, Name = "Category4" };
            yield return new CategoryEntity { Id = 5, Name = "Category5" };
        }
    }

    private static IEnumerable<ItemEntity> Items
    {
        get
        {
            yield return new ItemEntity { Id = 1, Name = "Item1", CategoryId = 1 };
            yield return new ItemEntity { Id = 2, Name = "Item2", CategoryId = 2 };
            yield return new ItemEntity { Id = 3, Name = "Item3", CategoryId = 3 };
            yield return new ItemEntity { Id = 4, Name = "Item4", CategoryId = 4 };
            yield return new ItemEntity { Id = 5, Name = "Item5", CategoryId = 5 };
        }
    }

    public static async Task SeedAsync(this RestfulDbContext context)
    {
        if (context.Items.Count() != 0 || context.Categories.Count() != 0) return;

        await context.Categories.AddRangeAsync(Categories);

        await context.SaveChangesAsync();

        await context.Items.AddRangeAsync(Items);

        await context.SaveChangesAsync();
    }
}
