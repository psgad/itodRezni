using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace итоговая_резня
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public static int func;
        int vib = 0;
        public Window2()
        {

            InitializeComponent();
            data.ItemsSource = My.User_dg.Items;
            cbVis(naz3, "Сотрудник", cb3, 0, My.Worker_dg);
            vib = 2;
            reliz(func);
            och();
            data.SelectedIndex = -1;
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedIndex == -1)
                return;
            if ((data.SelectedItem as DataRowView) == null)
                return;
            if (data.SelectedItem != null)
            {
                if (vib == 2)
                {
                    Text1.Text = (data.SelectedItem as DataRowView)[1].ToString();
                    Text2.Text = (data.SelectedItem as DataRowView)[2].ToString();
                    cb3.SelectedItem = (data.SelectedItem as DataRowView)[3].ToString();
                }
                if (vib == 1)
                    Text1.Text = (data.SelectedItem as DataRowView)[1].ToString();
                if (vib == 3)
                {
                    Text1.Text = (data.SelectedItem as DataRowView)[0].ToString();
                    Text2.Text = (data.SelectedItem as DataRowView)[1].ToString();
                    Text3.Text = (data.SelectedItem as DataRowView)[2].ToString();
                    foreach (var y in My.Role_dg.Items)
                    {
                        if ((y as DataRowView) == null)
                            break;
                        if (Convert.ToInt32((y as DataRowView)[0]) == Convert.ToInt32((data.SelectedItem as DataRowView)[3]))
                        {
                            cb4.SelectedItem = (y as DataRowView)[1];
                            break;
                        }
                    }
                }
                if (vib == 4)
                {
                    Text1.Text = (data.SelectedItem as DataRowView)[1].ToString();
                    Text2.Text = (data.SelectedItem as DataRowView)[2].ToString();
                    Text3.Text = (data.SelectedItem as DataRowView)[3].ToString();
                }
                if (vib == 5)
                {
                    foreach (var y in My.Category_dg.Items)
                    {
                        if ((y as DataRowView) == null)
                            break;
                        if (Convert.ToInt32((data.SelectedItem as DataRowView)[0]) == Convert.ToInt32((y as DataRowView)[0]))
                        {
                            cb1.SelectedItem = (y as DataRowView)[1].ToString();
                        }
                    }
                    try
                    {
                        Text2.Text = (data.SelectedItem as DataRowView)[2].ToString();
                        Text3.Text = (data.SelectedItem as DataRowView)[3].ToString();
                        Text4.Text = (data.SelectedItem as DataRowView)[4].ToString();
                        Text5.Text = (data.SelectedItem as DataRowView)[5].ToString();
                        Text6.Text = (data.SelectedItem as DataRowView)[6].ToString();
                    }
                    catch { }
                }
            }
        }
        void reliz(int f)
        {
            switch (f)
            {
                case 1:
                    break;
                case 2:
                    bb();
                    data.ItemsSource = My.Stock_dg.Items;
                    vib = 4;
                    bu2.Visibility = Visibility.Hidden;
                    bu1.Content = "Склад";
                    bu3.Content = "Продукт";
                    viz(naz1, Text1, "Продукт");
                    viz(naz2, Text2, "Адрес");
                    viz(naz3, Text3, "Количество");
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vib == 2)
            {
                if (Text1.Text == "" || Text2.Text == "" || cb3.SelectedIndex == -1)
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                foreach (user y in MainWindow.users)
                {
                    if (Text1.Text == y.login)
                    {
                        MainWindow.n("Список уже содержит в себе такие данные ");
                        return;
                    }
                }
                My.user_do(1, data.SelectedIndex, Text1.Text.ToString(), Text2.Text.ToString(), cb3.SelectedItem.ToString());
                MainWindow.users.Add(new user(Text1.Text, Text2.Text));
            }
            if (vib == 1)
            {
                if (Text1.Text == "")
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                foreach (var y in data.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if ((y as DataRowView)[1].ToString() == Text1.Text)
                    {
                        MainWindow.n("Список уже содержит в себе такие данные ");
                        return;
                    }
                }
                My.role_do(1, -1, Text1.Text);
            }
            if (vib == 3)
            {
                foreach (var y in data.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if (Text1.Text == (y as DataRowView)[0].ToString() || Text2.Text == (y as DataRowView)[1].ToString() || Text3.Text == (y as DataRowView)[2].ToString())
                    {
                        MainWindow.n("Такие данные уже имеются");
                        return;
                    }
                }
                if (Text1.Text == "" || Text2.Text == "" || Text3.Text == "" || cb4.SelectedIndex == -1)
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                foreach (var y in My.Role_dg.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if (cb4.SelectedItem.ToString() == (y as DataRowView)[1].ToString())
                        My.worker_do(1, Text1.Text, Text2.Text, "", Convert.ToInt32(Text3.Text), Convert.ToInt32((y as DataRowView)[0]));
                }
            }
            if (vib == 4)
            {
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                My.stock_do(1, -1, Text1.Text, Text2.Text, Convert.ToInt32(Text3.Text));
            }
            if (vib == 5)
            {
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                foreach (var y in My.Category_dg.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if ((y as DataRowView)[1] == cb1.SelectedItem)
                    {
                         My.product_do(1, -1, Convert.ToInt32((y as DataRowView)[0]), Text2.Text, Convert.ToInt32(Text3.Text), Convert.ToInt32(Text4.Text), Convert.ToDouble(Text5.Text), Convert.ToDouble(Text6.Text));
                    }
                }
            }
            och();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (vib == 2)
            {
                if (Text1.Text == "" || Text2.Text == "" || cb3.SelectedIndex == -1)
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                foreach (user y in MainWindow.users)
                {
                    if (Text1.Text == y.login && Text2.Text == y.password && cb3.SelectedItem == (data.SelectedItem as DataRowView)[2])
                    {
                        MainWindow.n("Список уже содержит в себе такие данные ");
                        return;
                    }
                }
                My.user_do(3, Convert.ToInt32((data.SelectedItem as DataRowView)[0]), Text1.Text, Text2.Text, cb3.SelectedItem.ToString());
            }
            if (vib == 1)
            {
                if (Text1.Text == "")
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                foreach (var y in data.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if ((y as DataRowView)[1].ToString() == Text1.Text)
                    {
                        MainWindow.n("Список уже содержит в себе такие данные ");
                        return;
                    }
                }
                My.role_do(3, Convert.ToInt32((data.SelectedItem as DataRowView)[0]), Text1.Text);
            }
            if (vib == 3)
            {
                if (Text1.Text == "" || Text2.Text == "" || Text3.Text == "" || cb4.SelectedIndex == -1)
                {
                    MainWindow.n("Не все поля заполнены");
                    return;
                }
                foreach (var y in data.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if (Text1.Text == (y as DataRowView)[0].ToString() && Text2.Text == (y as DataRowView)[1].ToString() && Text3.Text == (y as DataRowView)[2].ToString() && cb4.SelectedItem == (data.SelectedItem as DataRowView)[3])
                    {
                        MainWindow.n("Такие данные уже имеются");
                        return;
                    }
                }
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                foreach (var y in My.Role_dg.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if (cb4.SelectedItem == (y as DataRowView)[1])
                    {
                        My.worker_do(3, Text1.Text, Text2.Text, (data.SelectedItem as DataRowView)[0].ToString(), Convert.ToInt32(Text3.Text), Convert.ToInt32((y as DataRowView)[0]));
                        break;
                    }
                }
            }
            if (vib == 4)
            {
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                My.stock_do(1, Convert.ToInt32((data.SelectedItem as DataRowView)[0]), Text1.Text, Text2.Text, Convert.ToInt32(Text3.Text));
            }
            if (vib == 6)
            {
                try
                {
                    if (Convert.ToInt32(Text3.Text) <= 0)
                    {
                        MainWindow.n("Введено недопустимое значение!");
                        och();
                        return;
                    }
                }
                catch
                {
                    MainWindow.n("Введено недопустимое значение!");
                    och();
                    return;
                }
                foreach (var y in My.Category_dg.Items)
                {
                    if ((y as DataRowView) == null)
                        break;
                    if ((y as DataRowView)[1] == cb1.SelectedItem)
                    {
                        My.product_do(1, Convert.ToInt32((data.SelectedItem as DataRowView)[0]), Convert.ToInt32((y as DataRowView)[0]), Text2.Text, Convert.ToInt32(Text3.Text), Convert.ToInt32(Text4.Text), Convert.ToDouble(Text5.Text), Convert.ToDouble(Text6.Text));
                    }
                }
            }
            och();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
            {
                MainWindow.n("Вы не выбрали поле");
                return;
            }
            if ((data.SelectedItem as DataRowView) == null)
            {
                MainWindow.n("Вы выбрали пустую строку ");
                return;
            }
            if (vib == 2)
            {

                My.user_do(2, Convert.ToInt32((data.SelectedItem as DataRowView)[0]));
            }
            if (vib == 1)
            {
                My.role_do(2, Convert.ToInt32((data.SelectedItem as DataRowView)[0]));
            }
            if (vib == 3)
            {
                My.worker_do(2, (data.SelectedItem as DataRowView)[0].ToString());
            }
            if (vib == 4)
            {
                My.stock_do(2, Convert.ToInt32((data.SelectedItem as DataRowView)[0]));
            }
            if (vib == 5)
            {
                My.product_do(2, Convert.ToInt32((data.SelectedItem as DataRowView)[0]));
            }
            och();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }
        void viz(Label l, TextBox tb, string n1)
        {
            tb.Visibility = Visibility.Visible;
            l.Content = n1;
        }
        void cbVis(Label l, string naz, ComboBox cb, int sel, DataGrid dg)
        {
            l.Content = naz;
            cb.Visibility = Visibility.Visible;
            cb.Items.Clear();
            foreach (var v in dg.Items)
            {
                if ((v as DataRowView) != null)
                    cb.Items.Add((v as DataRowView)[sel]);
            }
        }
        void bb()
        {
            Text1.Visibility = Visibility.Hidden;
            Text2.Visibility = Visibility.Hidden;
            Text3.Visibility = Visibility.Hidden;
            Text4.Visibility = Visibility.Hidden;
            Text5.Visibility = Visibility.Hidden;
            Text6.Visibility = Visibility.Hidden;
            cb1.Visibility = Visibility.Hidden;
            cb2.Visibility = Visibility.Hidden;
            cb3.Visibility = Visibility.Hidden;
            cb4.Visibility = Visibility.Hidden;
            cb5.Visibility = Visibility.Hidden;
            cb6.Visibility = Visibility.Hidden;
            naz1.Content = "";
            naz2.Content = "";
            naz3.Content = "";
            naz4.Content = "";
            naz5.Content = "";
            naz6.Content = "";
        }
        void och()
        {
            Text1.Text = "";
            Text2.Text = "";
            Text3.Text = "";
            Text4.Text = "";
            Text5.Text = "";
            Text6.Text = "";
            cb1.SelectedIndex = -1;
            cb2.SelectedIndex = -1;
            cb3.SelectedIndex = -1;
            cb4.SelectedIndex = -1;
            cb5.SelectedIndex = -1;
            cb6.SelectedIndex = -1;
            data.SelectedIndex = -1;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (func == 1)
            {
                bb();
                vib = 1;
                data.ItemsSource = My.Role_dg.Items;
                viz(naz1, Text1, "Название Роли");
                och();
                data.SelectedIndex = -1;
            }
            if (func == 2)
            {
                bb();
                vib = 4;
                data.ItemsSource = My.Stock_dg.Items;
                viz(naz1, Text1, "Продукт");
                viz(naz2, Text2, "Адрес");
                viz(naz3, Text3, "Количество");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (func == 1)
            {
                bb();
                vib = 2;
                data.ItemsSource = My.User_dg.Items;
                My.user_do();
                viz(naz1, Text1, "Логин");
                viz(naz2, Text2, "Пароль");
                cbVis(naz3, "Сотрудник", cb3, 0, My.Worker_dg);
                och();
                data.SelectedIndex = -1;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (func == 1)
            {
                bb();
                vib = 3;
                data.ItemsSource = My.Worker_dg.Items;
                viz(naz1, Text1, "Имя");
                viz(naz2, Text2, "Фамилия");
                viz(naz3, Text3, "Возраст");
                cbVis(naz4, "Роль", cb4, 1, My.Role_dg);
                och();
            }
            if (func == 2)
            {
                bb();
                vib = 5;
                data.ItemsSource = My.Product_dg.Items;
                bu2.Visibility = Visibility.Hidden;
                cbVis(naz1, "Категория", cb1, 1, My.Category_dg);
                viz(naz2, Text2, "Название");
                viz(naz3, Text3, "Рейтинг");
                viz(naz4, Text4, "Цена");
                viz(naz5, Text5, "НДС");
                viz(naz6, Text6, "Доподнительная скидка");
                och();
            }
        }
    }
}