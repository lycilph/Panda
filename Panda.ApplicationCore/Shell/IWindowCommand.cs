using Caliburn.Micro;

namespace Panda.ApplicationCore.Shell
{
    public interface IWindowCommand : IHaveDisplayName
    {
        bool IsEnabled { get; set; }
        bool IsVisible { get; set; }
        void Execute();
    }
}
