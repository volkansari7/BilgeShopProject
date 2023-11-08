using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Dto
{
    public class AddCategotyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Id'ye gerek yok çünkü yeni eklenecek bir verinin Id'si 0'dır.
    }
}
