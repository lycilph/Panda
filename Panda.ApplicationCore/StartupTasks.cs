using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using NLog;
using Panda.ApplicationCore.Shell;
using LogManager = NLog.LogManager;

namespace Panda.ApplicationCore
{
    public class StartupTasks
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [Export(ApplicationBootstrapper.STARTUP_TASK_NAME, typeof(BootstrapperTask))]
        [ExportOrder(0)]
        public void ApplyBindingScopeOverride()
        {
            logger.Trace("ApplyBindingScopeOverride");

            var get_named_elements = BindingScope.GetNamedElements;
            BindingScope.GetNamedElements = (o =>
            {
                var elements = get_named_elements(o).ToList();

                if (o is MetroWindow)
                    elements.AddRange(ResolveMetroWindow(o, get_named_elements));

                return elements;
            });
        }

        private static IEnumerable<FrameworkElement> ResolveMetroWindow(DependencyObject o, Func<DependencyObject, IEnumerable<FrameworkElement>> get_named_elements)
        {
            logger.Trace("ResolveMetroWindow");

            var list = new List<FrameworkElement>();
            var type = o.GetType();

            // Check fields for elements to add
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                             .Where(f => f.DeclaringType == type)
                             .ToList();
            var flyouts = fields.Where(f => f.FieldType == typeof(FlyoutsControl))
                                .Select(f => f.GetValue(o))
                                .Cast<FlyoutsControl>()
                                .ToList();
            var commands = fields.Where(f => f.FieldType == typeof(WindowCommands))
                                .Select(f => f.GetValue(o))
                                .Cast<WindowCommands>()
                                .ToList();
            list.AddRange(flyouts);
            list.AddRange(commands);

            // Check properties for elements to add
            if (!flyouts.Any())
            {
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(FlyoutsControl))
                    .Select(p => p.GetValue(o))
                    .Cast<FlyoutsControl>()
                    .Where(c => c != null)
                    .Apply(c => list.AddRange(get_named_elements(c)));
            }
            if (!commands.Any())
            {
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(WindowCommands))
                    .Select(p => p.GetValue(o))
                    .Cast<WindowCommands>()
                    .Where(c => c != null)
                    .Apply(c => list.AddRange(get_named_elements(c)));
            }

            return list;
        }

        [Export(ApplicationBootstrapper.STARTUP_TASK_NAME, typeof(BootstrapperTask))]
        [ExportOrder(0)]
        public void ApplyParserOverride()
        {
            logger.Trace("ApplyParserOverride");

            var current_parser = Parser.CreateTrigger;
            Parser.CreateTrigger = (target, trigger_text) => InputBindingParser.CanParse(trigger_text)
                                                           ? InputBindingParser.CreateTrigger(trigger_text)
                                                           : current_parser(target, trigger_text);
        }
    }
}
