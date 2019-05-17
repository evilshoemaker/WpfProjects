using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstaSoft.Controls
{
    /// <summary>
    /// Логика взаимодействия для AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTextBox : TextBox
    {
        Popup Popup { get { return this.Template.FindName("PART_Popup", this) as Popup; } }
        ListBox ItemList { get { return this.Template.FindName("PART_ItemList", this) as ListBox; } }
        ScrollViewer Host { get { return this.Template.FindName("PART_ContentHost", this) as ScrollViewer; } }
        
        UIElement TextBoxView { 
            get 
            { 
                foreach (object o in LogicalTreeHelper.GetChildren(Host)) 
                    return o as UIElement; 

                return null; 
            } 
        }

        public AutoCompleteTextBox()
        {
            InitializeComponent();

            //Example 
            List<string> list = new List<string>();
            list.Add("192.168.1.1");
            list.Add("192.168.1.12");
            list.Add("192.168.1.35");


            ItemsSource = list;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(AutoCompleteTextBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.KeyDown += new KeyEventHandler(AutoCompleteTextBox_KeyDown);
            this.PreviewKeyDown += new KeyEventHandler(AutoCompleteTextBox_PreviewKeyDown);
            ItemList.PreviewMouseDown += new MouseButtonEventHandler(ItemList_PreviewMouseDown);
            ItemList.KeyDown += new KeyEventHandler(ItemList_KeyDown);

            this.AddHandler(TextBox.GotFocusEvent, (RoutedEventHandler)delegate { this.SelectAll(); });

        }

        void AutoCompleteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && ItemList.Items.Count > 0 && !(e.OriginalSource is ListBoxItem))
            {
                ItemList.Focus();
                ItemList.SelectedIndex = 0;
                ListBoxItem lbi = ItemList.ItemContainerGenerator.ContainerFromIndex(ItemList.SelectedIndex) as ListBoxItem;
                lbi.Focus();
                e.Handled = true;
            }
        }


        void ItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {

                ListBoxItem tb = e.OriginalSource as ListBoxItem;
                Text = (tb.Content as string);
                if (e.Key == Key.Enter)
                {
                    Popup.IsOpen = false;
                    updateSource();
                }

            }
        }


        void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Popup.IsOpen = false;
                updateSource();
            }
        }

        void updateSource()
        {
            if (this.GetBindingExpression(TextBox.TextProperty) != null)
                this.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        void ItemList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TextBlock tb = e.OriginalSource as TextBlock;
                if (tb != null)
                {
                    Text = tb.Text;
                    updateSource();
                    Popup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            try
            {
                Popup.IsOpen = true;

                ItemList.Items.Filter = p =>
                {
                    string path = p as string;
                    return path.StartsWith(this.Text, StringComparison.CurrentCultureIgnoreCase) &&
                        !(String.Equals(path, this.Text, StringComparison.CurrentCultureIgnoreCase));
                };
            }
            catch
            {

            }
        }


        private string[] Lookup(string path)
        {
            /*try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    DirectoryInfo lookupFolder = new DirectoryInfo(Path.GetDirectoryName(path));
                    if (lookupFolder != null)
                    {
                        DirectoryInfo[] AllItems = lookupFolder.GetDirectories();
                        return (from di in AllItems where di.FullName.StartsWith(path, StringComparison.CurrentCultureIgnoreCase) select di.FullName).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
            }*/
            return new string[0];
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = false;
        }
    }
}
