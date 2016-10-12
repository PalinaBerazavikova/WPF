using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.IO;
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
            var windowInterpopHelper = new WindowInteropHelper(this).Handle;
            SetWindowLong(windowInterpopHelper, GWL_STYLE, GetWindowLong(windowInterpopHelper, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChooseDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            DirectoryPathTextBox.Text = dialog.SelectedPath;
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

        private bool IsDirectoryValid()
        {
            try
            {
                Directory.GetFiles(this.SelectedPath);
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
            return true;
        }


        private void FindFiles()
        {
            foreach (var file in Directory.GetFiles(this.SelectedPath))
            {
                if (System.IO.Path.GetExtension(file).ToLower().Equals(".dll"))
                {
                    this.Files.Add(System.IO.Path.GetFileName(file));
                }
            }
            SetToListBox(this.Files, listBox);
        }
        private void ClearBoxes()
        {
            listBoxTypes.Items.Clear();
            listBox.Items.Clear();
            listBoxTypesMethods.Items.Clear();
            listBoxTypesFields.Items.Clear();
            listBoxTypesProperty.Items.Clear();
        }
        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Files.Clear();
            ClearBoxes();
            this.SelectedPath = DirectoryPathTextBox.Text;
            if (IsDirectoryValid())
            {
                FindFiles();
            }
        }

        public void SetToListBox(List<string> list,ListBox listBox)
        {
            foreach (var value in list)
            {
                listBox.Items.Add(value);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxTypesMethods.Items.Clear();
            listBoxTypesFields.Items.Clear();
            listBoxTypesProperty.Items.Clear();
            try
            {
                if (listBox.SelectedItem != null)
                {
                    listBoxTypes.Items.Clear();
                    PathToSelectedType = System.IO.Path.Combine(this.SelectedPath, listBox.SelectedItem.ToString());
                    Assembly lib = Assembly.LoadFile(PathToSelectedType);
                    ListOfTypes = lib.GetTypes().Select(t => t.FullName).ToList();
                    SetToListBox(ListOfTypes, listBoxTypes);
                }
            }
            catch (BadImageFormatException)
            {
                System.Windows.MessageBox.Show("File is empty!");
            }

        }
        private void SetItemsToTextBoxes()
        {
            listBoxTypesMethods.Items.Clear();
            listBoxTypesFields.Items.Clear();
            listBoxTypesProperty.Items.Clear();
            if (listBoxTypes.SelectedItem != null)
            {
                var type = Lib.GetTypes().Where(t => t.FullName == listBoxTypes.SelectedItem.ToString()).FirstOrDefault();
                if (checkBoxMethod.IsChecked == true || checkBoxAll.IsChecked == true)
                {
                    var ListTypeMethodsName = type.GetMethods().Select(m => m.Name).ToList();
                    SetToListBox(ListTypeMethodsName, listBoxTypesMethods);
                }
                if (checkBoxFields.IsChecked == true || checkBoxAll.IsChecked == true)
                {
                    var ListTypeFieldsName = type.GetFields().Select(f => f.Name).ToList();
                    SetToListBox(ListTypeFieldsName, listBoxTypesFields);
                }
                if (checkBoxProperty.IsChecked == true || checkBoxAll.IsChecked == true)
                {
                    var ListTypePropertiesName = type.GetProperties().Select(p => p.Name).ToList();
                    SetToListBox(ListTypePropertiesName, listBoxTypesProperty);

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
                SetItemsToTextBoxes();
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
            }
            else
            {
                list.Items.Clear();
            }
        }
        private void checkBoxProperty_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxAll.IsChecked = false;
            SetItemsToTextBoxes();
        }

        private void checkBoxFields_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxAll.IsChecked = false;
            SetItemsToTextBoxes();
        }

        private void checkBoxMethod_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxAll.IsChecked = false;
            SetItemsToTextBoxes();
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

        private void checkBoxAll_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxFields.IsChecked = false;
            checkBoxMethod.IsChecked = false;
            checkBoxProperty.IsChecked = false;
            SetItemsToTextBoxes();
        }
        private void checkBoxAll_Unchecked(object sender, RoutedEventArgs e)
        {
            listBoxTypesMethods.Items.Clear();
            listBoxTypesFields.Items.Clear();
            listBoxTypesProperty.Items.Clear();
        }
    }
}
