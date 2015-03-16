using System.IO;
using System.Linq;
using System.Reflection;

namespace Panda.Utilities.Extensions
{
    public static class ResourceExtensions
    {
        public static string GetResource(string name)
        {
            var resource_name = Assembly.GetExecutingAssembly().GetManifestResourceNames().First(n => n.Contains(name));
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource_name))
            using (var sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
