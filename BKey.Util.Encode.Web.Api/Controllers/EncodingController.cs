using BKey.Util.Encode.Encodings;
using Microsoft.AspNetCore.Mvc;

namespace BKey.Util.Encode.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EncodingController : ControllerBase
{

    private ILogger<EncodingController> Logger { get; }
    private IEncoderFactory EncoderFactory { get; }

    public EncodingController(
        ILogger<EncodingController> logger,
        IEncoderFactory encoderFactory)
    {
        Logger = logger;
        EncoderFactory = encoderFactory;
    }

    [HttpGet]
    public IActionResult GetSupportedEncodings()
    {
        var encodings = EncoderFactory.ListEncoders();
        return Ok(encodings);
    }

    [HttpPost("{encodingName}")]
    public IActionResult EncodeText(string encodingName, [FromForm] string text)
    {
        var encoder = EncoderFactory.CreateEncoder(encodingName);
        if (encoder is null)
        {
            return BadRequest("Encoding Not Supported");
        }

        try
        {
            var result = encoder.Process(text);
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
