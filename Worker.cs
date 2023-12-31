using System;

internal class Worker : User
{
    public enum WorkerType { Waiter, Chef }
    public WorkerType Type { get; set; }
    public double Salary { get; set; }
}
