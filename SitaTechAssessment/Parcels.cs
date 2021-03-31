using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SitaTechAssessment
{
    public class Parcels
    {
        //Properties of a parcel
        public double Weight { get; set; }
        public double Value { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string DStreet { get; set; }
        public string DHouseNumber { get; set; }
        public string DPostCode { get; set; }
        public string DCity { get; set; }
        public Departments Department { get; set; }
        public bool Checked { get; set; }

        //2 Contructors for creating a new instance of the class or a parcel

        public Parcels()
        {

        }

        public Parcels(double weight, double value, string sendername, 
            string recipientname, string street, string housenumber, string postcode, string city, 
            string dstreet, string dhousenumber, string dpostcode, string dcity, Departments department, bool insurancechecked)
        {
            Weight = weight;
            Value = value;
            SenderName = sendername;
            RecipientName = recipientname;
            Street = street;
            HouseNumber = housenumber;
            PostCode = postcode;
            City = city;
            DStreet = dstreet;
            DHouseNumber = dhousenumber;
            DPostCode = dpostcode;
            DCity = dcity;
            Department = department;
            Checked = insurancechecked;
        }

        //override ToString method to make the displaying of a parcel neated for the user.
        public override string ToString()
        {
            string output = "- From " + SenderName + " - To " + RecipientName + "\r\n Weight : " + Weight + "kg, Value : " + Value;
            return output;
        }

        //checks a parcels properties are all valid and not null
        public bool CheckParcel(Parcels p)
        {
            if (p != null)
            { // checks the p.SenderName contains only spaces, periods and letters of any case a-z
                if (Regex.IsMatch(p.SenderName, @"^[\sa-zA-Z.]+$") &&
                Regex.IsMatch(p.RecipientName, @"^[\sa-zA-Z.]+$") &&
                Regex.IsMatch(p.Street, @"^[\sa-zA-Z-]+$") &&
                Regex.IsMatch(p.DStreet, @"^[\sa-zA-Z-]+$") &&
                Regex.IsMatch(p.HouseNumber, @"^[\sa-zA-Z0-9]+$") &&
                Regex.IsMatch(p.DHouseNumber, @"^[\sa-zA-Z0-9]+$") &&
                Regex.IsMatch(p.PostCode, @"^[\sa-zA-Z0-9]+$") &&
                Regex.IsMatch(p.DPostCode, @"^[\sa-zA-Z0-9]+$") &&
                Regex.IsMatch(p.City, @"^[\sa-zA-Z/]+$") &&
                Regex.IsMatch(p.DCity, @"^[\sa-zA-Z/]+$"))
                {
                    if (p.Weight > 0 && p.Value >= 0)
                    {
                        if(p.Department != null)
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
    }
}
