using Caliburn.Micro.ReactiveUI;
using Panda.ApplicationCore.Menu;
using Panda.ApplicationCore.Menu.ViewModels;
using Panda.ApplicationCore.StatusBar;
using Panda.ApplicationCore.StatusBar.ViewModels;
using ReactiveUI;

namespace Panda.ApplicationCore.Shell
{
    public class ShellBase : ReactiveScreen, IShell
    {
        private readonly ReactiveList<IWindowCommand> left_shell_commands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> LeftShellCommands 
        {
            get { return left_shell_commands; }
        }

        private readonly ReactiveList<IWindowCommand> right_shell_commands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> RightShellCommands
        {
            get { return right_shell_commands; }
        }

        private readonly ReactiveList<IFlyout> shell_flyouts = new ReactiveList<IFlyout>();
        public ReactiveList<IFlyout> ShellFlyouts
        {
            get { return shell_flyouts; }
        }

        private readonly IMenu menu = new MenuViewModel();
        public IMenu Menu
        {
            get { return menu; }
        }

        private readonly IStatusBar _StatusBar = new StatusBarViewModel();
        public IStatusBar StatusBar
        {
            get { return _StatusBar; }
        }
    }
}
