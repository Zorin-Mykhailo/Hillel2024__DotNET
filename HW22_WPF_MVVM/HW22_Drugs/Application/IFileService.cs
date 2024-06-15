using HW22_Drugs.Model;

namespace HW22_Drugs.Application;

public interface IFileService
{
    List<FamilyMember> Open(string filename);

    void Save(string filename, List<FamilyMember> phoneList);
}