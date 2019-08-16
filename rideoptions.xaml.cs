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
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Sedansynergy.ServiceReference1;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sedansynergy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rideoptions : Page
    {
        String emails;

        public rideoptions()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            show s = (show)e.Parameter;
            emails = s.email;
        }

        private void setride(object sender, TappedRoutedEventArgs e)
        {
            setridepanel.Visibility = Visibility.Visible;
        }
        private void searchride(object sender, TappedRoutedEventArgs e)
        {
            setridepanel.Visibility = Visibility.Collapsed;
            searchridepanel.Visibility = Visibility.Visible;
        }

        private void cancelride(object sender, TappedRoutedEventArgs e)
        {

        }
        private async void setridefin(object sender, TappedRoutedEventArgs e)
        {
            int mi = timepick.Time.Minutes;
            int hrs = timepick.Time.Hours;
            int day = DateTime.Now.Date.Day;
            int month = DateTime.Now.Date.Month;
            int year = DateTime.Now.Date.Year;
            bool boo = true;

            if (textBox.Text.Length == 0)
            {
                button_Copy2.Content = "Starting place field is empty";
                boo = false;
            }
            if (textBox2.Text.Length == 0)
            {
                button_Copy2.Content = "Destination field is empty";
                boo = false;
            }
            if (textBox2_Copy.Text.Length == 0)
            {
                button_Copy2.Content = "No. of persons allowed field is empty";
                boo = false;
            }


            DBserviceSoapClient db = new DBserviceSoapClient();

            string addressToGeocode = textBox.Text;
            string addressToGeocode1 = textBox2.Text;

            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 20.5937;
            queryHint.Longitude = 79.9629;
            Geopoint hintPoint = new Geopoint(queryHint);
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);
            String lat = result.Locations[0].Point.Position.Latitude.ToString();
            String lon = result.Locations[0].Point.Position.Longitude.ToString();

            decimal orglat, orglon, orglat1, orglon1;
            BasicGeoposition queryHint1 = new BasicGeoposition();
            queryHint1.Latitude = 20.5937;
            queryHint1.Longitude = 79.9629;
            Geopoint hintPoint1 = new Geopoint(queryHint1);
            MapLocationFinderResult result1 = await MapLocationFinder.FindLocationsAsync(addressToGeocode1, hintPoint, 3);
            String lat1 = result1.Locations[0].Point.Position.Latitude.ToString();
            String lon1 = result1.Locations[0].Point.Position.Longitude.ToString();

            orglat = decimal.Parse(lat);
            orglon = decimal.Parse(lon);
            orglat1 = decimal.Parse(lat1);
            orglon1 = decimal.Parse(lon1);

            orglat = Math.Round(orglat, 4);
            orglon = Math.Round(orglon, 4);
            orglat1 = Math.Round(orglat1, 4);
            orglon1 = Math.Round(orglon1, 4);

            int cap = int.Parse(textBox2_Copy.Text);

            if (result1.Status == MapLocationFinderStatus.Success && result.Status == MapLocationFinderStatus.Success)
            {
                if (boo == true)
                {
                    try
                    {
                        await db.arraythingAsync(day, year, month);
                        var a = await db.setrideAsync(textBox.Text, textBox2.Text, orglat, orglon, orglat1, orglon1, hrs, mi, day, month, year, emails, "set", cap);
                        var bb = await db.arrlengthAsync(day);
                        /* String [] s = new String[bb];
                         for(int jj=0;jj< bb;jj++)
                         {
                             s[jj] = b.Body.arraythingResult[jj];
                         }
                        */
                        button_Copy2.Content = "sucess";
                    }
                    catch (Exception ex)
                    {
                        textBox.Text = "";
                        textBox2.Text = "";
                        textBox2_Copy.Text = "";
                        button_Copy2.Content = "Server error...Re-enter";
                    }
                }
            }
            else
            {
                button_Copy2.Content = "Invalid Place";
            }
        }
        public int[] hi()
        {
            int[] a = { 1, 2 };
            return a;
        }

        private async void searchridefin(object sender, TappedRoutedEventArgs e)
        {
            int mi = timepick1.Time.Minutes;
            int hrs = timepick1.Time.Hours;
            int day = DateTime.Now.Date.Day;
            int month = DateTime.Now.Date.Month;
            int year = DateTime.Now.Date.Year;
            bool boo = true;

            if (textBox1.Text.Length == 0)
            {
                button_Copy2.Content = "Starting place field is empty";
                boo = false;
            }
            if (textBox4.Text.Length == 0)
            {
                button_Copy2.Content = "Destination field is empty";
                boo = false;
            }
            if (textBox2_Copy1.Text.Length == 0)
            {
                button_Copy2.Content = "No. of persons allowed field is empty";
                boo = false;
            }


            DBserviceSoapClient db = new DBserviceSoapClient();

            string addressToGeocode = textBox1.Text;
            string addressToGeocode1 = textBox4.Text;

            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 20.5937;
            queryHint.Longitude = 79.9629;
            Geopoint hintPoint = new Geopoint(queryHint);
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);
            String lat = result.Locations[0].Point.Position.Latitude.ToString();
            String lon = result.Locations[0].Point.Position.Longitude.ToString();

            decimal orglat, orglon, orglat1, orglon1;
            BasicGeoposition queryHint1 = new BasicGeoposition();
            queryHint1.Latitude = 20.5937;
            queryHint1.Longitude = 79.9629;
            Geopoint hintPoint1 = new Geopoint(queryHint1);
            MapLocationFinderResult result1 = await MapLocationFinder.FindLocationsAsync(addressToGeocode1, hintPoint, 3);
            String lat1 = result1.Locations[0].Point.Position.Latitude.ToString();
            String lon1 = result1.Locations[0].Point.Position.Longitude.ToString();

            orglat = decimal.Parse(lat);
            orglon = decimal.Parse(lon);
            orglat1 = decimal.Parse(lat1);
            orglon1 = decimal.Parse(lon1);

            orglat = Math.Round(orglat, 4);
            orglon = Math.Round(orglon, 4);
            orglat1 = Math.Round(orglat1, 4);
            orglon1 = Math.Round(orglon1, 4);

            int cap = int.Parse(textBox2_Copy1.Text);

            show s = new show() { email = emails };

            if (result1.Status == MapLocationFinderStatus.Success && result.Status == MapLocationFinderStatus.Success)
            {
                if (boo == true)
                {
                    try
                    {
                        await db.arraythingAsync(day, year, month);
                        var a = await db.searchrideAsync(textBox1.Text, textBox4.Text, orglat, orglon, orglat1, orglon1, hrs, mi, day, month, year, emails, "search", cap);
                        this.Frame.Navigate(typeof(aftersearch), s);
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text = "";
                        textBox4.Text = "";
                        textBox2_Copy1.Text = "";
                        button_Copy2.Content = "Server error...Re-enter";
                    }
                }
            }
            else
            {
                button_Copy2.Content = "Invalid Place";
            }

        }
    }

}
