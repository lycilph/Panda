using Panda.ApplicationCore.Menu;
using Panda.ApplicationCore.StatusBar;
using ReactiveUI;

namespace Panda.ApplicationCore.Shell
{
    public interface IShell
    {
        ReactiveList<IWindowCommand> LeftShellCommands { get; }
        ReactiveList<IWindowCommand> RightShellCommands { get; }
        ReactiveList<IFlyout> ShellFlyouts { get; }
        IMenu Menu { get; }
        IStatusBar StatusBar { get; }
    }
}
