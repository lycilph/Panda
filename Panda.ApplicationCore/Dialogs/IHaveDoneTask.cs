using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace Panda.ApplicationCore.Dialogs
{
    public interface IHaveDoneTask
    {
        Task<MessageDialogResult> Done { get; }
    }
}
