using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Логика взаимодействия для SmileControl.xaml
    /// </summary>
    public partial class SmileControl : UserControl
    {
        public event DelegateString SelectedSmile;

        public SmileControl()
        {
            InitializeComponent();

            Emoticons = new EmoticonCollection
            {
                new EmoticonMapper
                {
                    Text=":happy-1:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/happy-1.png")
                },

                new EmoticonMapper
                {
                    Text=":angry-1:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/angry-1.png")
                },

                new EmoticonMapper
                {
                    Text=":angry:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/angry.png")
                },

                new EmoticonMapper
                {
                    Text=":bored-1:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/bored-1.png")
                },

                new EmoticonMapper
                {
                    Text=":bored-2:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/bored-2.png")
                },

                new EmoticonMapper
                {
                    Text=":bored:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/bored.png")
                },

                new EmoticonMapper
                {
                    Text=":confused-1:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/confused-1.png")
                },

                new EmoticonMapper
                {
                    Text=":confused:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/confused.png")
                },

                new EmoticonMapper
                {
                    Text=":crying-1:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/crying-1.png")
                },

                new EmoticonMapper
                {
                    Text=":crying:",
                    Icon = GetImageSource("pack://application:,,,/InstaSoft;component/Images/Smiles/crying.png")
                }


            };

            DataContext = this;
        }

        //public EmoticonCollection Emoticons { get; set; }

        public EmoticonCollection Emoticons
        {
            get { return (EmoticonCollection)GetValue(EmoticonsProperty); }
            set { SetValue(EmoticonsProperty, value); }
        }

        public static readonly DependencyProperty EmoticonsProperty =
            DependencyProperty.Register(
                "Emoticons",
                typeof(EmoticonCollection),
                typeof(SmileControl));

        private ImageSource GetImageSource(string url)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(url);
            img.EndInit();

            return img;
        }

        private void SmileButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            SelectedSmile?.Invoke(" " + button.Tag.ToString());
        }
    }
}
