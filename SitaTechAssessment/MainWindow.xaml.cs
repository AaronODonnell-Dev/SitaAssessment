using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace SitaTechAssessment
{
    public partial class MainWindow : Window
    {
        //creating instance of each class
        public Parcels parcels = new Parcels();
        public Insurance insurance = new Insurance();
        public Departments departments = new Departments();

        //observable collection to populate list boxes with once filled with the XML data
        public ObservableCollection<Parcels> ParcelList = new ObservableCollection<Parcels>();
        public ObservableCollection<Departments> departmentsList = new ObservableCollection<Departments>();

        public MainWindow()
        {
            InitializeComponent();
            ReadInXML();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Sort By Combo Box
            //Adding sortby options to the combobox
            cmbxSort.Items.Add("Weight");
            cmbxSort.Items.Add("Value");
            cmbxSort.Items.Add("Sender A-Z");
            cmbxSort.Items.Add("Recipient A-Z");
            #endregion
        }

        public void ReadInXML()
        {

            #region Department Info
            //Department objects and list to contain them
            Departments d1 = new Departments("Insurance", ">=0", "N/A");
            Departments d2 = new Departments("Mail", "1", "<1000");
            Departments d3 = new Departments("Regular", "10", "<1000");
            Departments d4 = new Departments("Heavy", ">10", "<1000");

            departmentsList.Add(d1);
            departmentsList.Add(d2);
            departmentsList.Add(d3);
            departmentsList.Add(d4);

            bool IsValidDepartment;
            #endregion

            #region Parcel Info
            //Creating Parcel objects
            Parcels p1 = new Parcels();
            Parcels p2 = new Parcels();
            Parcels p3 = new Parcels();
            Parcels p4 = new Parcels();

            //Adding Parcels to List of parcels
            ParcelList.Add(p1);
            ParcelList.Add(p2);
            ParcelList.Add(p3);
            ParcelList.Add(p4);

            //boolean for checking parcel validity
            bool IsValidParcel;
            #endregion

            //Temporary lists to read in for the XML
            List<string> sendernamesTemp = new List<string>();
            List<string> recipientnamesTemp = new List<string>();
            List<string> senderstreetTemp = new List<string>();
            List<string> sendershnTemp = new List<string>();
            List<string> senderpcTemp = new List<string>();
            List<string> sendercityTemp = new List<string>();
            List<string> recipientstreetTemp = new List<string>();
            List<string> recipienthnTemp = new List<string>();
            List<string> recipientpcTemp = new List<string>();
            List<string> recipientcityTemp = new List<string>();
            List<double> weightTemp = new List<double>();
            List<double> valueTemp = new List<double>();

            //Load the XML
            XmlDocument container = new XmlDocument();
            container.Load(@"../../Container_68465468.xml");

            //Pulling the data from the XML by nodes
            //populating temporary lists
            foreach (XmlNode node in container.DocumentElement.ChildNodes)
            {
                foreach(XmlNode parcels in node)
                {
                    foreach (XmlNode parcel in parcels)
                    {
                        if(parcel.Name == "Sender")
                        {
                            foreach (XmlNode sender in parcel)
                            {
                                if (sender.Name == "Name")
                                {
                                    sendernamesTemp.Add(sender.InnerText);
                                }
                                if (sender.Name == "Address")
                                {
                                    foreach (XmlNode addrs in sender)
                                    {
                                        if (addrs.Name == "Street")
                                        {
                                            senderstreetTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "HouseNumber")
                                        {
                                            sendershnTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "PostalCode")
                                        {
                                            senderpcTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "City")
                                        {
                                            sendercityTemp.Add(addrs.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                        if (parcel.Name == "Receipient")
                        {
                            foreach (XmlNode recipient in parcel)
                            {
                                if (recipient.Name == "Name")
                                {
                                    recipientnamesTemp.Add(recipient.InnerText);
                                }
                                if (recipient.Name == "Address")
                                {
                                    foreach (XmlNode addrs in recipient)
                                    {
                                        if (addrs.Name == "Street")
                                        {
                                            recipientstreetTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "HouseNumber")
                                        {
                                            recipienthnTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "PostalCode")
                                        {
                                            recipientpcTemp.Add(addrs.InnerText);
                                        }
                                        if (addrs.Name == "City")
                                        {
                                            recipientcityTemp.Add(addrs.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                        else if (parcel.Name == "Weight")
                        {
                            foreach (XmlNode weight in parcel)
                            {
                                double newweight = Convert.ToDouble(weight.InnerText);
                                weightTemp.Add(newweight);
                            }
                        }
                        else if (parcel.Name == "Value")
                        {
                            foreach (XmlNode value in parcel)
                            {
                                double newvalue = Convert.ToDouble(value.InnerText);
                                valueTemp.Add(newvalue);
                            }
                        }
                    }
                }
            }

            //set temporary list item values to Parcel properties
            for (int i = 0; i < ParcelList.Count; i++)
            {
                ParcelList[i].SenderName = sendernamesTemp[i];
                ParcelList[i].RecipientName = recipientnamesTemp[i];
                ParcelList[i].Street = senderstreetTemp[i];
                ParcelList[i].HouseNumber = sendershnTemp[i];
                ParcelList[i].PostCode = senderpcTemp[i];
                ParcelList[i].City = sendercityTemp[i];
                ParcelList[i].DStreet = recipientstreetTemp[i];
                ParcelList[i].DHouseNumber = recipienthnTemp[i];
                ParcelList[i].DPostCode = recipientpcTemp[i];
                ParcelList[i].DCity = recipientcityTemp[i];
                ParcelList[i].Weight = weightTemp[i];
                ParcelList[i].Value = valueTemp[i];
            }

            //Assign Department Info to each Parcel and check validity of parcel and department
            foreach (Parcels parcel in ParcelList)
            {
                //GetDepartmentDetails asigns each parcel a department
                GetDepartmentDetails(parcel);

                //booleans are set to true if parcel and department are validated by their individual validation methods
                //they'll return false if something is wrong
                IsValidParcel = parcels.CheckParcel(parcel);
                IsValidDepartment = departments.CheckDepartments(parcel.Department);

                //if either method returns false an error message will be displayed
                if(IsValidParcel == false)
                {
                    MessageBox.Show("Your Parcel is Invalid");
                }

                if(IsValidDepartment == false)
                {
                    MessageBox.Show("That is an invalid entry for a Department");
                }
            }
        }

        public void GetDepartmentDetails(Parcels p)
        {
            //check if it needs to be signed off by Insurance Department
            if (p.Value >= 1000 && p.Checked == false)
            {
                p.Department = departmentsList[0];
                departmentsList[0].Packages++;
            }
            //if not or it has already been checked then check it's weight
            else if (p.Value >= 1000 && p.Checked || p.Value < 1000)
            {
                if (p.Weight <= 1)
                {
                    p.Department = departmentsList[1];
                    departmentsList[1].Packages++;
                }
                else if (p.Weight > 1 && p.Weight <= 10)
                {
                    p.Department = departmentsList[2];
                    departmentsList[2].Packages++;
                }
                else if (p.Weight > 10)
                {
                    p.Department = departmentsList[3];
                    departmentsList[3].Packages++;
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //ordering by different attributes and repopulating listbox based on cmbx selected item
            //if nothing is selected it populates with the unsorted list
            switch ((string)cmbxSort.SelectedItem)
            {
                case "Weight":
                    {
                        List<Parcels> WSortedList = ParcelList.OrderBy(o => o.Weight).ToList();
                        lbxParcels.ItemsSource = WSortedList;
                    }
                    break;
                case "Value":
                    {
                        List<Parcels> VSortedList = ParcelList.OrderBy(o => o.Value).ToList();
                        lbxParcels.ItemsSource = VSortedList;
                    }
                    break;
                case "Sender A-Z":
                    {
                        List<Parcels> SSortedList = ParcelList.OrderBy(o => o.SenderName).ToList();
                        lbxParcels.ItemsSource = SSortedList;
                    }
                    break;
                case "Recipient A-Z":
                    {
                        List<Parcels> RSortedList = ParcelList.OrderBy(o => o.RecipientName).ToList();
                        lbxParcels.ItemsSource = RSortedList;
                    }
                    break;
                default:
                    lbxParcels.ItemsSource = ParcelList;
                    break;
            }
        }

        private void lbxParcels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //display selected parcel addresses in textboxes
            //cast the selected item as a parcel to access properties
            Parcels selectedParcel = lbxParcels.SelectedItem as Parcels;

            selectedParcel.Checked = insurance.InsuranceSignOff(selectedParcel.Value);

            if(selectedParcel != null)
            {
                tbxSStreet.Text = selectedParcel.Street.ToString();
                tbxSHN.Text = selectedParcel.HouseNumber.ToString();
                tbxSPC.Text = selectedParcel.PostCode.ToString();
                tbxSCity.Text = selectedParcel.City.ToString();
                tbxRStreet.Text = selectedParcel.DStreet.ToString();
                tbxRHN.Text = selectedParcel.DHouseNumber.ToString();
                tbxRPC.Text = selectedParcel.DPostCode.ToString();
                tbxRCity.Text = selectedParcel.DCity.ToString();
                tbxType.Text = selectedParcel.Department.Name.ToString();
                tbxChecked.Text = selectedParcel.Checked.ToString();
            }
        }

        private void EditDepartments_Click(object sender, RoutedEventArgs e)
        {
            //show department window once clicked
            DepartmentInfo departmentInfo = new DepartmentInfo();
            departmentInfo.Show();

            //hide main window when in department window
            this.Hide();
        }
    }
}
