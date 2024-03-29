﻿using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Core.Utilities.Results.Interface
{
    public interface IResponse
    {
        string Meessage { get; set; }
        ResponseType ResponseType { get; set; }
        List<CustomValidationErrors> ValidationErrors { get; set; }
    }
}
