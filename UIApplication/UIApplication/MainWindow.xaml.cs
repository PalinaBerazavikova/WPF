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
using System.Drawing;
using System.Globalization;
using System.Reflection;

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
        private string PathToSelectedType;
        private List<string> Files = new List<string>();
        private List<string> ListOfTypes = new List<string>();
        Assembly Lib;
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
            DirectoryPath.Width = 5 * dialog.SelectedPath.Length;
            DirectoryPath.Text = dialog.SelectedPath;
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



        private bool IsFilesFounded()
        {
            string[] files = { };
            try
            {
                files = Directory.GetFiles(this.SelectedPath);
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("This directory doesnt exist!");
                return false;
            }
            catch (System.ArgumentException e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Please, enter directory!");
                return false;
            }
            catch (System.NotSupportedException e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Not supported directory!");
                return false;
            }
            
            foreach (var file in files)
            {
                if (System.IO.Path.GetExtension(file).ToLower().Equals(".dll"))
                {
                    this.Files.Add(System.IO.Path.GetFileName(file));
                }
            }
            return true;

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Files.Clear();
            listBoxTypes.Items.Clear();
            this.SelectedPath = DirectoryPath.Text;
            listBox.Items.Clear();
            if (IsFilesFounded())
            {
                SetFilesToList();
            }
        }

        public void SetFilesToList()
        {
            foreach (var file in this.Files)
            {
                listBox.Items.Add(file);
            }

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listBox.SelectedItem != null)
                {
                    listBoxTypes.Items.Clear();
                    PathToSelectedType = System.IO.Path.Combine(this.SelectedPath, listBox.SelectedItem.ToString());
                    Assembly lib = Assembly.LoadFile(PathToSelectedType);
                    ListOfTypes = lib.GetTypes().Select(t => t.FullName).ToList();
                    foreach (var type in ListOfTypes)
                    {
                        this.listBoxTypes.Items.Add(type);
                    }
                }
            }
            catch (BadImageFormatException bife)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("File is empty!");
            }

        }
        private void ShowItems()
        {
            listBoxTypesMethods.Items.Clear();
            listBoxTypesFields.Items.Clear();
            listBoxTypesProperty.Items.Clear();
            if (listBoxTypes.SelectedItem != null)
            {
                var type = Lib.GetTypes().Where(t => t.FullName == listBoxTypes.SelectedItem.ToString()).FirstOrDefault();
                if (checkBoxMethod.IsChecked == true)
                {
                    var ListTypeMethodsName = type.GetMethods().Select(m => m.Name).ToList();
                    foreach (var method in ListTypeMethodsName)
                    {
                        this.listBoxTypesMethods.Items.Add(method);
                    }
                }
                if (checkBoxFields.IsChecked == true)
                {
                    var ListTypeFieldsName = type.GetFields().Select(f => f.Name).ToList();
                    foreach (var field in ListTypeFieldsName)
                    {
                        this.listBoxTypesFields.Items.Add(field);
                    }
                }
                if (checkBoxProperty.IsChecked == true)
                {
                    var ListTypePropertiesName = type.GetProperties().Select(p => p.Name).ToList();
                    foreach (var property in ListTypePropertiesName)
                    {
                        this.listBoxTypesProperty.Items.Add(property);
                    }
                }
            }
            
        }
        private void listBoxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxTypes.SelectedItem != null)
            {
                listBoxTypesMethods.Items.Clear();
                listBoxTypesFields.Items.Clear();
                listBoxTypesProperty.Items.Clear();
                Lib = Assembly.LoadFile(PathToSelectedType);
                ShowItems();
            }

        }
        private void setValues(Type type, System.Windows.Controls.ListBox list, System.Windows.Controls.CheckBox checkBox)
        {
            if (checkBoxMethod.IsChecked == true)
            {
                var ListTypeName = type.GetProperties().Select(p => p.Name).ToList();
                foreach (var value in ListTypeName)
                {
                    list.Items.Add(value);
                }
            }else
            {
                list.Items.Clear();
            }
        }
        private void checkBoxProperty_Checked(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }

        private void checkBoxFields_Checked(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }

        private void checkBoxMethod_Checked(object sender, RoutedEventArgs e)
        {
            ShowItems();
        }
        private void checkBoxProperty_Unchecked(object sender, RoutedEventArgs e)
        {
            listBoxTypesProperty.Items.Clear();
        }

        private void checkBoxFields_Unchecked(object sender, RoutedEventArgs e)
        {
            listBoxTypesFields.Items.Clear();
        }

        private void checkBoxMethod_Unchecked(object sender, RoutedEventArgs e)
        {
            listBoxTypesMethods.Items.Clear();

        }
    }
}
