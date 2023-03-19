using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Core.Utilities.Results.Concrete
{
    public class DataResponse<T> : Response, IDataResponse<T>
    {
        public T Data { get; set; }
        public string StringData { get; set; }
        //public List<CustomValidationErrors> ValidationErrors { get; set; }


        public DataResponse(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }
        public DataResponse(ResponseType responseType, string message) : base(responseType, message)
        {

        }
        public DataResponse(ResponseType responseType, string stringData, string message) : base(responseType, message)
        {
            StringData = stringData;
        }
        public DataResponse(ResponseType responseType, T data, List<CustomValidationErrors> errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }
    }
}
