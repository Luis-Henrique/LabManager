namespace LabManager.Models;

class Computer
{
    public int Id { get; set; }
    public String Ram { get; set; }
    public String Processor { get; set; }

    public Computer(int id, string ram,string processor)
    {
        Id = id;
        Ram = ram;
        Processor = processor;
    }
} 