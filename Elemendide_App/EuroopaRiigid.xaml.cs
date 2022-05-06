using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elemendide_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EuroopaRiigid : ContentPage
    {
        public ObservableCollection<Euroopa> europe { get; set; }
        Label lbl_list;
        ListView List;
        Button lisa, kustuta;
        public EuroopaRiigid()
        {
            europe = new ObservableCollection<Euroopa>
            { 
                new Euroopa {nameOfCountry="Estonia", nameOfCapital="Tallin", People="1.3M", Image="https://vp2006-2016.president.ee/images/stories/president_staatilised/lipp.jpg"},
                new Euroopa {nameOfCountry="Latvia", nameOfCapital="Riga", People="1.902M", Image="https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Flag_of_Latvia.svg/800px-Flag_of_Latvia.svg.png"},
                new Euroopa {nameOfCountry="Lithuania", nameOfCapital="Vilnius", People="2.795M", Image="https://upload.wikimedia.org/wikipedia/commons/1/11/Flag_of_Lithuania.svg"},
                new Euroopa {nameOfCountry="German", nameOfCapital="Berlin", People="83.24M", Image="https://upload.wikimedia.org/wikipedia/commons/b/ba/Flag_of_Germany.svg"}
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
                ItemsSource = europe,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "nameOfCountry");
                    Binding companyBinding = new Binding { Path = "nameOfCapital", StringFormat = "Tore telefon filmalt {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    Binding a = new Binding { Path = "People", StringFormat = "People: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, a);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image");
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

            string NameOfCountry = await DisplayPromptAsync("What name of this country?", " ", keyboard: Keyboard.Text);
            string NameOfCapital = await DisplayPromptAsync("What name of capital?", "Write it down:", keyboard: Keyboard.Text);
            string population = await DisplayPromptAsync("What population?", "Write it down:", keyboard: Keyboard.Numeric);
            string image = await DisplayPromptAsync("Write a link of the image?", "Write it down:", keyboard: Keyboard.Text);

            europe.Add(item: new Euroopa { nameOfCountry = NameOfCountry, nameOfCapital = NameOfCapital, People = population, Image = image });

        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Euroopa eur = List.SelectedItem as Euroopa;
            if (eur != null)
            {
                europe.Remove(eur);
                List.SelectedItem = null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Euroopa selectedPhone = e.Item as Euroopa;
            if (selectedPhone != null)
                await DisplayAlert("Выбранная модель", $"{selectedPhone.nameOfCountry}-{selectedPhone.nameOfCapital}", "OK");
        }
    }
}