using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundException(string id) :
        NotFoundException($"User With email {id} Not Found")
    {
    }
}
