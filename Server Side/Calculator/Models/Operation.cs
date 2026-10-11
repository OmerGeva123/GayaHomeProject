using System;

namespace Calculator.Models
{
    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class Operation
    {
        public int Id { get; set; }
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime Date { get; set; }
        public double result { get; set; }
    }
}
