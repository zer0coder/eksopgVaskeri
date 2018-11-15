using ModelsAndContext.Models;
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

namespace VaskeriAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager dbm;
        private WasherService currentService;
        private User currentUser;

        public MainWindow()
        {
            InitializeComponent();
            LoadDBData();
        }

        private void LoadDBData()
        {
            dbm = new DatabaseManager();
            ServiceList.ItemsSource = dbm.GetWasherServices();
        }

        private void ServiceSelected(object sender, SelectionChangedEventArgs e)
        {
            WasherService service = (WasherService)ServiceList.SelectedItem;

            if (service != null)
            {
                currentService = service;
                RefreshUsers();
            }
        }

        private void UserSelected(object sender, SelectionChangedEventArgs e)
        {
            User user = (User)UserList.SelectedItem;

            if (user != null)
            {
                UserDetail.DataContext = user;
                currentUser = user;
                RefreshReservations();
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            string un = tbUsername.Text;
            string alias = tbAlias.Text;
            string pass = "123456789";

            User user = new User() { Username = un, Password = pass, Alias = alias, ServiceID = currentService.Id };
            dbm.AddNewUser(user);
        }

        private void RefreshUsers()
        {
            if (currentService != null)
            {
                List<User> users = dbm.GetUsers(currentService);
                UserList.ItemsSource = users;
            }
        }

        private void RefreshReservations()
        {
            if(currentUser != null)
            {
                List<DoneReservation> list = dbm.GetDoneReservations(currentUser);
                lvUserReservations.ItemsSource = list;
                RefreshKonti(list);
            }
        }

        private void RefreshKonti(List<DoneReservation> list)
        {
            float cost = 0f;

            foreach (var item in list)
            {
                cost += item.Cost;
            }

            tbKonti.Text = cost.ToString();
        }
    }
}
