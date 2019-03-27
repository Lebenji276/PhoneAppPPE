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
    class Departement
    {
        public string nom { get; set; }
        public string code { get; set; }
        public string codeRegion { get; set; }

        public override string ToString() => $"Département : {nom} ({code})";

    }
}