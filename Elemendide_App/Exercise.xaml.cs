using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elemendide_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercise : ContentPage
    {
        Picker picker, picker_2;
        StackLayout st;
        Image img;
        Button linkButton;
        List<string> maakonnad;
        List<string> linnad;
        List<string> links;
        string[] imgs;
        public Exercise()
        {
            links = new List<string>() { "https://et.wikipedia.org/wiki/Ida-Viru_maakond", "https://et.wikipedia.org/wiki/J%C3%B5geva_maakond",
            "https://et.wikipedia.org/wiki/Harjumaa", "https://et.wikipedia.org/wiki/J%C3%A4rvamaa", "https://et.wikipedia.org/wiki/Rapla_maakond",
            "https://et.wikipedia.org/wiki/L%C3%A4%C3%A4nemaa", "https://et.wikipedia.org/wiki/P%C3%A4rnumaa", "https://et.wikipedia.org/wiki/Viljandimaa",
            "https://et.wikipedia.org/wiki/Valgamaa", "https://et.wikipedia.org/wiki/V%C3%B5rumaa", "https://et.wikipedia.org/wiki/P%C3%B5lva_maakond",
            "https://et.wikipedia.org/wiki/Tartumaa", "https://et.wikipedia.org/wiki/L%C3%A4%C3%A4ne-Viru_maakond", "https://et.wikipedia.org/wiki/Hiiumaa",
            "https://et.wikipedia.org/wiki/Saaremaa"};

            maakonnad = new List<string>() { 
                "Ida-Virumaa", "Jõgevamaa", "Harjumaa", "Järvamaa", "Raplamaa",
                "Läänemaa", "Pärnumaa", "Viljandimaa", "Valgamaa", "Võrumaa",
                "Põlvamaa", "Tartumaa", "Lääne-Virumaa", "Hiiumaa", "Saaremaa"};
            linnad = new List<string>() {
                "Jõhvi", "Jõgeva", "Tallinn", "Paide", "Rapla",
                "Haapsalu", "Pärnu", "Viljandi", "Valga", "Võru",
                "Põlva", "Tartu", "Rakvere", "Kärdla", "Kuressaare"};

            imgs = new string[] {
                "Ida.jpg",
                "Joge.jpg",
                "Harjumaa.jpg",
                "Jarvamaa.jpg",
                "Raplamaa.png",
                "Laanemaa.jpg",
                "Parnumaa.jpg",
                "Viljandi.jpg",
                "Valgamaa.jpg",
                "Voru.jpg",
                "Polvamaa.jpg",
                "Tartumaa.jpg",
                "LaaneViirumaa.jpg",
                "Hiiumaa.png",
                "Saaremaa.jpg"
            };

            picker = new Picker
            {
                Title = "Maakonnad"
            };

            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

            picker_2 = new Picker
            {
                Title = "Linnad"
            };

            picker_2.SelectedIndexChanged += Picker_2_SelectedIndexChanged;

            /*if (picker.SelectedIndex == 0)
            {
                img = new Image() { Source = " " };
            */

            img = new Image() { Source = " " };

            linkButton = new Button()
            {
                Text = " "
            };
            linkButton.IsVisible = false ;
            linkButton.Clicked += LinkButton_Clicked;

            st = new StackLayout { Children = { picker, picker_2, img, linkButton} };
            //sb = new StackLayout { Children = { btn2 } };
            Content = st;

            for (int i = 0; i < maakonnad.Count; i++)
            {
                picker.Items.Add(maakonnad[i]);
                picker_2.Items.Add(linnad[i]);
            }
        }

        private async void LinkButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(links[picker.SelectedIndex], BrowserLaunchMode.SystemPreferred);
        }

        private void Picker_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            picker.SelectedIndex = picker_2.SelectedIndex;
            ChangeImg(picker.SelectedIndex);
            linkButton.Text = $"Info: {maakonnad[picker.SelectedIndex]}";
            linkButton.IsVisible = true;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            picker_2.SelectedIndex = picker.SelectedIndex;
            ChangeImg(picker.SelectedIndex);
            linkButton.Text = $"Info: {maakonnad[picker.SelectedIndex]}";
            linkButton.IsVisible = true;
        }

        private void ChangeImg(int index)
        {
            img.Source = imgs[index];
        }
    }
}