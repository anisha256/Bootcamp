using Bootcamp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Write
{
    public class CreateItem
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateItem(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


    }
}
