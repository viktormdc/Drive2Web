using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Analytics.Domain.Enums.AdPointerEnums
{
    public enum PaymentIntervalEnum
    {
        [Description("Monthly")]
        Monthly = 1,
        [Description("Annually")]
        Annually = 2
    }
}
