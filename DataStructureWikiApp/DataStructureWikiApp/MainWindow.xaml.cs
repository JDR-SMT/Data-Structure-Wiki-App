using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
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

namespace DataStructureWikiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // global variables
        string[,] DataStructures = new string[12, 4];
        int ptr = 0;

        #region Utility
        private void DisplayArray()
        {
            ListViewOutput.Items.Clear();

            for (int row = 0; row < RowLength(); row++)
            {
                List<Pop> pops = new List<Pop>
                {
                    new Pop() { Name = DataStructures[row, 0], Category = DataStructures[row, 1] }
                };
                ListViewOutput.Items.Add(pops);
            }
        }

        private int RowLength()
        {
            return DataStructures.GetLength(0);
        }

        private int ColumnLength()
        {
            return DataStructures.GetLength(1);
        }

        private void ClearFocus()
        {
            TextBoxInput.Clear();
            TextBoxInput.Focus();
        }

        private void ClearAll()
        {
            TextBoxName.Clear();
            TextBoxCategory.Clear();
            TextBoxStructure.Clear();
            TextBoxDefinition.Clear();
        }

        private void ListViewOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBoxName.Text = DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 0];
                TextBoxCategory.Text = DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 1];
                TextBoxStructure.Text = DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 2];
                TextBoxDefinition.Text = DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 3];
            }
            catch (IndexOutOfRangeException) // when a row is deleted
            {
                ClearAll();
                return;
            }
        }

        private void TextBoxName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Add
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxStructure.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxDefinition.Text))
            {
                if (ptr < RowLength())
                {
                    DataStructures[ptr, 0] = TextBoxName.Text;
                    DataStructures[ptr, 1] = TextBoxCategory.Text;
                    DataStructures[ptr, 2] = TextBoxStructure.Text;
                    DataStructures[ptr, 3] = TextBoxDefinition.Text;

                    ptr++;

                    ClearAll();
                    BubbleSort();
                }
                else // if array is full
                { 
                    MessageBox.Show("Item limit reached.");
                }
            }
            else // if any TextBox is empty
            { 
                MessageBox.Show("Please complete all fields.");
            }
        }
        #endregion

        #region Edit
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Focus();
            ButtonVisibility(false, true, false); 
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 0] = TextBoxName.Text;
            DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 1] = TextBoxCategory.Text;
            DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 2] = TextBoxStructure.Text;
            DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 3] = TextBoxDefinition.Text;

            ClearAll();
            ClearFocus();
            ButtonVisibility(true, false, true); 
            BubbleSort();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ListViewOutput.SelectedItem = null;
            ClearAll();
            ClearFocus();
            ButtonVisibility(true, false, true);
        }

        private void ButtonVisibility(bool a, bool b, bool c)
        {
            if (a == false && b == true)
            {
                ButtonEdit.Visibility = Visibility.Hidden;
                ButtonApply.Visibility = Visibility.Visible;
                ButtonCancel.Visibility = Visibility.Visible;
            }

            if (a == true && b == false)
            {
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonApply.Visibility = Visibility.Hidden;
                ButtonCancel.Visibility = Visibility.Hidden;
            }

            ButtonOpen.IsEnabled = c;
            ButtonSave.IsEnabled = c;
            ButtonBinarySearch.IsEnabled = c;
            ButtonAdd.IsEnabled = c;
            ButtonDelete.IsEnabled = c;
        }
        #endregion

        #region Delete
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Bubble Sort
        private void BubbleSort()
        {

        }

        private void Swap(int a, int b)
        {

        }
        #endregion

        #region Binary Search
        private void ButtonBinarySearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BinarySearch(string[,] arr, string target)
        {

        }
        #endregion

        #region Open
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "data files (*.dat)|*.dat" };

            // if ok
            if (ofd.ShowDialog() == true)
            {
                // calls OpenFile with selected file
                OpenFile(ofd.FileName);

                BubbleSort();
            }
            else
            { // if cancel or exit
                return;
            }
        }

        private void OpenFile(string file)
        {
            try
            {
                Array.Clear(DataStructures);

                int row = 0;

                using (BinaryReader br = new BinaryReader(new FileStream(file, FileMode.Open)))
                {
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        DataStructures[row, 0] = br.ReadString();
                        DataStructures[row, 1] = br.ReadString();
                        DataStructures[row, 2] = br.ReadString();
                        DataStructures[row, 3] = br.ReadString();

                        row++;
                    }
                }
                ptr = row;
            }
            catch (Exception ex) // when file cannot be read from
            {
                MessageBox.Show("Exception thrown: " + ex);
            }
        }
        #endregion

        #region Save
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = "dat",
                Filter = "data files (*.dat)|*.dat"
            };

            // if ok
            if (sfd.ShowDialog() == true)
            {
                BubbleSort();

                // calls SaveFile with selected file
                SaveFile(sfd.FileName);
            }
            else
            { // if cancel or exit
                return;
            }
        }

        private void SaveFile(string file)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.Create)))
                {
                    for (int row = 0; row < ptr; row++)
                    {
                        bw.Write(DataStructures[row, 0]);
                        bw.Write(DataStructures[row, 1]);
                        bw.Write(DataStructures[row, 2]);
                        bw.Write(DataStructures[row, 3]);
                    }
                }
            }
            catch (Exception ex) // when file cannot be written to
            {
                MessageBox.Show("Exception thrown: " + ex);
            }
        }
        #endregion
    }

    #region Populate
    // class is used for data binding ListViewOutput columns
    public class Pop
    {
        public string? Name
        {
            get; set;
        }

        public string? Category
        {
            get; set;
        }
    }
    #endregion
}
