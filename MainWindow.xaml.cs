/**MainWindow.xaml.cs
*@author: CHANG WEI-LIN
*@Last Modify: 3 / 21 / 2023
*@About: This program is the main window of this calculator application with C# WPF.
*/

using System.Windows;
using System.Windows.Input;

namespace Lab2_Scientific_Calculator_With_CSharp_WPF
{
    //Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //按钮一：链接到“简易计算器”窗体
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //生成新的主窗体
            EasyCal EasyCal = new EasyCal();
            EasyCal.Title = "简易计算器";

            //设置系统主窗体
            App.Current.MainWindow = EasyCal;
            EasyCal.Owner = this;

            // 显示新窗口为模式对话框
            //
            var result = EasyCal.ShowDialog();
        }

        //按钮二：链接到“科学计算器”窗体
        private void Button_SciClick(object sender, RoutedEventArgs e)
        {
            //生成新的主窗体
            SciCal SciCal = new SciCal();
            SciCal.Title = "科学计算器";

            //设置系统主窗体
            App.Current.MainWindow = SciCal;
            SciCal.Owner = this;

            // 显示新窗口为模式对话框
            var result = SciCal.ShowDialog();
        }

        //按钮三：链接到“函数图像绘制器”窗体
        private void Button_ImgClick(object sender, RoutedEventArgs e)
        {
            //生成新的主窗体
            ImgCal ImgCal = new ImgCal();
            ImgCal.Title = "函数图像绘制器";

            //设置系统主窗体
            App.Current.MainWindow = ImgCal;
            ImgCal.Owner = this;

            // 显示新窗口为模式对话框
            var result = ImgCal.ShowDialog();
        }

        //用户点击按钮一时消息栏的反馈
        private void Button_EasyClick_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = " 可堪使用的简易功能计算器，能够计算基本的四则运算。";
        }

        //用户光标移开后消息栏的反馈
        private void Button_EasyClick_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = string.Empty;
        }

        //用户点击按钮二时消息栏的反馈
        private void Button_SciClick_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = " 具有科学计算功能的计算器，能够计算较复杂的表达式。";
        }

        //用户光标移开后消息栏的反馈
        private void Button_SciClick_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = string.Empty;
        }

        //用户点击按钮三时消息栏的反馈
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = " 提供基本函数图像绘制功能，仅支持在直角坐标下绘制。";
        }

        //用户光标移开后消息栏的反馈
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = string.Empty;
        }

        //用户点击作者信息时消息栏的反馈
        private void PerPho_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = " 绿色与自由软件倡导者同盟，CHANG WEI-LIN 制作。";
        }

        //用户光标移开后消息栏的反馈
        private void PerPho_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarText.Text = string.Empty;
        }
    }
}

// Program Over