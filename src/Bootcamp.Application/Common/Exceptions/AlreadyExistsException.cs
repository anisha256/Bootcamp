using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Common.Exceptions
{
    public class AlreadyExistsException :Exception
    {
        public AlreadyExistsException(): base() { 
        }

        public AlreadyExistsException(string name)
        : base($"{name} already exists!!")
        {
        }
    }
}
