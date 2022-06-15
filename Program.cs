using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var computerRepository = new ComputerRepository(databaseConfig);
var labRepository = new LabRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }

    if(modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
        var computer = computerRepository.GetById(id);
        Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        } else {
            Console.WriteLine($"O computador com id {id} não existe");
        }
    }

    if(modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
        } else {
            Console.WriteLine($"O computador com id {id} não existe");
        }
    }
    
    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
        computerRepository.Delete(id);
        } else {
            Console.WriteLine($"O computador com id {id} não existe");
        }
    }
}
if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lista de Laboratórios:");
        foreach (var lab in labRepository.GetAll())
        {
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        string name = args[4];
        string block = args[5];

        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }

    if(modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        string name = args[4];
        string block = args[5];

        var lab = new Lab(id, number, name, block);
        labRepository.Update(lab);
    }

    if(modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        if(labRepository.ExistsById(id))
        {
            var lab = labRepository.GetById(id);
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        } else{
            Console.WriteLine($"O Lab com Id {id} não existe");
        }
    
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        labRepository.Delete(id);
    }
}