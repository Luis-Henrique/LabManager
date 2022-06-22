
namespace LabManager.Models;

public class Lab
{
    public int Id { get; set; }
    public String Number { get; }
    public String Name { get;}
    public String Block { get; }

    public Lab()
    {
        
    }

    public Lab(int id, String number, String name, String block)
    {
        Id = id;
        Number = number;
        Name = name;
        Block = block;
    }
}