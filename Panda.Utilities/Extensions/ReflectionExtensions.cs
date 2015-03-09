using System.Collections;
using System.Linq;

namespace Panda.Utilities.Extensions
{
    public static class ReflectionExtensions
    {
        public static object CopyPropertiesTo(this object source, object destination)
        {
            var source_properties = source.GetType().GetProperties().AsEnumerable();
            var destination_properties = destination.GetType().GetProperties().AsEnumerable();
            source_properties.Join(destination_properties, 
                                   p => p.Name.ToUpperInvariant(), 
                                   p => p.Name.ToUpperInvariant(), 
                                   (source_info, destination_info) => new {Source = source_info, Destination = destination_info})
                             .Where(obj => obj.Destination.CanWrite)
                             .Apply(obj =>
                             {
                                 var value = obj.Source.GetValue(source);
                                 if (typeof (IList).IsAssignableFrom(obj.Source.PropertyType) &&
                                     typeof (IList).IsAssignableFrom(obj.Destination.PropertyType))
                                 {
                                     var source_list = value as IList;
                                     var destination_list = obj.Destination.GetValue(destination) as IList;
                                     if (source_list == null || destination_list == null) 
                                         return;

                                     foreach (var item in source_list)
                                         destination_list.Add(item);
                                 }
                                 else
                                    obj.Destination.SetValue(destination, value);
                             });
            return source;
        }
    }
}
