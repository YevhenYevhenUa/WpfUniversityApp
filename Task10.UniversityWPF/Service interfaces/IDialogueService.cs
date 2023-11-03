using System.Windows;

namespace Task10.UniversityWPF.Services;
public interface IDialogueService
{
    void EditMessageError();
    void EditMessageSuccess();
    MessageBoxResult DeleteMessage(string item);
    void DeleteMessageError();
    void AddMessageSuccess();
    void AddMessageError();
    void SuccessMessage();
    void FileMessageError();
    void NotSelectedEditWarning();
    void NotSelectedDeleteWarning();
    void NotSelectedForFileGenerationWarning();
    void NotSelectedForExportImportWarning();
}
