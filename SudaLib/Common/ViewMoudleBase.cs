using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudaLib
{
    public class ViewMoudleBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static void OnPropertyChangedStatic(PropertyChangedEventHandler handler, [CallerMemberName] string propertyName = null)
        {
            handler?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
