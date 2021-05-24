using System.Collections.ObjectModel;

namespace TabControlItem
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>()
        {
            new ItemViewModel(){Header="Tab1"},
            new ItemViewModel(){Header="Tab2"},
            new ItemViewModel(){Header="Tab3"},
            new ItemViewModel(){Header="Tab4"},
            new ItemViewModel(){Header="Tab5"}
        };

        public ObservableCollection<ItemViewModel> Items 
        { 
            get
            {
                return _items;
            }
            set
            {
                SetProperty(ref _items, value);
            }
        } 
    }
}
