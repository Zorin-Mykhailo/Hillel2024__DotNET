using HW22_Drugs.Application;
using HW22_Drugs.Model;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HW22_Drugs.Services;

public class JsonFileService : IFileService
{
    public List<FamilyMember> Open(string fileName)
    {
        List<FamilyMember> phones;

        DataContractJsonSerializer jsonFormatter =
                new (typeof(List<FamilyMember>));
        using(FileStream fs = new(fileName, FileMode.OpenOrCreate))
            phones = jsonFormatter.ReadObject(fs) as List<FamilyMember> ?? new();

        return phones;
    }

    public void Save(string filename, List<FamilyMember> phonesList)
    {
        DataContractJsonSerializer jsonFormatter = new (typeof(List<FamilyMember>));
        using(FileStream fs = new(filename, FileMode.Create))
            jsonFormatter.WriteObject(fs, phonesList);
    }
}