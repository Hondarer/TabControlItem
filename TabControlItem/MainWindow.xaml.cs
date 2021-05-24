// 継承かビヘイビアか、どちらでもいけそう
// DataContextがINotifyCollectionChangedの場合は考慮、ICollectionなら単発(ユースケースによって簡略化してもよいはず)
// 左のDataCntexts、右のDataCntextsに分ける
// 可視領域判定で見えないところを購読する
// プロパティの変化を捕まえ集約する
// 公開状態を更新する
// テンプレートで状態をバインドする

using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TabControlItem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            DataContext = new MainWindowViewModel();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;

            ScrollViewer scrollviewer = (ScrollViewer)tabControl.Template.FindName("scrollViewer", tabControl);

            //DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromName(nameof(tabControl.ItemsSource), typeof(ItemsControl), typeof(TabControl), true);
            //descriptor.AddValueChanged(tabControl, TabControl_ItemsSourceChanged);

            scrollviewer.ScrollChanged += Scrollviewer_ScrollChanged;

            DetermineVisible("MainWindow_Loaded", scrollviewer);
        }

        //private void TabControl_ItemsSourceChanged(object sender, EventArgs e)
        //{
        //    DetermineVisible("TabControl_ItemsSourceChanged", (ScrollViewer)((TabControl)sender).Template.FindName("scrollViewer", tabControl));
        //}

        private void Scrollviewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // サイズが変化して表示されているアイテムの個数が変化した際にもきちんと呼ばれる
            // ItemsSource(そのものの入替、ObservableCollection における増減)が変化したときもきちんと呼ばれる
            DetermineVisible("Scrollviewer_ScrollChanged", (ScrollViewer)sender);
        }

        private void DetermineVisible(string reason, ScrollViewer scrollviewer)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{DateTime.Now} {reason}");

            UIElementCollection children = ((Panel)scrollviewer.Content).Children;

            System.Drawing.Rectangle scrollviewerRectangle = new System.Drawing.Rectangle(
                (int)scrollviewer.PointToScreen(new Point(0.0d, 0.0d)).X,
                (int)scrollviewer.PointToScreen(new Point(0.0d, 0.0d)).Y,
                (int)scrollviewer.ActualWidth,
                (int)scrollviewer.ActualHeight
                );

            //sb.AppendLine($"*** ScrollViewer ***");
            //sb.AppendLine($"  X={scrollviewerRectangle.X}");
            //sb.AppendLine($"  Y={scrollviewerRectangle.Y}");
            //sb.AppendLine($"  W={scrollviewerRectangle.Width}");
            //sb.AppendLine($"  H={scrollviewerRectangle.Height}");

            foreach (FrameworkElement child in children)
            {
                System.Drawing.Rectangle childRectangle = new System.Drawing.Rectangle(
                    (int)child.PointToScreen(new Point(0.0d, 0.0d)).X,
                    (int)child.PointToScreen(new Point(0.0d, 0.0d)).Y,
                    (int)child.ActualWidth,
                    (int)child.ActualHeight
                    );

                sb.AppendLine($"*** Child ***");
                //sb.AppendLine($"    X={childRectangle.X}");
                //sb.AppendLine($"    Y={childRectangle.Y}");
                //sb.AppendLine($"    W={childRectangle.Width}");
                //sb.AppendLine($"    H={childRectangle.Height}");
                sb.AppendLine($"    DataContext.Header={((ItemViewModel)child.DataContext).Header}");
                sb.AppendLine($"    Contains={scrollviewerRectangle.Contains(childRectangle)}");
            }

            resultTextBox.Text = sb.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ItemViewModel> items = ((MainWindowViewModel)DataContext).Items;
            items.Add(new ItemViewModel() { Header = "Tab" });
        }
    }
}
