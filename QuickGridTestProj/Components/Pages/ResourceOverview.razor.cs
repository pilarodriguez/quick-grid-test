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

        protected override void OnInitialized()
        {
            _dbContext = _dbContextFactory.CreateDbContext();
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

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
