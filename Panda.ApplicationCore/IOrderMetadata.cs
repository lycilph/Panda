using System;
using System.ComponentModel;

namespace Panda.ApplicationCore
{
    public interface IOrderMetadata
    {
        [DefaultValue(Int32.MaxValue)]
        int Order { get; }
    }
}
