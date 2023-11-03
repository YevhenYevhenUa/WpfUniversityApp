using System.Windows;

namespace Task10.UniversityWPF.Services;
public class DialogueService : IDialogueService
{
    public MessageBoxResult DeleteMessage(string item)
    {
        var result = MessageBox.Show($"Do you really want to delete object: {item}", "deleting", MessageBoxButton.YesNo);
        return result;
    }

    public void DeleteMessageError()
    {
        MessageBox.Show("Unable to delete an object that contains items inside", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void EditMessageError()
    {
        MessageBox.Show("Fields should not be empty", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void EditMessageSuccess()
    {
        MessageBox.Show("Successfully edited", "Success", MessageBoxButton.OK);
    }

    public void AddMessageSuccess()
    {
        MessageBox.Show("Successfully created", "Success", MessageBoxButton.OK);
    }

    public void AddMessageError()
    {
        MessageBox.Show("Fields are empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void SuccessMessage()
    {
        MessageBox.Show("Success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void FileMessageError()
    {
        MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void NotSelectedEditWarning()
    {
        MessageBox.Show("Nothing is selected for editing! Select something and repeat the action", "Selection warning", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void NotSelectedDeleteWarning()
    {
        MessageBox.Show("Nothing is selected for deletion! Select something and repeat the action", "Deletion warning", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void NotSelectedForFileGenerationWarning()
    {
        MessageBox.Show("Nothing is selected for file with students generation! Select course or group and repeat the action", "Generate file warning", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void NotSelectedForExportImportWarning()
    {
        MessageBox.Show("Nothing is selected for Imort/Export students! Select group and repeat the action", "Import/Export warning", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
