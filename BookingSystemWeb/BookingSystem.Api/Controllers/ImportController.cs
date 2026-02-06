using BookingSystem.Application.Features.Import;
using BookingSystem.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/users/import")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
public class ImportController : ControllerBase
{
    private readonly IMediator _mediator;
    private const long MaxFileSizeLimit = 5L * 1024 * 1024 * 1024;
    
    public ImportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [RequestSizeLimit(MaxFileSizeLimit)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSizeLimit)]
    public async Task<IActionResult> ImportData(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest("File is empty.");
        }
        
        if (!Path.GetExtension(file.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only .json files are supported.");
        }

        using var stream = file.OpenReadStream();
        var command = new ImportDataCommand(stream, file.FileName, file.Length);
        
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Import completed successfully." });
        }
        return StatusCode(500, new { Message = result.ErrorMessage });
    }
}