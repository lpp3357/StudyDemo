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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SFZ_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void BindData(string username, string starttime, string endtime)
        {
            DataTable data = new DataTable();
            data.Columns.Add("username");
            data.Columns.Add("sfzno");
            data.Columns.Add("sfz_datetime");

            for (int i = 0; i < 10; i++)
            {
                DataRow row = data.NewRow();
                row["username"] = (string.IsNullOrWhiteSpace(username) ? "姓名" : username) + i.ToString();
                row["sfzno"] = "5113******888**88" + i.ToString();
                if (10 / 2 > i)
                {
                    row["sfz_datetime"] = (string.IsNullOrWhiteSpace(starttime)) ? DateTime.Now.ToString("yyyy/MM/dd") : starttime;
                }
                else
                {
                    row["sfz_datetime"] = (string.IsNullOrWhiteSpace(endtime)) ? DateTime.Now.ToString("yyyy/MM/dd") : endtime;
                }
                data.Rows.Add(row.ItemArray);
            }
            DataView dataView = new DataView(data);
            this.MyData.AutoGenerateColumns = false;
            this.MyData.ItemsSource = dataView;
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Q_startbtn_Click(object sender, RoutedEventArgs e)
        {
            string username = this.q_username.Text;
            string startTime = this.q_starttime.Text;
            string endTime = this.q_endtime.Text;
            BindData(username, startTime, endTime);
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
         
    }
}
