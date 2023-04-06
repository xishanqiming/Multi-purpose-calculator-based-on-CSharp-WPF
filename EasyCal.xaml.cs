/**EasyCal.xaml.cs
*@author: CHANG WEI-LIN 
*@Last Modify: 3 / 25 / 2023
*@About: This program is for the EasyCal with C# WPF.
*Users can input or select different bottoms in the window to input related parameters.
*/

using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AngouriMath;
using AngouriMath.Extensions;


namespace Lab2_Scientific_Calculator_With_CSharp_WPF
{
    public partial class EasyCal : Window
    {
        private bool isOperatorClicked;

        public EasyCal()
        {
            InitializeComponent();
        }

        //初始化结果栏
        public void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //There's Nothing. ^_^
        }

        //只允许用户输入以下字符：数字0-9 + - * / . ( )   （但是似乎这个方法失败了....)
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 允许的字符集合
            string allowedChars = "0123456789+-*/.()";

            // 如果输入字符不在允许的字符集合中，则阻止输入
            if (!allowedChars.Contains(e.Text))
            {
                // 替换输入的非法字符
                int caretIndex = ResultTextBox.CaretIndex;
                string text = ResultTextBox.Text;
                ResultTextBox.Text = text.Substring(0, caretIndex) + e.Text + text.Substring(caretIndex + ResultTextBox.SelectionLength);
                ResultTextBox.CaretIndex = caretIndex + 1;

                // 防止输入非法字符 
                e.Handled = true;
            }
        }

        //初始化数字按钮点击响应
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (isOperatorClicked)
            {
                ResultTextBox.Text = "";
                isOperatorClicked = false;
            }

            ResultTextBox.Text += digit;
        }


        //初始化操作符按钮点击响应
        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Content.ToString();

            ResultTextBox.Text += op;

            isOperatorClicked = false;
        }


        //初始化等号按钮点击响应（输出答案）
        // 初始化等号按钮点击响应（输出答案）
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 计算表达式的值
                string expression = ResultTextBox.Text;
                var result = (decimal)expression.EvalNumerical();

                // 显示计算结果
                ResultTextBox.Text = result.ToString();
                isOperatorClicked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("表达式错误: " + ex.Message);
            }
        }

        //初始化 AC 按钮点击响应
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";    //清空结果栏
            isOperatorClicked = false;
        }


        //初始化小数点按钮点击响应
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOperatorClicked)
            {
                ResultTextBox.Text += "0";
            }

            ResultTextBox.Text += ".";
            isOperatorClicked = false;
        }


        //初始化负数按钮点击响应
        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox.Text == "")
            {     //首先判断文本框内容是否为空，若为空，则在文本框中显示“-”
                ResultTextBox.Text = "-";
            }
            else if (ResultTextBox.Text.EndsWith(" "))
            {    //若文本框内容不为空，则判断最后一个单词是否以空格结尾，若是，就在文本框中添加“-”
                ResultTextBox.Text += "-";
            }
            else
            {    //若最后的单词不以空格结尾，则将文本框按空格分割成多个单词
                string[] tokens = ResultTextBox.Text.Split(' ');
                string lastToken = tokens[tokens.Length - 1];

                if (lastToken == "-" || lastToken == "+" || lastToken == "*" || lastToken == "/")
                {    //判断分割后的最后一个单词是否为“-”、“+”、“*”或“/”，若是，就在文本框中添加一个负号“-”
                    ResultTextBox.Text += "-";
                }
                else if (lastToken.StartsWith("-"))
                {    //若最后一个单词已经是负号“-”，就将其删除，只能存在一个负号
                    ResultTextBox.Text = ResultTextBox.Text.Substring(0, ResultTextBox.Text.Length - 1);
                }
                else
                {    //若最后一个单词也不以负号开头，则在文本框中添加一个空格和负号“-”，表示用户可能即将输入一个负数
                    ResultTextBox.Text += " -";
                }
            }
        }
    }
}

/* Program Over */