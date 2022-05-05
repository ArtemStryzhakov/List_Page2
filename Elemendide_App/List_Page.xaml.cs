using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elemendide_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_Page : ContentPage
    {
        public ObservableCollection<Telefon> telefons { get; set; }
        Label lbl_list;
        ListView List;
        Button lisa, kustuta;

        public List_Page()
        {
            telefons = new ObservableCollection<Telefon>
            {
                new Telefon {Nimetus="Samsung Galaxy 22 Ultra", Tootaja="Samsung", Hind="1349", Pilt="samsung.jpg"},
                new Telefon {Nimetus="Xiaomi Mi 11 Lite", Tootaja="Xiaomi", Hind="339", Pilt="XiaomiLite.jpg"},
                new Telefon {Nimetus="Xiaomi Mi 11 NE", Tootaja="Apple", Hind="1400", Pilt="XiaomiNE.jpg"},
                new Telefon {Nimetus="iPhone 13", Tootaja="Samsung", Hind="450", Pilt="iPhone.jpg"}
            };
            lbl_list = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            List = new ListView
            {
                SeparatorColor = Color.Orange,
                Header = "Minu oma kolektion",

                HasUnevenRows = true,
                ItemsSource = telefons,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Tootja", StringFormat = "Tore telefon filmalt {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    Binding a = new Binding { Path = "Hind", StringFormat = "Hind: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, a);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;

                })
            };
            lisa = new Button { Text = "Lisa Telefon" };
            kustuta = new Button { Text = "Kustuta telefon" };
            List.ItemTapped += List_ItemTapped;
            kustuta.Clicked += Kustuta_Clicked;
            lisa.Clicked += Lisa_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, List, lisa, kustuta } };
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {

            string nameOfPhone = await DisplayPromptAsync("What name of this phone?", "Write it down: ", keyboard: Keyboard.Text);
            string nameOfCompany = await DisplayPromptAsync("What name company of this phone?", "Write it down:", keyboard: Keyboard.Text);
            string cost = await DisplayPromptAsync("What cost of the phone?", "Write it down:", keyboard: Keyboard.Numeric);
            string phoneLink = await DisplayPromptAsync("Write a link of the image?", "Write it down:", keyboard: Keyboard.Text);

            telefons.Add(item: new Telefon { Nimetus = nameOfPhone, Tootaja = nameOfCompany, Hind = cost, Pilt = phoneLink });

        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Telefon phone = List.SelectedItem as Telefon;
            if (phone != null)
            {
                telefons.Remove(phone);
                List.SelectedItem = null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
                await DisplayAlert("Выбранная модель", $"{selectedPhone.Tootaja}-{selectedPhone.Nimetus}", "OK");
        }
    }
}