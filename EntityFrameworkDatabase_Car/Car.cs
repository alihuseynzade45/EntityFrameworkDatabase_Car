using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Car
{
    public int Id { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int StNumber { get; set; }

    public override string ToString() => $"{Id} {Mark} {Model} {Year} {StNumber}";
}
