using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneAppPPE
{
    class Visiteur
    {
        private string _MinCP;
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Cp { get; set; }
        public string Ville { get; set; }
        public string IDAPI { get; set; }
        public string MinCP
        {
            get
            {
                string PremierCp = Cp[0] + "";
                string DeuxCP = Cp[1] + "";
                return PremierCp + DeuxCP;
            }
            set => _MinCP = value;
        }
        public override string ToString() => $"{Nom.ToUpper()} {Prenom}\n{Adresse}\n{Cp}\t{Ville}";
    }
}