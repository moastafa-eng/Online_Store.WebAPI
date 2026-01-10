using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundEx
{
    public class ProductNotFoundEx(int id) : NotFoundEx($"Product with id:{id} is not found")
    {
    }
}
