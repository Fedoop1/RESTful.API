using RESTful.API.Data;
using RESTful.API.Models;

namespace RESTful.API.Extensions;
internal static class EntityMapper
{
    public static Item ToModel(this ItemEntity entity) =>
        new() {Id = entity.Id, Name = entity.Name, CategoryId = entity.CategoryId};

    public static Category ToModel(this CategoryEntity entity) =>
        new() { Id = entity.Id, Name = entity.Name };

    public static ItemEntity ToEntity(this ItemDetails details) =>
        new() { Name = details.Name, CategoryId = details.CategoryId };

    public static CategoryEntity ToEntity(this CategoryDetails details) =>
        new() { Name = details.Name };

}
