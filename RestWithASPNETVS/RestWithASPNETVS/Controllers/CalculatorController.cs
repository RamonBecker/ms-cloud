using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
 
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Get(string firstNumber, string secondNumber)
    {

        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var sum = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber);
            return Ok(sum.ToString());
        }

        return BadRequest("Invalid input");
    }

    private bool IsNumeric(string firstNumber)
    {
        throw new NotImplementedException();
    }
}
