using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitaTechAssessment
{
    public class Insurance : Parcels
    {
        //returns true if the value of a parcel is greater than 1000 and false if it's not
        public bool InsuranceSignOff(double value)
        {
            if (value > 1000)
            {
                return true;
            }
            else return false;
        }
    }
}
