using Core.Entities;

namespace Core.Specifications
{
    public class StackOverflowTagWithNameFilterSpecificication : BaseSpecification<StackOverflowTag>
    {
        public StackOverflowTagWithNameFilterSpecificication(string name)
        : base(x => x.Name == name)
        {

        }
    }
}
