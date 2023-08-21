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

        }

        private void RowLength()
        {

        }

        private void ColumnLength()
        {

        }

        private void ClearFocus()
        {

        }

        private void ClearAll()
        {

        }

        private void ListViewOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBoxName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Add
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Edit
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonVisibility(bool a, bool b, bool c)
        {

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

        }

        private void OpenFile(string file)
        {

        }
        #endregion

        #region Save
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFile(string file)
        {

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
