using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
