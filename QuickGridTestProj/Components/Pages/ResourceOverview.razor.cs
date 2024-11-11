using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;
using QuickGridTest.Data.Contexts;
using QuickGridTest.Data.Entities;

namespace QuickGridTestProj.Components.Pages
{
    public partial class ResourceOverview
    {
        [Inject]
        private IDbContextFactory<QuickGridDbContext> _dbContextFactory { get; set; } = default!;

        private QuickGridDbContext _dbContext = default!;

        [SupplyParameterFromForm]
        public Resource NewResource { get; set; } = new();

        public PaginationState Pagination = new PaginationState { ItemsPerPage = 10 };

        public async ValueTask<GridItemsProviderResult<Resource>> GetItems(GridItemsProviderRequest<Resource> request)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var totalCount = await context.Resources.CountAsync(request.CancellationToken);
            IQueryable<Resource> query = context.Resources.OrderBy(x => x.ResourceId);
            query = request.ApplySorting(query).Skip(request.StartIndex);
            if (request.Count.HasValue)
            {
                query = query.Take(request.Count.Value);
            }

            var items = await query.ToArrayAsync(request.CancellationToken);
            var result = new GridItemsProviderResult<Resource>
            {
                Items = items,
                TotalItemCount = totalCount
            };

            return result;
        }

        private async Task OnAddResourceSubmit()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            dbContext.Add(NewResource);
            await dbContext.SaveChangesAsync();

            NewResource = new();
        }

        private async Task Delete(int resourceId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var entity = dbContext.Resources.Find(resourceId);
            if (entity != null)
            {
                dbContext.Resources.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
