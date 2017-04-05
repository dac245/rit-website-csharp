using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTEProj3
{
    public partial class Tables2 : Form
    {
        public Tables2()
        {
            InitializeComponent();
        }

        #region getRestData - Returns the requested API information as a string
        private string getRestData(string url)
        {
            string baseUri = "http://ist.rit.edu/api";

            // connect to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri + url);
            try
            {
                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                // Something goes wrong, get the error response, then do something with it
                WebResponse err = we.Response;
                using (Stream responseStream = err.GetResponseStream())
                {
                    StreamReader r = new StreamReader(responseStream, Encoding.UTF8);
                    string errorText = r.ReadToEnd();
                    // display or log error
                    Console.WriteLine(errorText);
                }
                throw;
            }
        } // end getRestData
        #endregion

        private void Tables2_Load(object sender, EventArgs e)
        {
            string getEmploymentData = getRestData("/employment/");
            Employment emp = JToken.Parse(getEmploymentData).ToObject<Employment>();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Employer", 160);
            listView1.Columns.Add("Degree", 100);
            listView1.Columns.Add("City", 100);
            listView1.Columns.Add("Title", 220);

            ListViewItem item;
            for (var i = 0; i < emp.employmentTable.professionalEmploymentInformation.Count; i++)
            {
                item = new ListViewItem(new String[]
                {
                    emp.employmentTable.professionalEmploymentInformation[i].employer,
                    emp.employmentTable.professionalEmploymentInformation[i].degree,
                    emp.employmentTable.professionalEmploymentInformation[i].city,
                    emp.employmentTable.professionalEmploymentInformation[i].title
                });

                // append the new row to the ListView
                listView1.Items.Add(item);
            }
        }
    }
}
