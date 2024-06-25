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

    [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
    public IActionResult Subtraction(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            return Ok((Convert.ToDecimal(firstNumber) - Convert.ToDecimal(secondNumber)).ToString());

        return BadRequest("Invalid input");
    }


    [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
    public IActionResult Multiplication(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            return Ok((Convert.ToDecimal(firstNumber) * Convert.ToDecimal(secondNumber)).ToString());

        return BadRequest("Invalid input");
    }

    [HttpGet("mean/{firstNumber}/{secondNumber}")]
    public IActionResult Mean(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            return Ok(((Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber)) / 2).ToString());

        return BadRequest("Invalid input");
    }

    [HttpGet("square-root/{firstNumber}")]
    public IActionResult SquareRoot(string firstNumber)
    {
        if (IsNumeric(firstNumber))
            return Ok(Math.Sqrt((double)Convert.ToDecimal(firstNumber)).ToString());

        return BadRequest("Invalid input");
    }


    [HttpGet("Division/{firstNumber}/{secondNumber}")]
    public IActionResult Division(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber))
            return Ok((Convert.ToDecimal(firstNumber) / Convert.ToDecimal(secondNumber)).ToString());

        return BadRequest("Invalid input");
    }
}
