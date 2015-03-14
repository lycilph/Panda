using System;
using System.ComponentModel.Composition;

namespace Panda.ApplicationCore.Utilities
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ExportOrderAttribute : ExportAttribute
    {
        public int Order { get; set; }

        public ExportOrderAttribute(int order = Int32.MaxValue)
        {
            Order = order;
        }
    }
}
