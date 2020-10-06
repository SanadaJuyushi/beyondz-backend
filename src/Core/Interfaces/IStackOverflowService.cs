using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStackOverflowService
    {
        Task<List<ApiTagItem>> GetStackOverflowTagsAsync();
    }
}
