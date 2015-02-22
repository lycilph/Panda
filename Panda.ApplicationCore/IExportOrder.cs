using System;
using System.ComponentModel;

namespace Panda.ApplicationCore
{
    public interface IExportOrder
    {
        [DefaultValue(Int32.MaxValue)]
        int Order { get; }
    }
}
