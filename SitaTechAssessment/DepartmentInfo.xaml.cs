using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SitaTechAssessment
{
    /// <summary>
    /// Interaction logic for DepartmentInfo.xaml
    /// </summary>
    public partial class DepartmentInfo : Window
    {
        //create instances of the necessary classes
        public MainWindow main = new MainWindow();
        public Departments departments = new Departments();

        //temporary list for added, removed or edited departments so as not to make a mess of the actual list of departments
        ObservableCollection<Departments> tempDepartments = new ObservableCollection<Departments>();
        public DepartmentInfo()
        {
            InitializeComponent();

            //populate the listbox and tempdepartments list
            lbxDepartments.ItemsSource = main.departmentsList;
            tempDepartments = main.departmentsList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Departments newDep = new Departments();

            if(tbxName != null)
            {
                newDep.Name = tbxName.Text;
            }
            if(tbxWeightLimit != null)
            {
                //checks for "kg" so as not to duplicate it and it makes the department weight limit invalid as it contains letters
                if(tbxWeightLimit.Text.Contains("kg"))
                {
                    string newWeightLimit = tbxWeightLimit.Text.Replace("kg", string.Empty);
                    newDep.WeightLimit = newWeightLimit;
                }
            }
            if(tbxValueLimit != null)
            {
                //checks for € so as not to duplicate it
                if(tbxValueLimit.Text.Contains("€"))
                {
                    string newValueLimit = tbxValueLimit.Text.Replace("€", string.Empty);
                    newDep.ValueLimit = newValueLimit;
                }
            }

            tbxPackages.Text = "N/A";

            //checking validity of details entered
            if(departments.CheckDepartments(newDep))
            {
                //check for duplicate departments
                bool IsDuplicate = departments.CheckDepartmentDuplicate(newDep, tempDepartments.ToList());
                if (IsDuplicate == false)
                {
                    MessageBox.Show("Another department is too similar or the same");
                }
                else
                {
                    //use tempdepartments as the new list
                    tempDepartments.Add(newDep);
                    lbxDepartments.ItemsSource = tempDepartments;
                }
            }
            else
            {
                //error message is something is wrong
                MessageBox.Show("Invalid Department, try again");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //casting the selected item for the listbox as a department
            Departments selectedDepartment = lbxDepartments.SelectedItem as Departments;

            if(selectedDepartment != null)
            {
                //check that the selecteddepartment is valid
                if (departments.CheckDepartments(selectedDepartment))
                {
                    tempDepartments.Remove(selectedDepartment);
                    lbxDepartments.ItemsSource = tempDepartments;
                }
                else
                {
                    throw new Exception("Invalid Department, try again");
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Departments selectedDepartment = lbxDepartments.SelectedItem as Departments;

            if(selectedDepartment != null)
            {
                //checks that the parameters entered are valid then ammends the selected departments properties
                if(departments.CheckDepartments(selectedDepartment))
                {
                    selectedDepartment.Name = tbxName.Text;
                    selectedDepartment.WeightLimit = tbxWeightLimit.Text;
                    selectedDepartment.ValueLimit = tbxValueLimit.Text;
                    selectedDepartment.Packages = int.Parse(tbxPackages.Text);
                }
                else
                {
                    MessageBox.Show("Invalid Department Parameter");
                }
            }
        }

        private void lbxDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Departments selectedDepartment = lbxDepartments.SelectedItem as Departments;

            if(selectedDepartment != null)
            {
                //fills the text boxes with the info about the selected department
                tbxName.Text = selectedDepartment.Name;
                tbxWeightLimit.Text = selectedDepartment.WeightLimit +"kg";
                tbxValueLimit.Text = selectedDepartment.ValueLimit + "€";
                tbxPackages.Text = selectedDepartment.Packages.ToString();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxName.Text = "";
            tbxWeightLimit.Text = "";
            tbxValueLimit.Text = "";
            tbxPackages.Text = "";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main.Show();
        }

        //This Method would be used to save departments and write them back to an XML file, it could be edited to save 1 department
        //it is currently written to write the list of departments to the XML file
        //There would have to be a check for object duplication before writing the list to the XML file
        //it is currently not called
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Departments[] selectedDepartments = lbxDepartments.ItemsSource as Departments[];

            foreach (Departments dep in selectedDepartments)
            {
                bool isDuplicate = departments.CheckDepartments(dep);
                bool isValid = departments.CheckDepartmentDuplicate(dep, main.departmentsList.ToList());

                if (isDuplicate && isValid)
                {
                    XmlSerializer xml = new XmlSerializer(dep.GetType());

                    using (StreamWriter streamWriter = new StreamWriter(@"../../Container_6846568.xml"))
                    {
                        xml.Serialize(streamWriter, dep);
                    }
                }
            }
        }
    }
}
