using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Helpers
{
    public class RegisterRuleHelper
    {
        public static IResponse Run(params IResponse[] logics)
        {
            //parametrede aldığım her bir kontrol metotunu burda tek tek döndürmem lazım.
            foreach (var logic in logics)
            {
                if (logic.ResponseType != ResponseType.Success)
                {
                    //demekki hata var. Kayıt duracak hata mesajı gözükecek.
                    return logic;
                }
            }
            //eğer hiç bir metotta hata yoksa sucess döncek
            return new Response(ResponseType.Success);
        }
    }
}
