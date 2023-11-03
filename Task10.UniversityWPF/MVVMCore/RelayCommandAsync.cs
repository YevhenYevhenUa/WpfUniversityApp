using System;
using System.Threading.Tasks;

namespace Task10.UniversityWPF.MVVMCore;
public class RelayCommandAsync : AsyncCommandBase
{
    private Func<Task> _execute;

    public RelayCommandAsync(Func<Task> execute)
    {
        _execute = execute;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        await _execute();
    }
}
