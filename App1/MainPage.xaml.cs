using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        EmployeeClient webServeice = new EmployeeClient();

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            getEmployee();
        }

        async void getEmployee()
        {
            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                GridViewEmployee.ItemsSource = await webServeice.GetProductListAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
            catch (Exception ex)
            {

                MessageDialog m = new MessageDialog(ex.Message);
                await m.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }

        private async void GridViewEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try
            {
                if(e.AddedItems.Count != 0)
                {
                    Employee selectedEmployee = e.AddedItems[0] as Employee;
                    TextBoxName.Text = selectedEmployee.firstName;
                    TextBoxSurName.Text = selectedEmployee.lastName;
                    TextBoxCity.Text = selectedEmployee.address;
                    TextBoxAge.Text = selectedEmployee.Age.ToString();
                }
            }
            catch
            {
                MessageDialog m = new MessageDialog("Error data!");
                await m.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e) {

            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                Employee emp = new Employee();
                emp.firstName = TextBoxName.Text;
                emp.lastName = TextBoxSurName.Text;
                emp.address = TextBoxCity.Text;
                emp.Age =  Int32.Parse(TextBoxAge.Text);
                bool result = await webServeice.AddEmployeeAsync(emp);
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                if (result == true)
                {
                    MessageDialog ms = new MessageDialog("Insert Successfully");
                    await ms.ShowAsync();
                    Reset();
                }
                else
                {
                    MessageDialog ms = new MessageDialog("Can't Insert!");
                    await ms.ShowAsync();
                }
                getEmployee();

            }
            catch (Exception ex)
            {
                MessageDialog m = new MessageDialog(ex.Message);
                await m.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e) {
            if (GridViewEmployee.SelectedItem != null) {

                try
                {
                    ProgressBar.IsIndeterminate = true;
                    ProgressBar.Visibility = Visibility.Visible;
                    bool result = await webServeice.DeleteEmployeeAsync((GridViewEmployee.SelectedItem as Employee).empID);
                    if (result = true)
                    {
                        MessageDialog m = new MessageDialog("Can't Delete!");
                        await m.ShowAsync();
                        Reset();
                    }
                    else
                    {
                        MessageDialog m = new MessageDialog("Can't Delete!");
                        await m.ShowAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog m = new MessageDialog(ex.Message);
                    await m.ShowAsync();
                    ProgressBar.Visibility = Visibility.Collapsed;
                    ProgressBar.IsIndeterminate = false;
                }
            }
            else
            {
                MessageDialog ms = new MessageDialog("Choice record to delete!");
                await ms.ShowAsync();
            }
          
        }

        private async void ButtonModify_Click(object sender, RoutedEventArgs e) {

            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                Employee emp = new Employee();
                emp.empID = (GridViewEmployee.SelectedItem as Employee).empID;
                emp.firstName = TextBoxName.Text;
                emp.lastName = TextBoxSurName.Text;
                emp.address = TextBoxCity.Text;
                emp.Age = Int32.Parse(TextBoxAge.Text);
                bool result = await webServeice.UpdateEmployeeAsync(emp);
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                if (result == true)
                {
                    MessageDialog ms = new MessageDialog("Edited Successfully");
                    await ms.ShowAsync();
                    Reset();
                }
                else
                {
                    MessageDialog ms = new MessageDialog("Can't Edited!");
                    await ms.ShowAsync();
                }
                getEmployee();

            }
            catch
            {
                MessageDialog m = new MessageDialog("Choice Employee!");
                await m.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }

        }

        void Reset() {
            TextBoxName.Text = string.Empty;
            TextBoxSurName.Text = string.Empty;
            TextBoxCity.Text = string.Empty;
            TextBoxAge.Text = string.Empty;
        }
    }
}
