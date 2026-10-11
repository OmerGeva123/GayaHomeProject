using Calculator.Data;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("Calculate")]
    public class CalculateController : ControllerBase
    {

        private readonly AppDbContext _context;
        public CalculateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Calculate")]
        public ActionResult<double> Calculate([FromBody] Operation operation)
        {
            double result = operation.OperationType switch
            {
                OperationType.Add => operation.Number1 + operation.Number2,
                OperationType.Subtract => operation.Number1 - operation.Number2,
                OperationType.Multiply => operation.Number1 * operation.Number2,
                OperationType.Divide => operation.Number2 != 0 ? operation.Number1 / operation.Number2 : throw new DivideByZeroException("Cannot divide by zero."),
                _ => throw new InvalidOperationException("Invalid operation type.")
            };
            operation.Date = DateTime.Now;
            operation.result = result;
            _context.Operations.Add(operation);
            _context.SaveChanges();
            return Ok(result);
        }
        [HttpGet("History/{operationType}")]
        public async Task<OperationsHistory> GetHistory(OperationType operationType)
        {
            OperationsHistory op = new OperationsHistory();

            // Return the most recent 3 operations of the specified type
            op.Last3OfType =  await _context.Operations
                .Where(o => o.OperationType == operationType)
                .OrderByDescending(o => o.Date)
                .Take(3)
                .ToListAsync();
            op.LastMonthOfType = await _context.Operations
                .Where(o => o.OperationType == operationType && o.Date >= DateTime.Now.AddMonths(-1))
                .OrderByDescending(o => o.Date)
                .ToListAsync();
            return op;
        }
    }
}
