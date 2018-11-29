using System;
using System.Collections.Generic;

public class Reviewer {
    public DateTime  Month { get; set; }
    public int Projects { get; set; }
    public double Total { get; set; }
    public bool Paid { get; set; }
    public DateTime? PDate { get; set; }
    public bool Isvisible { get; set; }
    public TotalSum TotalSum {get; set;}
}