using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel.DataAnnotations;
using Sedansynergy.ServiceReference1;
using System.Text.RegularExpressions;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sedansynergy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void loginclick(object sender, TappedRoutedEventArgs e)
        {
            show s = new show() { email = textBox1.Text };
            String pwd = textBox2.Password;
            try
            {
                DBserviceSoapClient db = new DBserviceSoapClient();
                var a = await db.logincheckAsync(textBox1.Text, pwd);
                if (a.Body.logincheckResult == 0)
                {
                    textBox.Content = "login failed";
                }
                else
                {
                    this.Frame.Navigate(typeof(rideoptions), s);
                }
            }
            catch (Exception ex)
            {

                textBox.Content = "Server Error Try again";
            }
        }

        private async void registerclick(object sender, TappedRoutedEventArgs e)
        {
            textBox_Copy.Content = "";
            String pwd = textBox6.Password;
            String pwd1 = textBox7.Password;
            bool iss = true;
            DBserviceSoapClient db = new DBserviceSoapClient();
            if (textBox8.Text.Length == 0)
            {
                textBox_Copy1.Content = "Maker/Brand Field is Empty";
                iss = false;
            }
            if (textBox9.Text.Length == 0)
            {
                textBox_Copy1.Content = "Car Name field is empty";
                iss = false;
            }
            if (textBox10.Text.Length == 0)
            {
                textBox_Copy1.Content = "Mileage Field is empty";
                iss = false;
            }
            if (textBox11.Text.Length == 0)
            {
                textBox_Copy1.Content = "Variant Field is empty";
                iss = false;
            }
            if (IsDigitsOnly(textBox10.Text) == false)
            {
                textBox_Copy1.Content = "Mileage Field contains characters";
                iss = false;
            }
            if (iss == true)
            {
                try
                {
                    var a = await db.InsertDataAsync(textBox3.Text, textBox4.Text, Int64.Parse(textBox5.Text), pwd, textBox8.Text, textBox9.Text, int.Parse(textBox10.Text), textBox11.Text);
                    if (a.Body.InsertDataResult == 1)
                    {
                        textBox_Copy1.Content = "Email or phonenumber already registered";
                        reg2panel.Visibility = Visibility.Collapsed;
                        reg1panel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        reg2panel.Visibility = Visibility.Collapsed;
                        loginpanel.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    textBox_Copy1.Content = "Server error... Click the register button again";
                }
            }
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static bool IsValidPassword(string input)
        {
            Match match = Regex.Match(input, @"(?=^.{8,64}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*");

            if (match.Success && match.Index == 0 && match.Length == input.Length)
                return true;
            else
                return false;

        }

        private void nextclick(object sender, TappedRoutedEventArgs e)
        {

            String pwd = textBox6.Password;
            String pwd1 = textBox7.Password;
            bool bar, iss = true;
            if (new EmailAddressAttribute().IsValid(textBox4.Text))
                bar = true;
            else
                bar = false;

            if (textBox5.Text.Length != 10)
            {
                textBox_Copy.Content = "Invalid phonenumber";
                iss = false;
            }
            if (pwd != pwd1)
            {
                textBox_Copy.Content = "Password Do not match";
                iss = false;
            }
            if (textBox3.Text.Length == 0)
            {
                textBox_Copy.Content = "User Name is empty";
                iss = false;
            }
            if (textBox4.Text.Length == 0)
            {
                textBox_Copy.Content = "Email field is empty";
                iss = false;
            }
            if (textBox5.Text.Length == 0)
            {
                textBox_Copy.Content = "Phone Number Field is empty";
                iss = false;
            }
            if (textBox6.Password.Length == 0)
            {
                textBox_Copy.Content = "Password Field is empty";
                iss = false;
            }
            if (textBox7.Password.Length == 0)
            {
                textBox_Copy.Content = "Confirm Password Field is empty";
                iss = false;
            }
            if (IsValidPassword(pwd) == false)
            {
                textBox_Copy.Content = "Password isn't atleast 8 characters and doesn't contain 1 Capital Letter,1 Numeric and 1 special character ";
                iss = false;
            }
            if (bar == false)
            {
                textBox_Copy.Content = "Invalid email";
                iss = false;
            }
            if (IsDigitsOnly(textBox5.Text) == false)
            {
                textBox_Copy.Content = "Phone Number Field contains characters";
                iss = false;
            }

            if (iss == true)
            {
                reg1panel.Visibility = Visibility.Collapsed;
                reg2panel.Visibility = Visibility.Visible;
            }

        }

        private void toregisterclick(object sender, TappedRoutedEventArgs e)
        {
            loginpanel.Visibility = Visibility.Collapsed;
            reg1panel.Visibility = Visibility.Visible;
        }

        private void backtologin(object sender, TappedRoutedEventArgs e)
        {
            reg1panel.Visibility = Visibility.Collapsed;
            loginpanel.Visibility = Visibility.Visible;
        }
    }
}
