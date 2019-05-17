using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для CashWindow.xaml
    /// </summary>
    public partial class EditPhotoWindow : Window
    {
        public EditPhotoWindow()
        {
            InitializeComponent();
            createContent();
        }



        private void createContent()
        {   
            /*ToggleButton[] tgs = new ToggleButton[10];
            for (int i = 0; i < tgs.Length; i++)
            {
                tgs[i] = new ToggleButton();
                tgs[i].Width = 370;
                tgs[i].Height = 370;
                tgs[i].Style = (Style)this.FindResource("PhotoItemButton");

                Image img = new Image();
                img.Source = doGetImageSourceFromResource("Terminal", "Images/pic1.png");
                img.Stretch = Stretch.Fill;
                tgs[i].Content = img;

                contentPanel.Children.Add(tgs[i]);
            }*/
        }

        static internal ImageSource doGetImageSourceFromResource(string psAssemblyName, string psResourceName)
        {
            Uri oUri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + psResourceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }
    }
}
