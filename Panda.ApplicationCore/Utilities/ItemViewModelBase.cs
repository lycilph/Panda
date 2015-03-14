using ReactiveUI;

namespace Panda.ApplicationCore.Utilities
{
    public class ItemViewModelBase<T> : ReactiveObject
    {
        public T AssociatedObject { get; protected set; }

        public ItemViewModelBase(T obj)
        {
            AssociatedObject = obj;

            if (AssociatedObject is IReactiveObject)
            {
                var temp = AssociatedObject as IReactiveObject;
                // ReSharper disable ExplicitCallerInfoArgument
                temp.PropertyChanging += (sender, args) => this.RaisePropertyChanging(args.PropertyName);
                temp.PropertyChanged += (sender, args) => this.RaisePropertyChanged(args.PropertyName);
                // ReSharper restore ExplicitCallerInfoArgument
            }
        }
    }
}
