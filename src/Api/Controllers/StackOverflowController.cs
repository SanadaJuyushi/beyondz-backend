using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class StackOverflowController : BaseApiController
    {
        private readonly ISkillTagService _service;
        public StackOverflowController(ISkillTagService service)
        {
            _service = service;
        }

        [HttpGet("Send")]
        public async Task<ActionResult> GetApiData()
        {
            await _service.GeneratorStackOverflowTags();
            return Content("OK");
        }

    }
}
