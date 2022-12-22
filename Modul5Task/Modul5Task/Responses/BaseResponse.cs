using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul5Task.Models;

namespace Modul5Task.Responses
{
    public class BaseResponse<T>
    where T : class
    {
        public T Data { get; set; }
        public SupportDto Support { get; set; }
    }
}
