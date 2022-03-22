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
using System.Data.OleDb;
using System.Data;
using System.Configuration;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection Con_DB = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hotel.mdb");
        OleDbDataAdapter DA_DB;
        OleDbCommandBuilder CB_DB;
        OleDbCommand Command_DB;
        DataSet DS_DB;

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
        }

        private void Update_Data_Grid()
        {
            Con_DB.Open();
            DS_DB = new DataSet();
            DA_DB = new OleDbDataAdapter("select * from Rooms", Con_DB);
            CB_DB = new OleDbCommandBuilder(DA_DB);
            DA_DB.Fill(DS_DB, "Rooms");
            DA_DB = new OleDbDataAdapter("select * from Clients", Con_DB);
            CB_DB = new OleDbCommandBuilder(DA_DB);
            DA_DB.Fill(DS_DB, "Clients");
            Con_DB.Close();
            
            RoomDataGrid.ItemsSource = DS_DB.Tables["Rooms"].DefaultView;
            ClientDataGrid.ItemsSource = DS_DB.Tables["Clients"].DefaultView;
        }

        public MainWindow()
        {
            InitializeComponent();

            Update_Data_Grid();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?",
                                                      "Подтверждение выхода",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)tabControl.Items[0]).IsEnabled = false;
            ((TabItem)tabControl.Items[1]).IsEnabled = false;
            ((TabItem)tabControl.Items[2]).Visibility = Visibility.Visible;
            tabControl.SelectedItem = tabControl.Items[2];
        }

        private void CancellRoomButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Внесенные изменения не сохранятся",
                                                      "Подтверждение",
                                                      MessageBoxButton.OKCancel,
                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[2]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[0];
                RoomName_Add_textBox.Text = "";
                RoomType_Add_textBox.Text = "";
                RoomNum_Add_textBox.Text = "";
                RoomCost_Add_textBox.Text = "";
            }
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)tabControl.Items[0]).IsEnabled = false;
            ((TabItem)tabControl.Items[1]).IsEnabled = false;
            ((TabItem)tabControl.Items[4]).Visibility = Visibility.Visible;
            tabControl.SelectedItem = tabControl.Items[4];
            RoomName_Change_textBox.Text = ((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[1].ToString();
            RoomType_Change_textBox.Text = ((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[2].ToString();
            RoomNum_Change_textBox.Text = ((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[3].ToString();
            RoomCost_Change_textBox.Text = ((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[4].ToString();
        }

        private void CancellRoomChangeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Внесенные изменения не сохранятся",
                                                      "Подтверждение",
                                                      MessageBoxButton.OKCancel,
                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[4]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[0];
                RoomName_Change_textBox.Text = "";
                RoomType_Change_textBox.Text = "";
                RoomNum_Change_textBox.Text = "";
                RoomCost_Change_textBox.Text = "";
            }
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)tabControl.Items[0]).IsEnabled = false;
            ((TabItem)tabControl.Items[1]).IsEnabled = false;
            ((TabItem)tabControl.Items[3]).Visibility = Visibility.Visible;
            tabControl.SelectedItem = tabControl.Items[3];
        }

        private void CancellClientButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Внесенные изменения не сохранятся",
                                                      "Подтверждение",
                                                      MessageBoxButton.OKCancel,
                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[3]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[1];
                ClientRoom_Add_textBox.Text = "";
                ClientSurname_Add_textBox.Text = "";
                ClientName_Add_textBox.Text = "";
                ClientMiddleName_Add_textBox.Text = "";
                ClientSeries_Add_textBox.Text = "";
                ClientPassportID_Add_textBox.Text = "";
                datePicker1.Text = "";
                datePicker2.Text = "";
            }
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)tabControl.Items[0]).IsEnabled = false;
            ((TabItem)tabControl.Items[1]).IsEnabled = false;
            ((TabItem)tabControl.Items[5]).Visibility = Visibility.Visible;
            tabControl.SelectedItem = tabControl.Items[5];
            ClientRoom_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[1].ToString();
            ClientSurname_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[2].ToString();
            ClientName_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[3].ToString();
            ClientMiddleName_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[4].ToString();
            ClientSeries_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[5].ToString();
            ClientPassportID_Change_textBox.Text = ((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[6].ToString();
            datePicker3.SelectedDate = DateTime.Parse(((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[7].ToString());
            datePicker4.SelectedDate = DateTime.Parse(((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[8].ToString());
        }

        private void CancellClientChangeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Внесенные изменения не сохранятся",
                                                      "Подтверждение",
                                                      MessageBoxButton.OKCancel,
                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[5]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[1];
                ClientRoom_Change_textBox.Text = "";
                ClientSurname_Change_textBox.Text = "";
                ClientName_Change_textBox.Text = "";
                ClientMiddleName_Change_textBox.Text = "";
                ClientSeries_Change_textBox.Text = "";
                ClientPassportID_Change_textBox.Text = "";
                datePicker3.Text = "";
                datePicker4.Text = "";
            }
        }

        private void AcceptRoomButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Con_DB.Open();
                Command_DB = new OleDbCommand(@"INSERT INTO Rooms(Название_номера, Тип_номера, Количество_мест, Цена) VALUES(@Name, @Type, Num, Cost)", Con_DB);
                Command_DB.Parameters.AddWithValue("Name", RoomName_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("Type", RoomType_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("Num", Convert.ToInt32(RoomNum_Add_textBox.Text));
                Command_DB.Parameters.AddWithValue("Cost", Convert.ToInt32(RoomCost_Add_textBox.Text));
                Command_DB.ExecuteNonQuery();
                Con_DB.Close();
                Update_Data_Grid();
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[2]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[0];
                RoomName_Add_textBox.Text = "";
                RoomType_Add_textBox.Text = "";
                RoomNum_Add_textBox.Text = "";
                RoomCost_Add_textBox.Text = "";
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные\nДанные не добавлены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AcceptСlientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Con_DB.Open();
                Command_DB = new OleDbCommand(@"INSERT INTO Clients(Счетчик_номера, Фамилия, Имя, Отчество, Серия_паспорта, №_паспорта, Дата_заселения, Дата_выселения) VALUES(Room, @Surname, @Name, @MiddleName, @Series, @PassportID, DateIn, DateOut)", Con_DB);
                Command_DB.Parameters.AddWithValue("Room", Convert.ToInt32(ClientRoom_Add_textBox.Text));
                Command_DB.Parameters.AddWithValue("Surname", ClientSurname_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("Name", ClientName_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("MiddleName", ClientMiddleName_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("Series", ClientSeries_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("PassportID", ClientPassportID_Add_textBox.Text);
                Command_DB.Parameters.AddWithValue("DateIn", datePicker1.SelectedDate);
                Command_DB.Parameters.AddWithValue("DateOut", datePicker2.SelectedDate);
                Command_DB.ExecuteNonQuery();
                Con_DB.Close();
                Update_Data_Grid();
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[3]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[1];
                ClientRoom_Add_textBox.Text = "";
                ClientSurname_Add_textBox.Text = "";
                ClientName_Add_textBox.Text = "";
                ClientMiddleName_Add_textBox.Text = "";
                ClientSeries_Add_textBox.Text = "";
                ClientPassportID_Add_textBox.Text = "";
                datePicker1.Text = "";
                datePicker2.Text = "";
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные\nДанные не добавлены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveRoomButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить гостничный номер?",
                                                      "Подтверждение удаления",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Con_DB.Open();
                    Command_DB = new OleDbCommand($"DELETE FROM Rooms WHERE Счетчик_номера = {Convert.ToInt32(((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[0].ToString())}", Con_DB);
                    Command_DB.ExecuteNonQuery();
                    Con_DB.Close();
                    Update_Data_Grid();
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить данный гостиничный номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
            

        private void RemoveClientButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить данного постояльца?",
                                                      "Подтверждение удаления",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Con_DB.Open();
                    Command_DB = new OleDbCommand($"DELETE FROM Clients WHERE Счетчик_постояльца = {Convert.ToInt32(((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[0].ToString())}", Con_DB);
                    Command_DB.ExecuteNonQuery();
                    Con_DB.Close();
                    Update_Data_Grid();
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить данного постояльца", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AcceptRoomChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Con_DB.Open();
                Command_DB = new OleDbCommand($"UPDATE Rooms SET Название_номера = \"{RoomName_Change_textBox.Text}\", Тип_номера = \"{RoomType_Change_textBox.Text}\", Количество_мест = {Convert.ToInt32(RoomNum_Change_textBox.Text)}, Цена = {Convert.ToInt32(RoomCost_Change_textBox.Text)} WHERE Счетчик_номера={Convert.ToInt32(((DataRowView)RoomDataGrid.SelectedItem).Row.ItemArray[0].ToString())}", Con_DB);
                Command_DB.ExecuteNonQuery();
                Con_DB.Close();
                Update_Data_Grid();
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[4]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[0];
                RoomName_Change_textBox.Text = "";
                RoomType_Change_textBox.Text = "";
                RoomNum_Change_textBox.Text = "";
                RoomCost_Change_textBox.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно изменить данные гостиничного номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AcceptClientChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Con_DB.Open();
                Command_DB = new OleDbCommand($"UPDATE Clients SET Счетчик_номера = {Convert.ToInt32(ClientRoom_Change_textBox.Text)}, Фамилия = \"{ClientSurname_Change_textBox.Text}\", Имя = \"{ClientName_Change_textBox.Text}\", Отчество = \"{ClientMiddleName_Change_textBox.Text}\", Серия_паспорта = \"{ClientSeries_Change_textBox.Text}\", №_паспорта = \"{ClientPassportID_Change_textBox.Text}\", Дата_заселения = \"{datePicker3.SelectedDate}\", Дата_выселения = \"{datePicker4.SelectedDate}\" WHERE Счетчик_постояльца = {Convert.ToInt32(((DataRowView)ClientDataGrid.SelectedItem).Row.ItemArray[0].ToString())}", Con_DB);
                Command_DB.ExecuteNonQuery();
                Con_DB.Close();
                Update_Data_Grid();
                ((TabItem)tabControl.Items[0]).IsEnabled = true;
                ((TabItem)tabControl.Items[1]).IsEnabled = true;
                ((TabItem)tabControl.Items[5]).Visibility = Visibility.Hidden;
                tabControl.SelectedItem = tabControl.Items[1];
                ClientRoom_Change_textBox.Text = "";
                ClientSurname_Change_textBox.Text = "";
                ClientName_Change_textBox.Text = "";
                ClientMiddleName_Change_textBox.Text = "";
                ClientSeries_Change_textBox.Text = "";
                ClientPassportID_Change_textBox.Text = "";
                datePicker3.Text = "";
                datePicker4.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно изменить данные постояльца", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
