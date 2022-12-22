using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul5Task.Responses
{
    public class UpdateUserResponse
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
