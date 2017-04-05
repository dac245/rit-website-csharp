using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace ISTEProj3
{
    public partial class Form1 : Form
    {
        List<Panel> ListOfPanels = new List<Panel>();

        public Form1()
        {
            InitializeComponent();
            AboutAPI();
            DegreeAPI();
            MinorsAPI();
            EmploymentAPI();
            PeopleAPI();
            ResearchAPI();
            ResourcesAPI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        public void AboutAPI()
        {
            // get About information
            string jsonAbout = getRestData("/about/");

            // need a way to get the JSON form into an About object
            About about = JToken.Parse(jsonAbout).ToObject<About>();

            // start displaying the About object information on the screen
            label1.Text = about.title;
            label2.Text = about.description;
            label3.Text = about.quote;
            label4.Text = about.quoteAuthor;
        }

        public void DegreeAPI()
        {
            string getDegreeData = getRestData("/degrees/");
            Degrees degrees = JToken.Parse(getDegreeData).ToObject<Degrees>();

            undergrad1.Text = degrees.undergraduate[0].title;
            undergrad2.Text = degrees.undergraduate[1].title;
            undergrad3.Text = degrees.undergraduate[2].title;

            undergrad1.Text += "\n" + "(" + degrees.undergraduate[0].degreeName + ")";
            undergrad2.Text += "\n" + "(" + degrees.undergraduate[1].degreeName + ")";
            undergrad3.Text += "\n" + "(" + degrees.undergraduate[2].degreeName + ")";

            grad1.Text = degrees.graduate[0].title;
            grad2.Text = degrees.graduate[1].title;
            grad3.Text = degrees.graduate[2].title;
            grad4.Text = degrees.graduate[3].degreeName;

            grad1.Text += "\n" + "(" + degrees.graduate[0].degreeName + ")";
            grad2.Text += "\n" + "(" + degrees.graduate[1].degreeName + ")";
            grad3.Text += "\n" + "(" + degrees.graduate[2].degreeName + ")";
        }

        public void MinorsAPI()
        {
            string getMinorsData = getRestData("/minors/");
            Minors minors = JToken.Parse(getMinorsData).ToObject<Minors>();

            minor1.Text = minors.UgMinors[0].title;
            minor2.Text = minors.UgMinors[1].title;
            minor3.Text = minors.UgMinors[2].title;
            minor4.Text = minors.UgMinors[3].title;
            minor5.Text = minors.UgMinors[4].title;
            minor6.Text = minors.UgMinors[5].title;
            minor7.Text = minors.UgMinors[6].title;
            minor8.Text = minors.UgMinors[7].title;
        }

        public void EmploymentAPI()
        {
            string getEmploymentData = getRestData("/employment/");
            Employment employment = JToken.Parse(getEmploymentData).ToObject<Employment>();

            empIntro.Text = employment.introduction.title;
            emp1.Text = employment.introduction.content[0].title;
            emp2.Text = employment.introduction.content[1].title;

            emp1Content.Text = employment.introduction.content[0].description;
            emp2Content.Text = employment.introduction.content[1].description;

            degStats.Text = employment.degreeStatistics.title;

            groupBox1.Text = employment.degreeStatistics.statistics[0].value;
            groupBox2.Text = employment.degreeStatistics.statistics[1].value;
            groupBox3.Text = employment.degreeStatistics.statistics[2].value;
            groupBox4.Text = employment.degreeStatistics.statistics[3].value;
            label7.Text = employment.degreeStatistics.statistics[0].description;
            label8.Text = employment.degreeStatistics.statistics[1].description;
            label9.Text = employment.degreeStatistics.statistics[2].description;
            label10.Text = employment.degreeStatistics.statistics[3].description;

            coopTable.Text = employment.coopTable.title;
            empTable.Text = employment.employmentTable.title;
        }

        public void PeopleAPI()
        {
            string getPeopleData = getRestData("/people/");
            People people = JToken.Parse(getPeopleData).ToObject<People>();

            label11.Text = people.title;
            label12.Text = people.subTitle;

            ImageList facImg = new ImageList();
            ImageList staffImg = new ImageList();
            facImg.ImageSize = new Size(54, 64);
            facImg.ColorDepth = ColorDepth.Depth32Bit;
            staffImg.ImageSize = new Size(54, 64);
            staffImg.ColorDepth = ColorDepth.Depth32Bit;

            foreach (Faculty thisFac in people.faculty)
            {
                facImg.Images.Add(LoadImage(thisFac.imagePath));
            }

            listView1.LargeImageList = facImg;

            for (var i = 0; i < facImg.Images.Count; i++)
            {
                listView1.Items.Add(people.faculty[i].name, i);
            }

            // break between faculy and staff

            foreach (Staff thisStaff in people.staff)
            {
                staffImg.Images.Add(LoadImage(thisStaff.imagePath));
            }

            listView2.LargeImageList = staffImg;

            for (var i = 0; i < staffImg.Images.Count; i++)
            {
                listView2.Items.Add(people.staff[i].name, i);
            }
        }

        public void ResearchAPI()
        {
            string jsonAbout = getRestData("/research/");
            Research research = JToken.Parse(jsonAbout).ToObject<Research>();
            Button[] listOfIntButtons = new Button[]{res1, res2, res3, res4, res5, res6,
                                                 res7, res8, res9, res10, res11, res12};
            Button[] listOfFacButtons = new Button[]{byFac1, byFac2, byFac3, byFac4, byFac5,
                                                     byFac6, byFac7, byFac8, byFac9, byFac10,
                                                     byFac11, byFac12, byFac13, byFac14, byFac15,
                                                     byFac16, byFac17, byFac18, byFac19, byFac20, byFac21};

            for (int btns = 0; btns < listOfIntButtons.Length; btns++)
            {
                listOfIntButtons[btns].Text = research.byInterestArea[btns].areaName;
            }

            for (int btns2 = 0; btns2 < listOfFacButtons.Length; btns2++)
            {
                listOfFacButtons[btns2].Text = research.byFaculty[btns2].facultyName;
            }
        }

        public void ResourcesAPI()
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();
            Button[] istButtons = new Button[]{ist1, ist2, ist3, ist4, ist5, ist6, ist7, ist8};
            LinkLabel[] ListLL = new LinkLabel[] { f2, f3, f4, f5, f6, f7, f8 };

            label15.Text = resources.title;
            label16.Text = resources.subTitle;

            tabPage1.Text = resources.studyAbroad.title;
            tabPage2.Text = resources.studentServices.title;
            tabPage3.Text = resources.tutorsAndLabInformation.title;
            tabPage4.Text = resources.studentAmbassadors.title;
            tabPage5.Text = "Forms";
            tabPage6.Text = resources.coopEnrollment.title;

            label17.Text = resources.studyAbroad.title;
            label18.Text = resources.studyAbroad.description;

            label19.Text = resources.studyAbroad.places[0].nameOfPlace;
            label20.Text = resources.studyAbroad.places[1].nameOfPlace;

            label21.Text = resources.studyAbroad.places[0].description;
            label22.Text = resources.studyAbroad.places[1].description;

            label23.Text = resources.studentServices.academicAdvisors.title;
            label24.Text = resources.studentServices.academicAdvisors.description;

            label25.Text = resources.studentServices.facultyAdvisors.title;
            label26.Text = resources.studentServices.facultyAdvisors.description;

            label27.Text = resources.studentServices.academicAdvisors.faq.title;

            label29.Text = resources.studentServices.professonalAdvisors.title;

            ad1.Text = resources.studentServices.professonalAdvisors.advisorInformation[0].name;
            ad2.Text = resources.studentServices.professonalAdvisors.advisorInformation[1].name;
            ad3.Text = resources.studentServices.professonalAdvisors.advisorInformation[2].name;

            label30.Text = resources.studentServices.istMinorAdvising.title;

            for(int q = 0; q < istButtons.Length; q++)
            {
                istButtons[q].Text = resources.studentServices.istMinorAdvising.minorAdvisorInformation[q].advisor;
            }

            label28.Text = resources.tutorsAndLabInformation.title;

            label31.Text = resources.tutorsAndLabInformation.description;

            pictureBox1.ImageLocation = resources.studentAmbassadors.ambassadorsImageSource;

            am1.Text = resources.studentAmbassadors.subSectionContent[0].title;
            am2.Text = resources.studentAmbassadors.subSectionContent[1].title;
            am3.Text = resources.studentAmbassadors.subSectionContent[2].title;
            am4.Text = resources.studentAmbassadors.subSectionContent[3].title;
            am5.Text = resources.studentAmbassadors.subSectionContent[4].title;
            am6.Text = resources.studentAmbassadors.subSectionContent[5].title;
            am7.Text = resources.studentAmbassadors.subSectionContent[6].title;

            textBox1.Text = resources.studentAmbassadors.subSectionContent[0].description;
            textBox2.Text = resources.studentAmbassadors.subSectionContent[1].description;
            textBox3.Text = resources.studentAmbassadors.subSectionContent[2].description;
            textBox4.Text = resources.studentAmbassadors.subSectionContent[3].description;
            textBox5.Text = resources.studentAmbassadors.subSectionContent[4].description;
            textBox6.Text = resources.studentAmbassadors.subSectionContent[5].description;
            textBox7.Text = resources.studentAmbassadors.subSectionContent[6].description;
            textBox7.Text += " " + resources.studentAmbassadors.applicationFormLink;

            f1.Text = resources.forms.undergraduateForms[0].formName;

            for (int ll = 0; ll < ListLL.Length; ll++)
            {
                ListLL[ll].Text = resources.forms.graduateForms[ll].formName;
            }

            label35.Text = resources.coopEnrollment.title;

            label36.Text = resources.coopEnrollment.enrollmentInformationContent[0].title;
            label37.Text = resources.coopEnrollment.enrollmentInformationContent[1].title;
            label38.Text = resources.coopEnrollment.enrollmentInformationContent[2].title;
            label39.Text = resources.coopEnrollment.enrollmentInformationContent[3].title;

            richTextBox1.Text = resources.coopEnrollment.enrollmentInformationContent[0].description;
            richTextBox2.Text = resources.coopEnrollment.enrollmentInformationContent[1].description;
            richTextBox3.Text = resources.coopEnrollment.enrollmentInformationContent[2].description;
            richTextBox4.Text = resources.coopEnrollment.enrollmentInformationContent[3].description;

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

        #region Navigation button clicks to make them visible or not
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel4.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel5.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            panel6.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
        }
        #endregion

        #region Undergrate and Graduate Information popup events
        private void undergrad1_Click(object sender, EventArgs e)
        {
            ShowUndergradInfo(0);
        }

        private void undergrad2_Click(object sender, EventArgs e)
        {
            ShowUndergradInfo(1);
        }

        private void undergrad3_Click(object sender, EventArgs e)
        {
            ShowUndergradInfo(2);
        }

        private void ShowUndergradInfo(int i)
        {
            string getDegreeData = getRestData("/degrees/");
            String getcon = "";
            Degrees degrees = JToken.Parse(getDegreeData).ToObject<Degrees>();

            foreach (String concen1 in degrees.undergraduate[i].concentrations)
            {
                getcon += concen1 + "\n";
            }

            MessageBox.Show(
                degrees.undergraduate[i].title + "\n\n" +
                degrees.undergraduate[i].description + "\n\n" +
                getcon
            );
        }

        private void grad1_Click(object sender, EventArgs e)
        {
            ShowGradInfo(0);
        }

        private void grad2_Click(object sender, EventArgs e)
        {
            ShowGradInfo(1);
        }

        private void grad3_Click(object sender, EventArgs e)
        {
            ShowGradInfo(2);
        }

        private void grad4_Click(object sender, EventArgs e)
        {
            ShowGradInfo(3);
        }

        private void ShowGradInfo(int i)
        {
            string getDegreeData = getRestData("/degrees/");
            String getcon = "";
            Degrees degrees = JToken.Parse(getDegreeData).ToObject<Degrees>();

            if (i != 3)
            {
                foreach (String concen1 in degrees.graduate[i].concentrations)
                {
                    getcon += concen1 + "\n";
                }

                MessageBox.Show(
                    degrees.graduate[i].title + "\n\n" +
                    degrees.graduate[i].description + "\n\n" +
                    getcon
                );
            }
            else
            {
                foreach (String concen1 in degrees.graduate[i].availableCertificates)
                {
                    getcon += concen1 + "\n";
                }

                MessageBox.Show(
                    degrees.graduate[i].degreeName + "\n\n" +
                    getcon
                );
            }
        }

        #endregion

        #region Undergraduate Minors Area popup events
        private void minor1_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(0);
        }

        private void minor2_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(1);
        }

        private void minor3_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(2);
        }

        private void minor4_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(3);
        }

        private void minor5_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(4);
        }

        private void minor6_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(5);
        }

        private void minor7_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(6);
        }

        private void minor8_Click(object sender, EventArgs e)
        {
            ShowMinorsInfo(7);
        }

        private void ShowMinorsInfo(int i)
        {
            string getMinorsData = getRestData("/minors/");
            Minors minors = JToken.Parse(getMinorsData).ToObject<Minors>();
            String getcourses = "";

            foreach (String concen1 in minors.UgMinors[i].courses)
            {
                getcourses += concen1 + "\n";
            }

            MessageBox.Show(
                minors.UgMinors[i].title + "\n\n" +
                minors.UgMinors[i].description + "\n\n" +
                getcourses
            );
        }


        #endregion

        #region Co-op and Employment Tables to make the custom form popup
        private void coopTable_Click(object sender, EventArgs e)
        {
            Tables coopForm = new Tables();
            coopForm.Show();
        }

        private void empTable_Click(object sender, EventArgs e)
        {
            Tables2 empForm = new Tables2();
            empForm.Show();
        }

        private void mapLoc_Click(object sender, EventArgs e)
        {
            WebMap webmap = new WebMap();
            webmap.Show();
        }

        #endregion

        #region To get the images from the faculty and staff and click popups
        private Image LoadImage(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();

            Image img = Image.FromStream(responseStream);

            responseStream.Dispose();

            return img;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string getPeopleData = getRestData("/people/");
            People people = JToken.Parse(getPeopleData).ToObject<People>();

            int li1 = listView1.SelectedItems[0].Index;

            MessageBox.Show(
                "Name: " + people.faculty[li1].name + "\n\n" +
                "Title: " + people.faculty[li1].title + "\n\n" +
                "Office: " + people.faculty[li1].office + "\n\n" +
                "E-mail: " + people.faculty[li1].email + "\n\n" +
                "Phone: " + people.faculty[li1].phone
                );
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            string getPeopleData = getRestData("/people/");
            People people = JToken.Parse(getPeopleData).ToObject<People>();

            int li2 = listView2.SelectedItems[0].Index;

            MessageBox.Show(
                    "Name: " + people.staff[li2].name + "\n\n" +
                    "Title: " + people.staff[li2].title + "\n\n" +
                    "Office: " + people.staff[li2].office + "\n\n" +
                    "E-mail: " + people.staff[li2].email + "\n\n" +
                    "Phone: " + people.staff[li2].phone
                );
        }

        #endregion

        #region Button clicks of of interest area buttons
        private void res1_Click(object sender, EventArgs e)
        {
            GetCitations(0);
        }

        private void res2_Click(object sender, EventArgs e)
        {
            GetCitations(1);
        }

        private void res3_Click(object sender, EventArgs e)
        {
            GetCitations(2);
        }

        private void res4_Click(object sender, EventArgs e)
        {
            GetCitations(3);
        }

        private void res5_Click(object sender, EventArgs e)
        {
            GetCitations(4);
        }

        private void res6_Click(object sender, EventArgs e)
        {
            GetCitations(5);
        }

        private void res7_Click(object sender, EventArgs e)
        {
            GetCitations(6);
        }

        private void res8_Click(object sender, EventArgs e)
        {
            GetCitations(7);
        }

        private void res9_Click(object sender, EventArgs e)
        {
            GetCitations(8);
        }

        private void res10_Click(object sender, EventArgs e)
        {
            GetCitations(9);
        }

        private void res11_Click(object sender, EventArgs e)
        {
            GetCitations(10);
        }

        private void res12_Click(object sender, EventArgs e)
        {
            GetCitations(11);
        }

        public void GetCitations(int cita)
        {
            string jsonAbout = getRestData("/research/");
            Research research = JToken.Parse(jsonAbout).ToObject<Research>();
            String InterPopups = "";

            foreach (String resInter in research.byInterestArea[cita].citations)
            {
                InterPopups += resInter;
            }

            Citas CitBox = new Citas(InterPopups);

            CitBox.Show();
        }
        #endregion

        #region Button clicks of the faculty area buttons
        private void byFac1_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(0);
        }

        private void byFac2_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(1);
        }

        private void byFac3_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(2);
        }

        private void byFac4_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(3);
        }

        private void byFac5_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(4);
        }

        private void byFac6_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(5);
        }

        private void byFac7_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(6);
        }

        private void byFac8_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(7);
        }

        private void byFac9_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(8);
        }

        private void byFac10_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(9);
        }

        private void byFac11_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(10);
        }

        private void byFac12_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(11);
        }

        private void byFac13_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(12);
        }

        private void byFac14_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(13);
        }

        private void byFac15_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(14);
        }

        private void byFac16_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(15);
        }

        private void byFac17_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(16);
        }

        private void byFac18_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(17);
        }

        private void byFac19_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(18);
        }

        private void byFac20_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(19);
        }

        private void byFac21_Click(object sender, EventArgs e)
        {
            GetCitationsDeux(20);
        }

        public void GetCitationsDeux(int cita)
        {
            string jsonAbout = getRestData("/research/");
            Research research = JToken.Parse(jsonAbout).ToObject<Research>();
            String FacPopups = "";

            foreach (String resFac in research.byFaculty[cita].citations)
            {
                FacPopups += resFac;
            }

            Citas CitBox = new Citas(FacPopups);

            CitBox.Show();
        }
        #endregion

        #region For the resources tab
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            Process.Start(resources.studentServices.academicAdvisors.faq.contentHref);
        }

        private void ad1_Click(object sender, EventArgs e)
        {
            GetAdvisorInfo(0);
        }

        private void ad2_Click(object sender, EventArgs e)
        {
            GetAdvisorInfo(1);
        }

        private void ad3_Click(object sender, EventArgs e)
        {
            GetAdvisorInfo(2);
        }

        public void GetAdvisorInfo(int TheAd)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            MessageBox.Show(
                    resources.studentServices.professonalAdvisors.advisorInformation[TheAd].name + "\n\n" +
                    resources.studentServices.professonalAdvisors.advisorInformation[TheAd].department + "\n\n" +
                    resources.studentServices.professonalAdvisors.advisorInformation[TheAd].email
                );
        }

        private void ist1_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(0);
        }

        private void ist2_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(1);
        }

        private void ist3_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(2);
        }

        private void ist4_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(3);
        }

        private void ist5_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(4);
        }

        private void ist6_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(5);
        }

        private void ist7_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(6);
        }

        private void ist8_Click(object sender, EventArgs e)
        {
            GetMinorAdvisorInfo(7);
        }

        public void GetMinorAdvisorInfo(int TheAd)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            MessageBox.Show(
                    resources.studentServices.istMinorAdvising.minorAdvisorInformation[TheAd].advisor + "\n\n" +
                    resources.studentServices.istMinorAdvising.minorAdvisorInformation[TheAd].title + "\n\n" +
                    resources.studentServices.istMinorAdvising.minorAdvisorInformation[TheAd].email
                );
        }
        #endregion

        #region For the tutors and Lab tab
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            Process.Start(resources.tutorsAndLabInformation.tutoringLabHoursLink);
        }

        #endregion

        #region LinkLabel clicks for forms tab
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            Process.Start("http://ist.rit.edu/" + resources.forms.undergraduateForms[0].href);
        }

        private void f2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(0);
        }

        private void f3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(1);
        }

        private void f4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(2);
        }

        private void f5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(3);
        }

        private void f6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(4);
        }

        private void f7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(5);
        }

        private void f8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetUndergradHref(6);
        }

        public void SetUndergradHref(int under)
        {
            string getResourcesData = getRestData("/resources/");
            Resources resources = JToken.Parse(getResourcesData).ToObject<Resources>();

            Process.Start("http://ist.rit.edu/" + resources.forms.graduateForms[under].href);
        }
        #endregion
    }
}
