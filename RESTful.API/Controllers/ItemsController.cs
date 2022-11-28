using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RESTful.API.Data;
using RESTful.API.Extensions;
using RESTful.API.Models;
using RESTful.API.Queries;

namespace RESTful.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly RestfulDbContext context;

        public ItemsController(RestfulDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IPagedResult<Item> Search([FromQuery] ItemsPagedQuery query)
        {
            var items =
                context.Items
                    .Where(i => query.CategoryId == null || i.CategoryId == query.CategoryId)
                    .Select(i => i.ToModel());

            var page = items.ApplyPaging(query);

            return page;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await context.Items.FindAsync(id);

            if (item is null) return NotFound();

            return item.ToModel();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemDetails details)
        {
            var entity = details.ToEntity();

            if (!await IsCategoryExistAsync(details.CategoryId))
            {
                var error = new {categoryId = $"Category list with id {details.CategoryId} doesn't exist"};

                return BadRequest(error);
            }

            await this.context.Items.AddAsync(entity);
            await this.context.SaveChangesAsync();

            return Created($"{HttpContext.Request.GetDisplayUrl()}/{entity.Id}", entity.ToModel());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await this.context.Items.FindAsync(id);

            if (entity is null)
            {
                return NotFound();
            }

            this.context.Items.Remove(entity);

            await this.context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ItemDetails details)
        {
            var entity = details.ToEntity();
            entity.Id = id;

            if (this.context.Items.FirstOrDefault(c => c.Id == id) is null)
            {
                var error = new { id = $"Item with id {id} doesn't exist" };

                return BadRequest(error);
            }

            if (!await IsCategoryExistAsync(details.CategoryId))
            {
                var error = new { categoryId = $"Category list with id {details.CategoryId} doesn't exist" };

                return BadRequest(error);
            }

            this.context.Items.Update(entity);

            await this.context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> IsCategoryExistAsync(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);

            return category is not null;
        }
    }
}
