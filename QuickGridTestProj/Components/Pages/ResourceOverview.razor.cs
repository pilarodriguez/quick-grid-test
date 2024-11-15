using QuickGridTest.Data.Entities;

namespace QuickGridTestProj.Components.Pages
{
    public partial class ResourceOverview
    {
        List<Resource> ResourceList = default!;

        protected override void OnInitialized()
        {
            var resources =

            ResourceList = new List<Resource>()
            {
                new Resource
                {
                    ResourceId = 1,
                    ResourceName = "item 1",
                    ResourceType = "type 1"
                },
                new Resource
                {
                    ResourceId = 2,
                    ResourceName = "item 2",
                    ResourceType = "type 1"
                },
                new Resource
                {
                    ResourceId = 3,
                    ResourceName = "item 3",
                    ResourceType = "type 1"
                }
            };
        }       
    }
}
