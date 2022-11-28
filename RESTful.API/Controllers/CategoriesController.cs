using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful.API.Data;
using RESTful.API.Extensions;
using RESTful.API.Models;
using RESTful.API.Queries;

namespace RESTful.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RestfulDbContext context;

        public CategoriesController(RestfulDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IPagedResult<Category> Search([FromQuery] CategoriesPagedQuery query)
        {
            var items = context.Categories.Select(c => c.ToModel());

            var page = items.ApplyPaging(query);

            return page;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var item = await this.context.Categories.FindAsync(id);

            if (item is null) return NotFound();

            return item.ToModel();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDetails category)
        {
            var entity = category.ToEntity();

            await this.context.Categories.AddAsync(entity);
            await this.context.SaveChangesAsync();

            return Created($"{HttpContext.Request.GetDisplayUrl()}/{entity.Id}", category.ToEntity());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Due to the lack of cascade deletion in in-memory db, we have to include dependent entities explicitly
            var entity = await this.context.Categories
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null) return NotFound();

            this.context.Categories.Remove(entity);

            await this.context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDetails details)
        {
            var entity = details.ToEntity();
            entity.Id = id;

            if (this.context.Categories.FirstOrDefault(c => c.Id == id) is null)
            {
                var error = new {id = $"Category with id {id} doesn't exist"};
                return BadRequest(error);
            }

            this.context.Categories.Update(entity);

            await this.context.SaveChangesAsync();

            return Ok();
        }
    }
}
