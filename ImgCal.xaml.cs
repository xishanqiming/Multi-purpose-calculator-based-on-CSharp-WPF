/**ImgCal.xaml.cs
*@author: CHANG WEI-LIN
*@Last Modify: 3 / 25 / 2023
*@About: This program is for the ImgCal with C# WPF.
*Users can input a function expression and get the Image of this function in XOY Canvas.
*/

using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using AngouriMath;
using Window = System.Windows.Window;
using AngouriMath.Core.Exceptions;
using System.Windows;
using System.Linq;

namespace Lab2_Scientific_Calculator_With_CSharp_WPF
{
    public partial class ImgCal : Window
    {
        public ImgCal()
        {
            InitializeComponent();
        }

        private void TxtFunction_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        public class CannotEvalException : Exception
        {
            public CannotEvalException(string message) : base(message)
            {
            }
        }

        private void BtnPlot_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //获取用户输入的函数表达式
            string function = txtFunction.Text;

            //校验用户输入的表达式是否为非法
            if (string.IsNullOrWhiteSpace(function))
            {
                MessageBox.Show("请输入合法的函数表达式！");
                return;
            }

            try
            {
                //开始记时
                var startTime = DateTime.Now;

                //解析表达式，如果失败则抛出异常
                Entity expr = MathS.FromString(function);

                //检查表达式中是否包含未定义的变量，如果包含则抛出异常
                var undefinedVars = expr.Vars.Except(new[] { MathS.Var("x") });
                if (undefinedVars.Count() > 0)
                {
                    throw new CannotEvalException($"表达式中包含未定义的变量：{string.Join(",", undefinedVars)}");
                }

                //定义绘图区域的大小和范围
                double xmin = -50;
                double xmax = 50;
                double ymin = -50;
                double ymax = 50;

                //清空绘图控件
                plotView1.Model = null;

                //创建绘图模型
                var plotModel = new PlotModel();
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = xmin, Maximum = xmax });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = ymin, Maximum = ymax });

                //创建函数图像，取点率为0.1（按理说应该允许用户自定义设置）
                FunctionSeries functionSeries = new FunctionSeries(x => EvaluateParallel(function, x), xmin, xmax, 0.1, function);

                //添加函数图像到绘图模型
                plotModel.Series.Add(functionSeries);

                //显示绘图结果
                plotView1.Model = plotModel;

                //记录结束时间并计算耗时
                var endTime = DateTime.Now;
                var timeElapsed = endTime - startTime;
                MessageBox.Show($"计算耗时：{timeElapsed.TotalMilliseconds:F2} 毫秒");
            }

            //异常检测模块
            catch (ParseException)
            {
                MessageBox.Show("表达式无法解析，请输入合法的函数表达式！");
            }

            catch (CannotEvalException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception)
            {
                MessageBox.Show("发生未知错误，请稍后重试！");
            }
        }

        //定义静态方法 EvaluateParallel（多线程浅试，但好像没啥变化）
        private static double EvaluateParallel(string function, double x)
        {
            //将函数表达式解析为 AngouriMath 的表达式对象
            Entity expr = MathS.FromString(function);

            //将变量 x 替换为指定的值
            Entity result = expr.Substitute("x", x);

            //检查结果是否可以表示为简单数字
            if (!result.EvaluableNumerical)
            {
                //无法转换为简单数字，抛出异常
                throw new CannotEvalException("Result cannot be represented as a simple number! Use EvaluableNumerical to check beforehand.");
            }

            //将结果转换为 double 类型并返回
            return (double)result.EvalNumerical();
        }
    }
}
/* Program Over */