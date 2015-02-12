using Panda.ApplicationCore.Menu.ViewModels;
using ReactiveUI;

namespace Panda.ApplicationCore.Shell
{
    public interface IShell
    {
        ReactiveList<IWindowCommand> LeftShellCommands { get; }
        ReactiveList<IWindowCommand> RightShellCommands { get; }
        ReactiveList<IFlyout> ShellFlyouts { get; }
        IMenu Menu { get; }
    }
}
