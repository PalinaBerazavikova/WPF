using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace UIApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        private string SelectedPath;
        private List<string> Files = new List<string>();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);



        public MainWindow()
        {
            InitializeComponent();
            CenterWindowScreen();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChooseDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            this.SelectedPath = dialog.SelectedPath;
            SetDirectoryToTextBox();
        }
        private void CenterWindowScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void DirectoryPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox objTextBox = (System.Windows.Controls.TextBox)sender;
            this.SelectedPath = objTextBox.Text;
        }

        private void SetDirectoryToTextBox()
        {
            DirectoryPath.AppendText(this.SelectedPath);
        }

        private void FindFiles()
        {
            string[] files = Directory.GetFiles(this.SelectedPath);
            foreach(var file in files)
            {
                if (System.IO.Path.GetExtension(file).ToLower().Equals(".dll"))
                {
                    this.Files.Add(System.IO.Path.GetFileName(file));
                }
            }
            
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            FindFiles();
            SetFilesToList();
        }

        public void SetFilesToList()
        {
            ListBoxItem item = new ListBoxItem();
            foreach(var file in this.Files)
            {
                listBox.Items.Add(file);
            }
        }
    }
}
