using System;
using System.Collections.Generic;
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

namespace Mikhailov_Glazki_save
{
    /// <summary>
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();

            var currentServices=Mikhailov_GlazkiEntities.GetContext().Agent.ToList();

            AgentListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;
            ComboType1.SelectedIndex = 0;

            UpdateServices();
            
        }
        private void UpdateServices()
        {
            var currentServices = Mikhailov_GlazkiEntities.GetContext().Agent.ToList();

            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", "").ToLower().Contains(TBoxSearch.Text.Replace(" ", "").Replace(")", "").Replace("(", "").Replace("-", "").ToLower()) || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            

            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == p.AgentTypeID)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 1)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 2)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 3)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 4)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 5)).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 6)).ToList();
            }

            if (ComboType1.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.Title == p.Title)).ToList();
            }
            if (ComboType1.SelectedIndex == 1)
            {
                currentServices = currentServices.OrderBy(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 2)
            {
                currentServices = currentServices.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 3)
            {

            }
            if (ComboType1.SelectedIndex == 4)
            {

            }
            if (ComboType1.SelectedIndex == 5)
            {
                currentServices = currentServices.OrderBy(p => p.Priority).ToList();
            }
            if (ComboType1.SelectedIndex == 6)
            {
                currentServices = currentServices.OrderByDescending(p => p.Priority).ToList();
            }

            AgentListView.ItemsSource = currentServices;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void ComboType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
    }
}
