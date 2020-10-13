using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class SkillTagService : ISkillTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStackOverflowApiService _stackOverflowService;

        public SkillTagService(IStackOverflowApiService stackOverflowService, IUnitOfWork unitOfWork)
        {
            _stackOverflowService = stackOverflowService;
            _unitOfWork = unitOfWork;
        }

        public async Task GeneratorStackOverflowTags()
        {

            var items = await _stackOverflowService.GetStackOverflowTagsAsync();

            foreach (var tag in items)
            {
                var repo = _unitOfWork.Repository<StackOverflowTag>();
                var spec = new StackOverflowTagWithNameFilterSpecificication(tag.Name);

                if (await repo.ExistAsync(spec))
                {
                    var orgTag = await repo.GetEntityWithSpecAsync(spec);
                    orgTag.Popular = tag.Count;
                    orgTag.Description = tag.Excerpt;
                    repo.Update(orgTag);
                }
                else
                {
                    repo.Add(new StackOverflowTag
                    {
                        Name = tag.Name,
                        Popular = tag.Count,
                        Description = tag.Excerpt
                    });
                }
            }
            await _unitOfWork.Complete();

        }
    }
}
