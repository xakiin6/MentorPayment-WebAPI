using System;
using System.Collections.Generic;

public class ClassMentor {
public DateTime  Week { get; set; }
    public int Students { get; set; }
    public double Total { get; set; }
    public bool Paid { get; set; }
    public DateTime? PDate { get; set; }
    public bool Isvisible { get; set; }
    public TotalSum TotalSum {get; set;}
    }

   public class TotalSum{
    public DateTime? Month { get; set; }
    public double Total { get; set; }
   } 