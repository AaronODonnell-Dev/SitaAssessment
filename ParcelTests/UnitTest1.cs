using NUnit.Framework;
using SitaTechAssessment;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParcelTests
{
    [TestFixture]
    public class Tests : Parcels
    {
        [Test]
        //This test checks that the InsuranceSignOff Method only returns true if the value is over 1000
        public void TestInsurance()
        {
            var insurance = new Insurance();

            bool result = insurance.InsuranceSignOff(1001);
            bool result2 = insurance.InsuranceSignOff(998);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(result2, Is.EqualTo(false));
        }

        [Test]
        //This test checks that each property of the Parcel is entered correctly and contains only the correct type of input
        //and that all properties are filled in
        public void TestValidParcel()
        {
            var Parcel = new Parcels();

            Departments d1 = new Departments("Insurance", ">=0", "N/A");
            Departments d2 = new Departments("Mail", "1", "<1000");
            Departments d3 = new Departments("Regular", "10", "<1000");
            Departments d4 = new Departments("Heavy", ">10", "<1000");

            Parcels pMail = new Parcels(0.5, 75, "Aaron", "Ryan", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d2, false);

            Parcels pRegular = new Parcels(4, 175, "Aaron", "Ryan", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d3, false);

            Parcels pHeavy = new Parcels(50, 975, "Aaron", "Ryan", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d4, false);

            Parcels pInsuarnce = new Parcels(15, 1075, "Aaron", "Ryan", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d1, true);

            var mailResult = Parcel.CheckParcel(pMail);
            var regResult = Parcel.CheckParcel(pRegular);
            var heavyResult = Parcel.CheckParcel(pHeavy);
            var insuranceResult = Parcel.CheckParcel(pInsuarnce);

            Assert.IsTrue(mailResult);
            Assert.IsTrue(regResult);
            Assert.IsTrue(heavyResult);
            Assert.IsTrue(insuranceResult);
        }

        [Test]
        //This test checks that a parcel with an incorrectly entered details will fail its validity test
        public void TestInvalidParcel()
        {
            var Parcel = new Parcels();

            Departments d1 = new Departments("Insurance", ">=0", "N/A");

            //The testparcel has an intentioally incorrect format for a property to make it invalid
            Parcels testParcel = new Parcels(0.5, 75, "Aaron+-#", "Ryan", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d1, false);

            var result = Parcel.CheckParcel(testParcel);

            Assert.IsFalse(result);
        }

        [Test]
        //This test checks that a departments details are entered correctly
        public void TestValidDepartment()
        {
            var Department = new Departments();

            Departments d1 = new Departments("Insurance", ">=0", "N/A");
            Departments d2 = new Departments("Mail", "1", "<1000");

            var mailResult = Department.CheckDepartments(d1);
            var insuranceResult = Department.CheckDepartments(d2);

            Assert.IsTrue(mailResult);
            Assert.IsTrue(insuranceResult);
        }

        [Test]
        //This test checks that a department is invalid if a detail is incorrectly entered
        public void TestInvalidDepartment()
        {
            var Department = new Departments();

            Departments d1 = new Departments("Insur10ance", ">=0", "N/A");
            Departments d2 = new Departments("Mail", "1YENB", "<1000");

            var mailResult = Department.CheckDepartments(d1);
            var insuranceResult = Department.CheckDepartments(d2);

            Assert.IsFalse(mailResult);
            Assert.IsFalse(insuranceResult);
        }

        [Test]
        //This tests Parcel details ingoring case
        public void TestParcelCaseSensitivity()
        {
            Departments d0 = new Departments("Heavy", ">10", "<1000");

            Parcels pMail = new Parcels(0.5, 75, "aaron", "RYAN", "Van der Duynstraat", "55B", "2515NL", "Den Haag",
                "Derinkehir", "12", "N41DX68", "Ballinamore", d0, false);

            Assert.That(pMail.SenderName, Is.EqualTo("Aaron").IgnoreCase);
            Assert.That(pMail.RecipientName, Is.EqualTo("Ryan").IgnoreCase);
        }

        [Test]
        //This test Department details ignoring case
        public void TestDepartmentCaseSensitivity()
        {
            Departments d0 = new Departments("heavy", ">10", "<1000");

            Assert.That(d0.Name, Is.EqualTo("HEAVY").IgnoreCase);
        }

        [Test]
        public void TestDepartmentDuplicates()
        {
            var departments = new Departments();

            List<Departments> depList = new List<Departments>();

            Departments d0 = new Departments("Mail", "10", "1000");
            Departments d1 = new Departments("Mail", "15", "1000");
            Departments d2 = new Departments("Small", "25", "1000");

            depList.Add(d0);

            var falseResult = departments.CheckDepartmentDuplicate(d1, depList);
            var trueResult = departments.CheckDepartmentDuplicate(d2, depList);

            Assert.IsFalse(falseResult);
            Assert.IsTrue(trueResult);
        }
    }
}