using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using итоговая_резня.online_shopDataSetTableAdapters;

namespace итоговая_резня
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static List<user> users = new List<user>();
        public MainWindow()
        {
            InitializeComponent();
            My.user_do();
            My.worker_do();
            My.role_do();
            My.worker_do();
            My.stock_do();
            My.category_do();
            My.product_do();
            My.promotion_do();
            My.raiting_do();
            My.deliverer_do();
            My.order_do();
            My.delivery_do();
            My.pr_or_do();
            foreach (var u in My.User_dg.Items)
            {
                if ((u as DataRowView) == null)
                    break;
                users.Add(new user((u as DataRowView)[1].ToString(), (u as DataRowView)[2].ToString()));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string role = "";
            int a = 0;
            if (password.Password.ToString() == "" || login.Text == "")
            {
                n("Не все поля заполнены");
                return;
            }
            foreach (var y in My.User_dg.Items)
            {
                if ((y as DataRowView) == null)
                    break;
                if (login.Text == (y as DataRowView)[1].ToString() && password.Password.ToString() == (y as DataRowView)[2].ToString())
                {
                    foreach (var u in My.Worker_dg.Items)
                    {
                        if ((u as DataRowView) == null)
                            break;
                        if ((y as DataRowView)[3].ToString() == (u as DataRowView)[0].ToString())
                        {
                            foreach (var i in My.Role_dg.Items)
                            {
                                if ((i as DataRowView) == null)
                                    break;
                                if (Convert.ToInt32((i as DataRowView)[0]) == Convert.ToInt32((u as DataRowView)[3]))
                                {
                                    a = 1;
                                    role = (i as DataRowView)[1].ToString();
                                    switch (role)
                                    {
                                        case "Администратор":
                                            Window2.func = 1;
                                            break;
                                        case "Кассир":
                                            Hide();
                                            new Window3().Show();
                                            return;
                                        case "Склад":
                                            Window2.func = 2;
                                            break;
                                        case "Пользователь":
                                            Window2.func = 3;
                                            break;
                                        case "Акционер":
                                            Window2.func = 4;
                                            break;
                                        default:
                                            MessageBox.Show("Для данной роли нет доступного функционала");
                                            break;
                                    }
                                    Window2 w = new Window2();
                                    w.Title = role;
                                    Hide();
                                    w.Show();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            if (a == 0)
            {
                n("Неверный логин или пароль");
            }
        }
        public static void n(object g)
        {
            MessageBox.Show(g.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
    public class user
    {
        public string login { get; set; }
        public string password { get; set; }
        public user(string log, string pass)
        {
            login = log;
            password = pass;
        }
    }
    class My
    {
        public static muserTableAdapter User = new muserTableAdapter();
        public static roleTableAdapter Role = new roleTableAdapter();
        public static workerTableAdapter Worker = new workerTableAdapter();
        public static raitingTableAdapter Raiting = new raitingTableAdapter();
        public static delivererTableAdapter Deliverer = new delivererTableAdapter();
        public static deliveryTableAdapter Delivery = new deliveryTableAdapter();
        public static morderTableAdapter Order = new morderTableAdapter();
        public static product_orderTableAdapter pr_or = new product_orderTableAdapter();
        public static promotionTableAdapter Promotion = new promotionTableAdapter();
        public static categoryTableAdapter Category = new categoryTableAdapter();
        public static stockTableAdapter Stock = new stockTableAdapter();
        public static stock_productTableAdapter St_pr = new stock_productTableAdapter();
        public static productTableAdapter Product = new productTableAdapter();
        public static DataGrid User_dg = new DataGrid();
        public static DataGrid Role_dg = new DataGrid();
        public static DataGrid Worker_dg = new DataGrid();
        public static DataGrid Stock_dg = new DataGrid();
        public static DataGrid Product_dg = new DataGrid();
        public static DataGrid Category_dg = new DataGrid();
        public static DataGrid Promotion_dg = new DataGrid();
        public static DataGrid Deliverer_dg = new DataGrid();
        public static DataGrid Raiting_dg = new DataGrid();
        public static DataGrid Delivery_dg = new DataGrid();
        public static DataGrid Order_dg = new DataGrid();
        public static DataGrid Pr_Or_dg = new DataGrid();
        public static void pr_or_do(int doing = 0, int c1 = -1, int c2 = -1, int c3 = -1)
        {
            Pr_Or_dg.ItemsSource = pr_or.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    pr_or.InsertQuery(c1, c2, c3);
                    break;
                case 2:
                    pr_or.DeleteQuery();
                    break;
            }
            Pr_Or_dg.ItemsSource = pr_or.GetData();
        }
        public static void user_do(int doing = 0, int c1 = -1, string c2 = "", string c3 = "", string c4 = "")
        {
            User_dg.ItemsSource = User.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    User.InsertQuery(c2, c3, c4);
                    break;
                case 2:
                    User.DeleteQuery(c1);
                    break;
                case 3:
                    User.UpdateQuery(c2, c3, c4, c1);
                    break;
            }
            User_dg.ItemsSource = User.GetData();
        }
        public static void role_do(int doing = 0, int c1 = -1, string c2 = "")
        {
            Role_dg.ItemsSource = Role.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Role.InsertQuery(c2);
                    break;
                case 2:
                    Role.DeleteQuery(c1);
                    break;
                case 3:
                    Role.UpdateQuery(c2, c1);
                    break;
            }
            Role_dg.ItemsSource = Role.GetData();
        }
        public static void worker_do(int doing = 0, string c1 = "", string c2 = "", string c3 = "", int c4 = -1, int c5 = -1)
        {
            Worker_dg.ItemsSource = Worker.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Worker.InsertQuery(c1, c2, c4, c5);
                    break;
                case 2:
                    Worker.DeleteQuery(c1);
                    break;
                case 3:
                    Worker.UpdateQuery(c1, c2, c4, c5, c3);
                    break;
            }
            Worker_dg.ItemsSource = Worker.GetData();
        }
        public static void stock_do(int doing = 0, int c1 = -1, string c2 = "", string c3 = "", int c4 = -1)
        {
            Stock_dg.ItemsSource = Stock.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Stock.InsertQuery(c2, c3, c4);
                    break;
                case 2:
                    Stock.DeleteQuery(c1);
                    break;
                case 3:
                    Stock.UpdateQuery(c2, c3, c4, c1);
                    break;
            }
            Stock_dg.ItemsSource = Stock.GetData();
        }
        public static void product_do(int doing = 0, int c1 = -1, int c2 = -1, string c3 = "", int c4 = -1, int c5 = -1, double c6 = -0.1, double c7 = -0.1)
        {
            Product_dg.ItemsSource = Stock.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Product.InsertQuery(c2, c3, c4, c5, (float)c6, (float)c7);
                    break;
                case 2:
                    Stock.DeleteQuery(c1);
                    break;
                case 3:
                    Product.UpdateQuery(c2, c3, c4, c5, (float)c6, (float)c7, c1);
                    break;
            }
            Product_dg.ItemsSource = Product.GetData();
        }
        public static void promotion_do(int doing = 0, int c1 = -1, int c2 = -1, string c3 = "", string c4 = "")
        {
            Promotion_dg.ItemsSource = Promotion.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Promotion.InsertQuery(c2, c3, c4);
                    break;
                case 2:
                    Promotion.DeleteQuery(c1);
                    break;
                case 3:
                    Promotion.UpdateQuery(c2, c3, c4, c1);
                    break;
            }
            Promotion_dg.ItemsSource = Promotion.GetData();
        }
        public static void category_do(int doing = 0, int c1 = -1, string c2 = "")
        {
            Category_dg.ItemsSource = Category.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Category.InsertQuery(c2);
                    break;
                case 2:
                    Category.DeleteQuery(c1);
                    break;
                case 3:
                    Category.UpdateQuery(c2, c1);
                    break;
            }
            Category_dg.ItemsSource = Category.GetData();
        }
        public static void deliverer_do(int doing = 0, int c1 = -1, string c2 = "", int c3 = -1)
        {
            Deliverer_dg.ItemsSource = Deliverer.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Deliverer.InsertQuery(c2, c3);
                    break;
                case 2:
                    Deliverer.DeleteQuery(c1);
                    break;
                case 3:
                    Deliverer.UpdateQuery(c2, c3, c1);
                    break;
            }
            Deliverer_dg.ItemsSource = Deliverer.GetData();
        }
        public static void raiting_do(int doing = 0, int c1 = -1, int c2 = -1, int c3 = -1, int c4 = -1)
        {
            Raiting_dg.ItemsSource = Raiting.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Raiting.InsertQuery(c2, c3, c4);
                    break;
                case 2:
                    Raiting.DeleteQuery(c1);
                    break;
                case 3:
                    Raiting.UpdateQuery(c2, c3, c4, c1);
                    break;
            }
            Raiting_dg.ItemsSource = Raiting.GetData();
        }
        public static void delivery_do(int doing = 0, int c1 = -1, int c2 = -1, int c3 = -1, string c4 = "", int c5 = -1)
        {
            Delivery_dg.ItemsSource = Delivery.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Delivery.InsertQuery(c2, c3, c4, c5);
                    break;
                case 2:
                    Delivery.DeleteQuery(c1);
                    break;
                case 3:
                    Delivery.UpdateQuery(c2, c3, c4, c5, c1);
                    break;
            }
            Delivery_dg.ItemsSource = Delivery.GetData();
        }
        public static void order_do(int doing = 0, int c1 = -1, int c2 = -1, string c3 = "")
        {
            Order_dg.ItemsSource = Order.GetData();
            switch (doing)
            {
                case 0:
                    break;
                case 1:
                    Order.InsertQuery(c2, c3);
                    break;
                case 2:
                    Order.DeleteQuery(c1);
                    break;
                case 3:
                    Order.UpdateQuery(c2, c3, c1);
                    break;
            }
            Order_dg.ItemsSource = Order.GetData();
        }
    }
}