using System.Collections.ObjectModel;

namespace TabControlItem
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>()
        {
            new ItemViewModel(){Header="Tab1"},
            new ItemViewModel(){Header="Tab2"},
            new ItemViewModel(){Header="Tab3"},
            new ItemViewModel(){Header="Tab4"},
            new ItemViewModel(){Header="Tab5"}
        };
    }
}
