using System.Collections.Generic;
using Android.Content;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Phoneword
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static readonly List<string> phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.activity_main);            
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.phonewordEditText);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.phonewordText);
            Button translateButton = FindViewById<Button>(Resource.Id.translateButton);
            translateButton.ContentDescription = "save data";
            translatedPhoneWord.LabelFor = Resource.Id.phonewordEditText;
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.historyButton);
            Button hello = FindViewById<Button>(Resource.Id.myButton);
            translationHistoryButton.Hint= Resources.GetText(Resource.String.translationHistory);
            translatedPhoneWord.Text = "Changed the font";
            string translatedNumber = string.Empty;
            translateButton.Click += (sender, e) =>
            {                
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }
            };
            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
            var count = 0;
            translatedPhoneWord.Focusable = false;
            translationHistoryButton.Click += delegate {
                translationHistoryButton.Text = string.Format("{0} clicks!", count++);
                translationHistoryButton.AnnounceForAccessibility(translationHistoryButton.Text);
            };
        }
    }
}