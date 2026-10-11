namespace Calculator.Models
{
    public class OperationsHistory
    {
        public List<Operation> Last3OfType { get; set; }
        public List<Operation> LastMonthOfType { get; set; }
    }
}
