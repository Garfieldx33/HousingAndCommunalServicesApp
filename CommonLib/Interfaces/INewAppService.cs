using CommonLib.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CommonLib.Interfaces;

public interface INewAppService
{
    public Task<IActionResult> AddNewApplication([FromQuery] ApplicationDTO dataClass);
}
