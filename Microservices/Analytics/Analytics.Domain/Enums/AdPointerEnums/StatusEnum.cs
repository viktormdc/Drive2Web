using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Analytics.Domain.Enums.AdPointerEnums
{
    public enum StatusEnum
    {
        [Description("Active")]
        Active = 1,
        [Description("Canceled")]
        Canceled = 2
    }
}
