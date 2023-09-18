using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            // clears all items in ListViewOutput
            ListViewOutput.Items.Clear();

            // adds elements 0 and 1 from each row in the 2D array to the appropriate ListViewOutput columns
            for (int row = 0; row < RowLength(); row++)
            {
                List<ListViewItem> lvi = new List<ListViewItem>
                {
                    new ListViewItem() { Name = DataStructures[row, 0], Category = DataStructures[row, 1] }
                };
                ListViewOutput.Items.Add(lvi);
            }
        }

        // returns the row length of the 2D array
        private int RowLength()
        {
            return DataStructures.GetLength(0);
        }

        // returns the column length of the 2D array
        private int ColumnLength()
        {
            return DataStructures.GetLength(1);
        }

        // clears text from and sets focus on TextBoxInput
        private void ClearFocus()
        {
            TextBoxInput.Clear();
            TextBoxInput.Focus();
        }

        // clears text from TextBoxes Name, Category, Structure and Definition
        private void ClearAll()
        {
            TextBoxName.Clear();
            TextBoxCategory.Clear();
            TextBoxStructure.Clear();
            TextBoxDefinition.Clear();
        }

        // displays a selected row from ListViewOutput in TextBoxes Name, Category, Structure and Definition
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

        // calls ClearAll and sets focus on TextBoxName
        private void TextBoxName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearAll();
            TextBoxName.Focus();
        }
        #endregion

        #region Add
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // if TextBoxes Name, Category, Structure and Definition is not empty
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxStructure.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxDefinition.Text))
            {
                // if ptr is less than 2D array row length
                if (ptr < RowLength())
                {
                    // add TextBoxes Name, Category, Structure and Definition to ptr elements 0, 1, 2 and 3 in the 2D array
                    DataStructures[ptr, 0] = TextBoxName.Text;
                    DataStructures[ptr, 1] = TextBoxCategory.Text;
                    DataStructures[ptr, 2] = TextBoxStructure.Text;
                    DataStructures[ptr, 3] = TextBoxDefinition.Text;

                    // increment ptr
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
        // sets focus on TextBoxName
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Focus();
            ButtonVisibility(false, true, false); // show ButtonApply and ButtonCancel
        }

        // replaces selected row in ListViewOutput with text from TextBoxName, TextBoxCategory, TextBoxStructure and TextBoxDefinition
        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 0] = TextBoxName.Text;
                DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 1] = TextBoxCategory.Text;
                DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 2] = TextBoxStructure.Text;
                DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 3] = TextBoxDefinition.Text;

                ClearAll();
                ClearFocus();
                ButtonVisibility(true, false, true); // hide ButtonApply and ButtonCancel
                BubbleSort();
            }
            catch (IndexOutOfRangeException) // when no row is selected
            {
                MessageBox.Show("Please select an item to edit.");
                ClearAll();
                ButtonVisibility(true, false, true); // hide ButtonApply and ButtonCancel
                return;
            }
        }

        // clears selected row in ListViewOutput
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ListViewOutput.SelectedItem = null;
            ClearAll();
            ClearFocus();
            ButtonVisibility(true, false, true); // hide ButtonApply and ButtonCancel
        }

        private void ButtonVisibility(bool buttonEdit, bool buttonApplyCancel, bool enabled)
        {
            // if ButtonEdit is clicked
            if (buttonEdit == false && buttonApplyCancel == true)
            {
                ButtonEdit.Visibility = Visibility.Hidden;
                ButtonApply.Visibility = Visibility.Visible;
                ButtonCancel.Visibility = Visibility.Visible;
            }

            // if ButtonApply or ButtonCancel is clicked
            if (buttonEdit == true && buttonApplyCancel == false)
            {
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonApply.Visibility = Visibility.Hidden;
                ButtonCancel.Visibility = Visibility.Hidden;
            }

            ButtonOpen.IsEnabled = enabled;
            ButtonSave.IsEnabled = enabled;
            ButtonBinarySearch.IsEnabled = enabled;
            ButtonAdd.IsEnabled = enabled;
            ButtonDelete.IsEnabled = enabled;
        }
        #endregion

        #region Delete
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // display prompt
                MessageBoxResult result = MessageBox.Show("Delete this data structure?", "", MessageBoxButton.YesNo);

                // if yes, set elements in selected row from ListViewOutput as null
                if (result == MessageBoxResult.Yes)
                {
                    DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 0] = null;
                    DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 1] = null;
                    DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 2] = null;
                    DataStructures[ListViewOutput.Items.IndexOf(ListViewOutput.SelectedItem), 3] = null;

                    // decrease ptr
                    ptr--;

                    ClearAll();
                    BubbleSort();
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Please select an item to delete.");
                ClearAll();
                return;
            }
        }
        #endregion

        #region Bubble Sort
        private void BubbleSort()
        {
            for (int a = 0; a < RowLength(); a++)
            {
                for (int b = a + 1; b < RowLength(); b++)
                {
                    // if element a, 0 is after element b, 0 and neither is null, swap positions
                    if (DataStructures[a, 0] != null && DataStructures[b, 0] != null &&
                        string.Compare(DataStructures[a, 0], DataStructures[b, 0]) > 0)
                    {
                        Swap(a, b);
                    }
                    // if element a, 0 is null and element b, 0 is not null, swap positions
                    else if (DataStructures[a, 0] == null && DataStructures[b, 0] != null)
                    {
                        Swap(a, b);
                    }
                    DisplayArray();
                }
            }
        }

        private void Swap(int a, int b)
        {
            for (int col = 0; col < ColumnLength(); col++)
            {
                string temp = DataStructures[a, col];
                DataStructures[a, col] = DataStructures[b, col];
                DataStructures[b, col] = temp;
            }
        }
        #endregion

        #region Binary Search
        private void ButtonBinarySearch_Click(object sender, RoutedEventArgs e)
        {
            // sorts 2D array to ensure BinarySearch is functional
            BubbleSort();

            // TextBoxInput is not empty
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                // returns index of TextBoxInput in 2D array
                int index = BinarySearch(DataStructures, TextBoxInput.Text);

                if (index >= 0) // found
                {
                    MessageBox.Show(TextBoxInput.Text + " found.");
                    ListViewOutput.SelectedIndex = index;
                    ClearFocus();
                }
                else // not found
                {
                    MessageBox.Show(TextBoxInput.Text + " not found.");
                    ListViewOutput.SelectedIndex = -1;
                    ClearFocus();
                }
            }
            else // TextBoxInput is empty
            {
                MessageBox.Show("Please enter a word to search.");
                ClearFocus();
            }
        }

        private int BinarySearch(string[,] arr, string target)
        {
            List<string> list = new List<string>();

            // add all elements a, 0 from 2D array that are not null to list
            for (int a = 0; a < RowLength(); a++)
            {
                if (arr[a, 0] != null)
                {
                    list.Add(arr[a, 0]);
                }
            }
            // add list to temp array
            string[] temp = list.ToArray();

            int min = 0;
            int max = temp.Length - 1;

            while (min <= max)
            {
                int mid = (min + max) / 2;

                // compare element in temp array to target with ignoreCase
                int index = string.Compare(temp[mid], target, true);

                if (index == 0) // target exists
                {
                    return mid;
                }
                else if (index > 0) // search right
                {
                    max = mid - 1;
                }
                else if (index < 0) // search left
                {
                    min = mid + 1;
                }
            }
            return -1; // target does not exist
        }
        #endregion

        #region Open
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            // displays prompt to open a data file
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
                // clears all elements in 2D array
                Array.Clear(DataStructures);

                int row = 0;

                // reads file
                using (BinaryReader br = new BinaryReader(new FileStream(file, FileMode.Open)))
                {
                    // while position does not equal length of BaseStream
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        // read and add string to row elements 0, 1, 2 and 3 in the 2D array
                        DataStructures[row, 0] = br.ReadString();
                        DataStructures[row, 1] = br.ReadString();
                        DataStructures[row, 2] = br.ReadString();
                        DataStructures[row, 3] = br.ReadString();

                        row++;
                    }
                }

                // set ptr as row
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
            if (DataStructures[0, 0] == null)
            {
                MessageBox.Show("Please enter data to save.");
                return;
            }
            else
            {
                // displays prompt to save the 2D array to a data file
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
        }

        private void SaveFile(string file)
        {
            try
            {
                // creates file
                using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.Create)))
                {
                    // while row is less than ptr (to not include nulls)
                    for (int row = 0; row < ptr; row++)
                    {
                        if (DataStructures[row, 0] != null)
                        {
                            // writes row elements 0, 1, 2 and 3 into the 2D array
                            bw.Write(DataStructures[row, 0]);
                            bw.Write(DataStructures[row, 1]);
                            bw.Write(DataStructures[row, 2]);
                            bw.Write(DataStructures[row, 3]);
                        }
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

    #region ListViewItem
    // class is used for data binding ListViewOutput columns
    public class ListViewItem
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
