using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace PhoneAppPPE
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Dictionary<string, Dictionary<string, Praticien>> DicoFinal = new Dictionary<string, Dictionary<string, Praticien>>();
        private Dictionary<string, string> Departement = new Dictionary<string, string>();

        private ExpandableListViewAdapter mAdapter;
        private ExpandableListView expandableListView;
        private List<string> group = new List<string>();
        private List<string> groupAff = new List<string>();
        private Dictionary<string, List<string>> dicMyMap = new Dictionary<string, List<string>>();
        protected override void OnCreate( Bundle bundle )
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            expandableListView.SetAdapter(mAdapter);

            SetData(out mAdapter);
            //Set Data
            try
            {
            }
            catch (System.Exception)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Message d'erreur");
                alert.SetMessage("Vérifiez votre connexion internet");
                alert.SetPositiveButton("Quitter", ( senderAlert, args ) =>
                {
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                });
                alert.SetNegativeButton("OK", ( senderAlert, args ) =>
                {
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
            expandableListView.SetAdapter(mAdapter);

        }

        private void SetData( out ExpandableListViewAdapter mAdapter )
        {
            mAdapter = null;
            HttpWebRequest webRequest = WebRequest.Create("https://bridge.buddyweb.fr/api/praticien/praticien") as HttpWebRequest;
            List<string> ListCP = new List<string>();
            if (webRequest == null)
            {
                return;
            }
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    string contributorsAsJson = sr.ReadToEnd();
                    List<Praticien> contributors = JsonConvert.DeserializeObject<List<Praticien>>(contributorsAsJson);
                    foreach (Praticien visiteur in contributors)
                    {
                        if (!ListCP.Contains(visiteur.MinCP))
                        {
                            ListCP.Add(visiteur.MinCP);
                            ListCP.Sort();
                        }
                    }

                    foreach (string item in ListCP)
                    {
                        Dictionary<string, Praticien> dictionaryaa = contributors.Where(x => x.MinCP == item).ToDictionary(x => x.pra_num);
                        DicoFinal.Add(item, dictionaryaa);
                    }

                }
            }

            HttpWebRequest webRequest2 = WebRequest.Create("https://geo.api.gouv.fr/departements") as HttpWebRequest;
            if (webRequest2 == null)
            {
                return;
            }
            webRequest2.ContentType = "application/json";
            webRequest2.UserAgent = "Nothing";

            using (Stream s = webRequest2.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {

                    string contributorsAsJson = sr.ReadToEnd();
                    List<Departement> contributors = JsonConvert.DeserializeObject<List<Departement>>(contributorsAsJson);
                    foreach (Departement item in contributors)
                    {
                        Departement.Add(item.code, item.nom);
                    }
                }
            }

            foreach (string item in ListCP)
            {
                group.Add(item);

                string nomDepartement = Departement[item];
                groupAff.Add("Département : " + nomDepartement + "(" + item + ")");

            }

            foreach (string item in group)
            {
                add_dicMyMap(item);
            }

            mAdapter = new ExpandableListViewAdapter(this, groupAff, dicMyMap);
        }
        public void add_dicMyMap( string dep )
        {
            int index = group.IndexOf(dep);
            List<string> groupB = new List<string>();
            foreach (KeyValuePair<string, Praticien> item in DicoFinal[group[index]])
            {
                groupB.Add(item.Value.ToString());
            }
            dicMyMap.Add(groupAff[index], groupB);
        }
    }
}