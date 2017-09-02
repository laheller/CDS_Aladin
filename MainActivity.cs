using Android.App;
using Android.Views.InputMethods;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using SysTrace = System.Diagnostics.Trace;

namespace CDS_Aladin
{
	[Activity(Label = "AladinLiteDemo", MainLauncher = true, Icon = "@mipmap/logo")]
	public class MainActivity : Activity
	{
		readonly string template = @"<html>
			<head>
				<meta charset='UTF-8'>
				<link rel='stylesheet' href='https://aladin.u-strasbg.fr/AladinLite/api/v2/latest/aladin.min.css' >
				<script type='text/javascript' src='https://code.jquery.com/jquery-1.9.1.min.js' charset='utf-8'></script>
				<script type='text/javascript' src='https://aladin.u-strasbg.fr/AladinLite/api/v2/latest/aladin.min.js' charset='utf-8'></script>
				<title>[target]</title>
			</head>
			<body>
				<div id='aladin-lite-div' style='width:auto;'>
					<script type='text/javascript'>var aladin = A.aladin('#aladin-lite-div', {fov:0.5, target: '[target]'});</script>
				</div>
			</body>
		</html>";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var imm = (InputMethodManager)GetSystemService(InputMethodService);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			var target = FindViewById<EditText>(Resource.Id.target);
			var webView = FindViewById<WebView>(Resource.Id.webView);
			webView.Settings.JavaScriptEnabled = true;
			webView.SetWebViewClient(new MyWebViewClient());
			var navigate = FindViewById<Button>(Resource.Id.navigate);
			navigate.Click += (sender, e) =>
			{
				var obj = target.Text;
				if (string.IsNullOrEmpty(obj)) return;
				imm.HideSoftInputFromWindow(target.WindowToken, HideSoftInputFlags.None);
				var page = template.Replace("[target]", obj);
				SysTrace.WriteLine("Page source: " + page);
				webView.LoadData(page, "text/html", "UTF-8");
			};
		}

		internal class MyWebViewClient : WebViewClient
		{
			public override bool ShouldOverrideUrlLoading(WebView webView, string url)
			{
				return false;
			}
		}
	}
}
