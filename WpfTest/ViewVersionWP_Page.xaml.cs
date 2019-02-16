using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for ViewVersionWP_Page.xaml
    /// </summary>
    public partial class ViewVersionWP_Page : Page
    {
        public ViewVersionWP_Page()
        {
            InitializeComponent();
        }
        public class Outer
        {
            public string _type { get; set; }
            public int total { get; set; }
            public int count { get; set; }

            public EmbeddedWorkPackage _embedded { get; set; }
        }

        public class EmbeddedWorkPackage
        {
            public List<WorkPackage> elements { get; set; }
        }
        public class Links
        {
            public Version version { get; set; }
        }
        public class Version
        {
            public string href { get; set; }
            public string title { get; set; }
        }
        public class WorkPackage
        {

            public string id { get; set; }
            public string subject { get; set; }
            public string spentTime { get; set; }
            public Links _links { get; set; }
            public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
            {
                WorkPackage new_wp = new WorkPackage()
                {
                    id = wp.id,
                    subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
                    spentTime = XmlConvert.ToTimeSpan(wp.spentTime).TotalHours.ToString() + "H",
                    _links = wp._links
                };
                return new_wp;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://luattest2.openproject.com/");
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            string project_id = (App.Current as App).project_id;
            string version_name = (App.Current as App).version_name;
            var endpoint = "api/v3/projects/"+ project_id + "/work_packages";
            var request = new RestSharp.RestRequest(endpoint, Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);
            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
              new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));
            var WP = new ObservableCollection<WorkPackage>();
            foreach (var item in wp_without_newline)
                WP.Add(item);
            for (int i = 0; i < WP.Count();)
            {
                if (String.IsNullOrEmpty(WP[i]._links.version.title))
                {
                    //assign emptry string value to prevent null, so filter can work
                    WP[i]._links.version.title = "";
                    i++;
                    continue;
                }
                else
                {
                    i++;
                    continue;
                }
            }
            var filter= WP.Where(WP_item => WP_item._links.version.title.Contains(version_name));
            wpListView.ItemsSource = filter;
        }

        private void LogTime_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
