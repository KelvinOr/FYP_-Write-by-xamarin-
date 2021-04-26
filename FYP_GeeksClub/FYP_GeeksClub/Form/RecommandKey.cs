using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace FYP_GeeksClub.Form
{
    public class RecommandKey
    {
        private List<string> keylist = new List<string>()
        {
            "intel",
            "AMD",
            "NVIDIA",
            "SSD",
            "HDD",
            "Windows",
            "Mac",
            "Apple",
            "Big Sur",
            "Laptop",
            "3DMark",
            "cinebench",
            "Review",
            "Bench Mark",
            "cpu-z",
            "gpu-z",
            "core",
            "ryzen",
            "ram",
            "i3",
            "R3",
            "1080p",
            "2k",
            "power",
            "MSI",
            "ASUS",
            "gigabyte",
            "zen",
            "11th",
            "CPU",
            "GPU",
            "Harddisk",
            "MotherBoard",
            "Power Supply",
            "RAM"
        };

        public List<string> GetKey()
        {
            switch(Preferences.Get("UseCase", ""))
            {
                case "Audiovisual":
                    keylist.Add("i7");
                    keylist.Add("r7");
                    keylist.Add("4k");
                    keylist.Add("8k");
                    keylist.Add("SRGB");
                    keylist.Add("ARGB");
                    keylist.Add("SoundCard");
                    break;
                case "Height_GPU&&CPU":
                case "Mid&&Hight_m":
                    keylist.Add("i5");
                    keylist.Add("r5");
                    keylist.Add("i7");
                    keylist.Add("r7");
                    keylist.Add("144hz");
                    keylist.Add("240hz");
                    keylist.Add("GTX");
                    keylist.Add("RTX");
                    keylist.Add("RX");
                    break;
                case "Low":
                    keylist.Add("APU");
                    break;
                case "Height_CPUf":
                    keylist.Add("i5");
                    keylist.Add("r5");
                    keylist.Add("GTX");
                    keylist.Add("RTX");
                    break;
                case "Hight_Gpu":
                    keylist.Add("i5");
                    keylist.Add("r5");
                    keylist.Add("GTX");
                    keylist.Add("RTX");
                    keylist.Add("RX");
                    keylist.Add("3080");
                    keylist.Add("3090");
                    keylist.Add("750W");
                    break;
                default:
                    break;
            }

            return keylist;
        }


        public Specification GenRecommandKey()
        {
            switch(Preferences.Get("UseCase", ""))
            {
                case "Low":
                    return (new Specification("i3 or APU", null , 8, "SSD 256GB - 1TB", "1080p 60hz"));
                    break;
                case "Audiovisual":
                    return (new Specification("i7/R7 upper", 60, 16,"SSD 1TB or upper and HDD(If you need)", "2k-4k hight color standard"));
                    break;
                case "Hight_Gpu":
                    return (new Specification("i3/R3 or upper", 70, 16, "SSD 256 or upper, HDD 2TB ", "2k-4k(hight color stadard if you are designer)"));
                    break;
                case "Height":
                    return (new Specification("i7/R7 or upper", 70, 16, "SSD 256 or upper, HDD 2TB ", "2k-4k hight color standard"));
                    break;
                case "Height_CPUf":
                    return (new Specification("i5/R5 or upper", 50, 16, "SSD 256 or upper, HDD 2TB ", "2k-4k(hight color stadard if you are designer)"));
                    break;
                case "Height_GPU&&CPU":
                case "Mid&&Hight_m":
                    return (new Specification("i3/R3 or upper", 50, 16, "SSD 256 or upper, HDD 1TB ", "1080p 144hz or upper"));
                    break;
                default:
                    return (new Specification("i3 or APU or upper", null, 8, "SSD 256GB - 1TB", "1080p 60hz"));
            }
        }

        public class Specification {
            public Specification(string cpu, int? gpu , int ram, string harddisk, string monitor)
            {
                this.cpu = cpu;
                this.gpu = gpu;
                this.ram_size = ram;
                this.harddisk = harddisk;
                this.monitor = monitor;

                if (gpu == null)
                {
                    this.power_supply = 400;
                }
                else if (gpu <= 60)
                {
                    this.power_supply = 600;
                }
                else if (gpu <= 70)
                {
                    this.power_supply = 750;
                }
                
            }

            public string cpu { get; set; }
            public int? gpu { get; set; }
            public int ram_size { get; set; }
            public string harddisk { get; set; }
            public int power_supply { get; set; }
            public string monitor { get; set; }            
        }
    }
}
