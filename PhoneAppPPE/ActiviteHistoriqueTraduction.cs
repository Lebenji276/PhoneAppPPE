using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
namespace PhoneAppPPE
{
    [Activity(Label = "@string/translationHistory")]
    public class TranslationHistoryActivity : ListActivity
    {
        protected override void OnCreate( Bundle bundle )
        {
            base.OnCreate(bundle);
            // Create your application here
            IList<string> phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
        }
    }
}