using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elemendide_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EuroopaRiigid : ContentPage
    {
        public ObservableCollection<Euroopa> Countries { get; set; }
        Label lbl_list;
        ListView List;
        Button add, delete;
        public EuroopaRiigid()
        {

            Countries = new ObservableCollection<Euroopa>
            {
                new Euroopa {nameOfCountry="Estonia", nameOfCapital="Tallinn", People="1 328 439", Image="https://upload.wikimedia.org/wikipedia/commons/thumb/8/8f/Flag_of_Estonia.svg/255px-Flag_of_Estonia.svg.png"},
                new Euroopa {nameOfCountry="Russia", nameOfCapital="Moscow", People="147 000 000", Image="https://www.advantour.com/russia/images/symbolics/russia-flag.jpg"},
                new Euroopa {nameOfCountry="German", nameOfCapital="Berlin", People="83 700 000", Image="https://upload.wikimedia.org/wikipedia/en/thumb/b/ba/Flag_of_Germany.svg/1200px-Flag_of_Germany.svg.png"},
                new Euroopa {nameOfCountry="France", nameOfCapital="Paris", People="76 000 000", Image="https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Flag_of_France_%28lighter_variant%29.svg/250px-Flag_of_France_%28lighter_variant%29.svg.png"}
            };
            lbl_list = new Label
            {
                Text = "List of countries",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            List = new ListView
            {
                SeparatorColor = Color.AliceBlue,
                Header = "Country",

                HasUnevenRows = true,
                ItemsSource = Countries,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.White, DetailColor = Color.White };
                    imageCell.SetBinding(ImageCell.TextProperty, "nameOfCountry");
                    Binding companyBinding = new Binding { Path = "nameOfCapital", StringFormat = " {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    Binding a = new Binding { Path = "People", StringFormat = "About the population: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, a);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image");
                    return imageCell;

                })
            };
            add = new Button { Text = "Add country" };
            delete = new Button { Text = "Delete country" };
            List.ItemTapped += List_ItemTapped;
            delete.Clicked += Delete_Clicked;
            add.Clicked += Add_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, List, add, delete } };
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            // Write Country
            string Country = await DisplayPromptAsync("Which country do you want to add??", "Write it down:", keyboard: Keyboard.Text);
            // Write Capital
            string Capital = await DisplayPromptAsync("What is its capital?", "Write it down:", keyboard: Keyboard.Text);
            // Write Population of country
            string Population = await DisplayPromptAsync("How many people live there?", "Write it down:", keyboard: Keyboard.Telephone);
            // Write link of flag
            string image = await DisplayPromptAsync("Enter a photo of the flag", "Write it down:", keyboard: Keyboard.Text);
            
            if (Country == "" || Capital == "" || Population == "" || image == "") return;
            Euroopa newest = new Euroopa { nameOfCountry = Country, nameOfCapital = Capital, People = Population, Image = image };
            foreach (Euroopa thing in Countries)
            {
                if (thing.nameOfCountry == newest.nameOfCountry)
                    return;
            }
            Countries.Add(item: newest);
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            Euroopa country = List.SelectedItem as Euroopa;
            if (country != null)
            {
                Countries.Remove(country);
                List.SelectedItem = null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Euroopa selectedCountry = e.Item as Euroopa;
            if (selectedCountry != null)
            {
                await DisplayAlert("Country", $"{selectedCountry.nameOfCapital}-{selectedCountry.nameOfCapital}", "OK");
            }               
        }
    }
}