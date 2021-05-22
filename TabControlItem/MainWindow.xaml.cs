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

            DataContext = new MainWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollviewer = (ScrollViewer)tabControl.Template.FindName("scrollViewer", tabControl);
            var children = ((Panel)scrollviewer.Content).Children;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** ScrollViewer ***");

            System.Drawing.Rectangle scrollviewerRectangle = new System.Drawing.Rectangle(
                (int)scrollviewer.PointToScreen(new Point(0.0d, 0.0d)).X,
                (int)scrollviewer.PointToScreen(new Point(0.0d, 0.0d)).Y,
                (int)scrollviewer.ActualWidth,
                (int)scrollviewer.ActualHeight
                );

            sb.AppendLine($"  X={scrollviewerRectangle.X}");
            sb.AppendLine($"  Y={scrollviewerRectangle.Y}");
            sb.AppendLine($"  W={scrollviewerRectangle.Width}");
            sb.AppendLine($"  H={scrollviewerRectangle.Height}");

            foreach (FrameworkElement child in children)
            {
                System.Drawing.Rectangle childRectangle = new System.Drawing.Rectangle(
                    (int)child.PointToScreen(new Point(0.0d, 0.0d)).X,
                    (int)child.PointToScreen(new Point(0.0d, 0.0d)).Y,
                    (int)child.ActualWidth,
                    (int)child.ActualHeight
                    );

                sb.AppendLine($"*** Child ***");
                sb.AppendLine($"    X={childRectangle.X}");
                sb.AppendLine($"    Y={childRectangle.Y}");
                sb.AppendLine($"    W={childRectangle.Width}");
                sb.AppendLine($"    H={childRectangle.Height}");
                sb.AppendLine($"    DataContext.Header={((ItemViewModel)child.DataContext).Header}");
                sb.AppendLine($"    Contains={scrollviewerRectangle.Contains(childRectangle)}");
            }

            resultTextBox.Text = sb.ToString();
        }
    }
}
