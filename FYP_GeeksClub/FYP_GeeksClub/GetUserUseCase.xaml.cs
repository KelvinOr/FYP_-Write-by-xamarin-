using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class GetUserUseCase : ContentPage { 
    
        private List<string> usecase = new List<string>()
        {
            "Paperwork",
            "Gaming",
            "Audiovisual work",
            "Build 3D module",
            "Game Design",
            "Just watch video and using internet",
            "Other"
        };

        private List<string> gametype = new List<string>
        {
            "3A",
            "Emulator",
            "MMORPG",
            "Simulation Game",
            "Strategy game",
            "FPS",
            "MOBA"
        };

        public GetUserUseCase()
        {
            InitializeComponent();

            foreach(string text in usecase)
            {
                picker.Items.Add(text);
            }

            
        }

        private void btn_Next_Clicked(System.Object sender, System.EventArgs e)
        {
            St_UseCaseSelect.IsVisible = false;
            st_showrecommand.IsVisible = true;
            UseCaseClasstification();
            var get = new RecommandKey();
            cpu.Text = "CPU: " + get.GenRecommandKey().cpu;
            if (get.GenRecommandKey().gpu >= 60)
            {
                gpu.Text = "GPU: RTX30" + get.GenRecommandKey().gpu + " or upper";
            } else if (get.GenRecommandKey().gpu == 50)
            {
                gpu.Text = "GPU: GTX16" + get.GenRecommandKey().gpu + " or upper";
            } else if (get.GenRecommandKey().gpu == null)
            {
                gpu.Text = "GPU: null";
            }
            ram.Text = "RAM: " + get.GenRecommandKey().ram_size + "GB or upper";
            harddisk.Text = "Hard Disk: " + get.GenRecommandKey().harddisk + "or upper";
            mon.Text = "Monitor: " + get.GenRecommandKey().monitor;
            power.Text = "Suggest: " + get.GenRecommandKey().power_supply + "W or upper";
            
        }

        private void picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (picker.SelectedItem == "Gaming")
            {
                sf_detail_layout.IsVisible = true;
                foreach (string text in gametype)
                {
                    detail_picker.Items.Add(text);
                }
            }
            else { sf_detail_layout.IsVisible = false; }
        }

        private void UseCaseClasstification()
        {
            if (Preferences.ContainsKey("UseCase"))
            {
                Preferences.Clear("UseCase");
            }

            switch (picker.SelectedItem)
            {
                case "Gaming":
                    GamingSpecificationClasstification(detail_picker.SelectedItem.ToString());
                    break;
                case "Paperwork":
                case "Just watch video and using internet":
                    Preferences.Set("UseCase", "Low");
                    break;
                case "Audiovisual work":
                    Preferences.Set("UseCase", "Audiovisual");
                    break;
                case "Build 3D module":
                    Preferences.Set("UseCase", "Hight_Gpu");
                    break;
                case "Game Design":
                    Preferences.Set("UseCase", "Height");
                    break;
                case "Other":
                    Preferences.Set("UseCase", "ramdom");
                    break;
                default:
                    Preferences.Set("UseCase", "ramdom");
                    break;
            }
        }

        private void GamingSpecificationClasstification(string type)
        {
            switch (type)
            {
                case "3A":
                    Preferences.Set("UseCase", "Hight_Gpu");
                    break;
                case "Emulator":
                case "MMORPG":
                case "Simulation Game":
                case "Strategy game":
                    Preferences.Set("UseCase", "Height_CPUf");
                    break;
                case "FPS":
                    Preferences.Set("UseCase", "Height_GPU&&CPU");
                    break;
                case "MOBA":
                    Preferences.Set("UseCase", "Mid&&Hight_m");
                    break;
                default:
                    Preferences.Set("UseCase", "Height_GPU&&CPU");
                    break;
            }
        }

        private async void btn_close_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
