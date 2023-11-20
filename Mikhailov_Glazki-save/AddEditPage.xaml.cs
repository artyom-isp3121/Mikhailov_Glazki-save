using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();

            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;
                ComboType.SelectedIndex = _currentAgent.AgentTypeID - 1;

            }

            DataContext = _currentAgent;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var context = Mikhailov_GlazkiEntities.GetContext();

            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            else if (context.Agent.Any(agent => agent.Title == _currentAgent.Title && agent.ID != _currentAgent.ID))
            {
                errors.AppendLine("Уже существует такая услуга");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
            {
                errors.AppendLine("Укажите адрес услуги");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
            {
                errors.AppendLine("Укажите ФИО директора");
            }
            if (ComboType.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }
            else
            {
                _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
            {
                errors.AppendLine("Укажите приоритет агента");
            }

            if (_currentAgent.Priority == 0)
            {
                errors.AppendLine("Укажите приоритет агента");
            }
            if (_currentAgent.Priority < 0)
            {
                errors.AppendLine("Укажите положительный приоритет агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
            {
                errors.AppendLine("Укажите ИНН агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
            {
                errors.AppendLine("Укажите КПП агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
            {
                errors.AppendLine("Укажите телефон агента");
            }
            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "").Replace(" ", "");
                if (ph.Length > 0)
                {
                    if ( ph[0] != '8' || ph.Length != 11 )
                        errors.AppendLine("Укажите правильно телефон агента");
                }

            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
            {
                errors.AppendLine("Укажите почту агента");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgent.ID == 0)
                Mikhailov_GlazkiEntities.GetContext().Agent.Add(_currentAgent);

            try
            {
                Mikhailov_GlazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;

            var currentProductSale = Mikhailov_GlazkiEntities.GetContext().ProductSale.ToList();
            currentProductSale = currentProductSale.Where(p => p.AgentID == currentAgent.ID).ToList();

            if (currentProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существуют записи на эту услугу");
            else
            {

                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Mikhailov_GlazkiEntities.GetContext().Agent.Remove(currentAgent);
                        Mikhailov_GlazkiEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }

        }
    }
}
