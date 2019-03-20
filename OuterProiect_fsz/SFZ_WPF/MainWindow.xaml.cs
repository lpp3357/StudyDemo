using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SFZ_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable data = new DataTable();

        private DispatcherTimer timer = new DispatcherTimer();



        public MainWindow()
        {
            InitializeComponent();
            CreateData();
        }

        /// <summary>
        /// 数据源初始化
        /// </summary>
        private void CreateData()
        {
            data.Columns.Add("username");
            data.Columns.Add("sfzno");
            data.Columns.Add("sfz_datetime");

            timer.Tick += new EventHandler(Event_DispatcherTimer);
            timer.Interval = TimeSpan.FromSeconds(1.5);
        }


        public void BindData(string username, string starttime, string endtime)
        {
            data.Clear();
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
            timer.Stop();
            string username = this.q_username.Text;
            string startTime = this.q_starttime.Text;
            string endTime = this.q_endtime.Text;
            BindData(username, startTime, endTime);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            DataTable sourcData = data.Copy();
            sourcData.Columns[0].ColumnName = "姓名";
            sourcData.Columns[1].ColumnName = "身份证号码";
            sourcData.Columns[2].ColumnName = "时间";
            SaveFileDialog saveFile = new SaveFileDialog();
            //设置文件类型
            saveFile.Filter = "Execl files (*.xls)|*.xls";
            saveFile.FilterIndex = 0;    //设置文件显示顺序；
            saveFile.RestoreDirectory = true;   //保存对话框是否记忆上次打开的目录
            saveFile.CreatePrompt = true;
            saveFile.Title = "导出Excel";
            saveFile.FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            saveFile.ShowDialog();
            if (saveFile.FileName.IndexOf("\\") < 0) return; //被点了"取消"
            Thread.Sleep(TimeSpan.FromSeconds(1)); //休眠1秒
            Stream myStream = saveFile.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            StringBuilder sb = new StringBuilder();
            try
            {
                for (int i = 0; i < sourcData.Columns.Count; i++)
                {
                    sb.Append(@"=""" + sourcData.Columns[i].ToString() + @"""" + "\t");
                }
                foreach (DataRow row in sourcData.Rows)
                {
                    //换行字符
                    sb.Append(Environment.NewLine);
                    //导出列内容
                    foreach (DataColumn column in sourcData.Columns)
                    {
                        if (column.DataType == typeof(DateTime))
                        {
                            sb.Append(@"=""" + row[column] + @"""" + "\t");
                            continue;
                        }
                        sb.Append(row[column].ToString() + "\t");
                    }
                    // sb.Append(Environment.NewLine);
                }
                sw.WriteLine(sb.ToString());
                sw.Flush();
                MessageBox.Show("导出成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("崩溃了");
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }


        /// <summary>
        /// 定时器触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_DispatcherTimer(object sender, EventArgs e)
        {
            if (data.Rows.Count >= 10)
            {
                data.Clear();
            }
            DataRow row = data.NewRow();
            Random random = new Random();
            row["username"] = CreateName(random.Next(0, 7));
            row["sfzno"] = random.Next(111111, 333333).ToString() + random.Next(333333, 666666).ToString() + random.Next(666666, 999999).ToString();
            row["sfz_datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            data.Rows.Add(row.ItemArray);
            DataView dataView = new DataView(data);
            this.MyData.AutoGenerateColumns = false;
            this.MyData.ItemsSource = dataView;
        }

        private string CreateName(int index)
        {
            string[] nameArr = { "小张", "小王", "小李", "二狗", "乌嘴", "小花", "花花", "小美", "小明" };
            if (index > nameArr.Length) index = 0;
            return nameArr[index];
        }
        /// <summary>
        /// 开始识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Q_start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
