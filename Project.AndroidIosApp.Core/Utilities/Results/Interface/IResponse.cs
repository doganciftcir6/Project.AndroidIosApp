using Project.AndroidIosApp.Core.Enums;
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
    }
}
