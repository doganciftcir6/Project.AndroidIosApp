using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Core.Utilities.Results.Concrete
{
    public class Response : IResponse
    {
        public string Meessage { get; set; }
        public ResponseType ResponseType { get; set; }


        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Meessage = message;

        }
     
    }
}
