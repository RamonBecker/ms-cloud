using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }
    private bool IsNumeric(string strNumber)
    {
        double number;

        var isNumber = double.TryParse(strNumber,
                                       System.Globalization.NumberStyles.Any,
                                       System.Globalization.NumberFormatInfo.InvariantInfo,
                                       out number);
        return isNumber;
    }


    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            return Ok((Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber)).ToString());

        return BadRequest("Invalid input");
    }

}
