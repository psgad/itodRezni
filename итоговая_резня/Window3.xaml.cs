using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Shapes;
using итоговая_резня.online_shopDataSetTableAdapters;

namespace итоговая_резня
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        int uf = 0;
        public Window3()
        {
            InitializeComponent();
            data.ItemsSource = My.Product_dg.Items;
            order.ItemsSource = My.Pr_Or_dg.Items;
            foreach (var y in My.Product_dg.Items)
            {
                if ((y as DataRowView) == null)
                    break;
                cb.Items.Add((y as DataRowView)[2].ToString());
            }
            if (My.Order_dg.Items.Count == 1)
            {
                My.order_do(1, -1, 1, "Админ");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedIndex != -1)
            {
                cb.SelectedItem = (data.SelectedItem as DataRowView)[2];
                uf = Convert.ToInt32((data.SelectedItem as DataRowView)[0]);
                count.Text = "1";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (count.Text == "")
            {
                MainWindow.n("Не все поля заполнены");
                return;
            }
            My.pr_or_do(1, uf, My.Order_dg.Items.Count, Convert.ToInt32(count.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (da.Items.Count == 1)
            {
                MainWindow.n("Чек пуст");
                return;
            }
            if (count.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            double rez = 0;
            double itog = 0;
            double fix = 0;
            string Text = $"              ____ Online_shop ____\n----------------------------------------------------\n";
            foreach (var y in My.Pr_Or_dg.Items)
            {
                if ((y as DataRowView) == null)
                    break;
                foreach (var u in My.Product_dg.Items)
                {
                    if ((u as DataRowView) == null)
                        break;

                    if (Convert.ToInt32((y as DataRowView)[0]) == Convert.ToInt32((u as DataRowView)[0]))
                    {
                        Text += $"  {(u as DataRowView)[2]}\t-\t{(u as DataRowView)[4]}\n";
                        rez += Convert.ToDouble((u as DataRowView)[4]);
                        itog += Convert.ToDouble((u as DataRowView)[4]) * (1.0 - (Convert.ToDouble((u as DataRowView)[5]) + Convert.ToDouble((u as DataRowView)[6])));
                        fix += itog;
                        itog = 0;
                    }
                }
            }
            Text += $"----------------------------------------------------\n  Итог: \n\tСУММА ПОКУПКИ:\t{(float)rez}\n\tСКИДКА:\t{(float)(rez - fix)}\n\t" +
                $"CУММА СО СКИДКОЙ:  {(float)fix}\n\tОПЛАТА:\t{(float)fix}\n\tСДАЧА: 0\n----------------------------------------------------\n\t\t  СПАСИБО ЗА ПОКУПКУ!";
            File.WriteAllText($"чек №{My.Order_dg.Items.Count - 1}.txt", Text);
            My.pr_or_do(2);
            My.order_do(1, -1, 1, "Админ");
        }

        private void bu2_Click(object sender, RoutedEventArgs e)
        {
            naz1.Visibility = Visibility.Hidden;
            cb.Visibility = Visibility.Hidden;
            bu4.Visibility = Visibility.Hidden;
            count.Visibility = Visibility.Hidden;
            bu5.Visibility = Visibility.Visible;
            bu5_.Visibility = Visibility.Hidden;
            bu6.Visibility = Visibility.Visible;
            bu6_.Visibility = Visibility.Hidden;
            bu7.Visibility = Visibility.Visible;
            bu7_.Visibility = Visibility.Hidden;
            b1.Visibility = Visibility.Hidden;
            chek.Visibility = Visibility.Visible;
            dg.Visibility = Visibility.Hidden;
            Window2.cbVis(naz3, "Доставка", cb1, 3, My.Delivery_dg);
            naz3.Visibility = Visibility.Visible;
            Window2.cbVis(naz2, "Сотрудник", cb2, 0, My.Worker_dg);
            da.Visibility = Visibility.Visible;
            da.ItemsSource = My.Order_dg.Items;
            cbb.Visibility = Visibility.Hidden;
            naz5.Content = "";
            naz4.Content = "";
            text1.Visibility = Visibility.Hidden;
            text2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Невозможно удалить заказ до его выполнения");
        }

        private void da_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (da.SelectedItem == null)
                return;
            if (da.SelectedIndex == da.Items.Count - 1)
                return;
            cb2.SelectedItem = (da.SelectedItem as DataRowView)[2];
            foreach (var y in My.Delivery_dg.Items)
            {
                if ((y as DataRowView) == null)
                    break;
                if (Convert.ToInt32((da.SelectedItem as DataRowView)[1]) == Convert.ToInt32((y as DataRowView)[0]))
                    cb1.SelectedItem = (y as DataRowView)[3];
            }
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавлять заказ может только кассир!");
        }

        private void update(object sender, RoutedEventArgs e)
        {
            foreach (var y in My.Delivery_dg.Items)
            {
                if ((y as DataRowView) == null)
                    break;
                if ((y as DataRowView)[3] == cb1.SelectedItem)
                    My.order_do(3, Convert.ToInt32((da.SelectedItem as DataRowView)[0]), Convert.ToInt32((y as DataRowView)[0]), cb2.SelectedItem.ToString());
            }
        }

        private void bu1_Click(object sender, RoutedEventArgs e)
        {
            naz1.Visibility = Visibility.Visible;
            naz1.Content = "Продукт";
            cb.Visibility = Visibility.Visible;
            bu4.Visibility = Visibility.Visible;
            count.Visibility = Visibility.Visible;
            bu5.Visibility = Visibility.Hidden;
            bu5_.Visibility = Visibility.Hidden;
            bu6.Visibility = Visibility.Hidden;
            bu6_.Visibility = Visibility.Hidden;
            bu7.Visibility = Visibility.Hidden;
            bu7_.Visibility = Visibility.Hidden;
            b1.Visibility = Visibility.Visible;
            cb1.Visibility = Visibility.Hidden;
            naz3.Visibility = Visibility.Hidden;
            cb2.Visibility = Visibility.Hidden;
            chek.Visibility = Visibility.Hidden;
            da.Visibility = Visibility.Hidden;
            naz2.Content = "Количество";
            cbb.Visibility = Visibility.Hidden;
            text1.Visibility = Visibility.Hidden;
            text2.Visibility = Visibility.Hidden;
            naz4.Content = "";
            naz5.Content = "";
            dg.Visibility = Visibility.Hidden;
        }

        private void bu3_Click(object sender, RoutedEventArgs e)
        {
            count.Text = "";
            naz1.Visibility = Visibility.Visible;
            dg.ItemsSource = My.Delivery_dg.Items;
            cb.Visibility = Visibility.Hidden;
            bu4.Visibility = Visibility.Hidden;
            count.Visibility = Visibility.Visible;
            bu5_.Visibility = Visibility.Visible;
            bu5.Visibility = Visibility.Hidden;
            bu6_.Visibility = Visibility.Visible;
            bu6.Visibility = Visibility.Hidden;
            bu7.Visibility = Visibility.Hidden;
            bu7_.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Hidden;
            cb1.Visibility = Visibility.Hidden;
            cb2.Visibility = Visibility.Hidden;
            chek.Visibility = Visibility.Hidden;
            dg.Visibility = Visibility.Visible;
            naz2.Content = "Время ожидания";
            naz1.Content = "Цена";
            text1.Visibility = Visibility.Visible;
            text2.Visibility = Visibility.Visible;
            naz4.Content = "Способ доставки";
            naz3.Content = "";
            Window2.cbVis(naz5, "Курьер", cbb, 1, My.Deliverer_dg);
        }

        private void chek_Click(object sender, RoutedEventArgs e)
        {
            if (da.SelectedIndex == da.Items.Count - 1)
            {
                MessageBox.Show("Невозможно открыть пустой чек");
                return;
            }
            if (da.SelectedIndex == da.Items.Count - 2)
            {
                MessageBox.Show("Невозможно открыть чек, который еще не готов!");
                return;
            }
            Process.Start(new ProcessStartInfo { FileName = $"чек №{da.SelectedIndex + 1}.txt", UseShellExecute = true });
        }

        private void update_(object sender, RoutedEventArgs e)
        {
            if (count.Text == "" || text1.Text == "" || text2.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            try
            {
                if (Convert.ToInt32(count.Text) < 0 || Convert.ToInt32(text1.Text) < 0)
                {
                    MessageBox.Show("Введено недопустимое значение");
                    return;
                }
            }
            catch
            {

                MessageBox.Show("Введено недопустимое значение");
                return;
            }
            foreach (var y in My.Deliverer_dg.Items)
            {
                if (y as DataRowView == null)
                    break;
                if (cbb.SelectedItem.ToString() == (y as DataRowView)[1].ToString())
                    My.delivery_do(3, Convert.ToInt32((dg.SelectedItem as DataRowView)[0]), Convert.ToInt32(text1.Text), Convert.ToInt32(count.Text), text2.Text, Convert.ToInt32((y as DataRowView)[0]));
            }
        }

        private void Button_Click_2_(object sender, RoutedEventArgs e)
        {
            My.delivery_do(2, Convert.ToInt32((dg.SelectedItem as DataRowView)[0]));
        }

        private void Insert_(object sender, RoutedEventArgs e)
        {
            if (count.Text == "" || text1.Text == "" || text2.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            try
            {
                if (Convert.ToInt32(count.Text) < 0 || Convert.ToInt32(text1.Text) < 0)
                {
                    MessageBox.Show("Введено недопустимое значение");
                    return;
                }
            }
            catch
            {

                MessageBox.Show("Введено недопустимое значение");
                return;
            }
            foreach (var y in My.Deliverer_dg.Items)
            {
                if (y as DataRowView == null)
                    break;
                if (cbb.SelectedItem.ToString() == (y as DataRowView)[1].ToString())
                    My.delivery_do(1, -1, Convert.ToInt32(text1.Text), Convert.ToInt32(count.Text), text2.Text, Convert.ToInt32((y as DataRowView)[0]));
            }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem as DataRowView == null)
                return;
            if (dg.SelectedIndex == dg.Items.Count - 1)
                return;
            text1.Text = (dg.SelectedItem as DataRowView)[1].ToString();
            count.Text = (dg.SelectedItem as DataRowView)[2].ToString();
            text2.Text = (dg.SelectedItem as DataRowView)[3].ToString();
            foreach (var y in My.Deliverer_dg.Items)
            {
                if (y as DataRowView == null)
                    break;
                if (Convert.ToInt32((y as DataRowView)[0]) == Convert.ToInt32((dg.SelectedItem as DataRowView)[4]))
                    cbb.SelectedItem = (y as DataRowView)[1];
            }
        }
    }
}
