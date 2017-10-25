using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GingerbreadExchange.Models;
using System.ComponentModel.DataAnnotations;

namespace GingerbreadExchange.ViewModels
{
    public class OrderVM
    {
        public Deal DealOperation { get; set; }


        public GingerbreadVM GingerbreadVM { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        public OrderVM(Order ord)
        {
            Email = ord.Email;
            DealOperation = ord.DealOperation;
            GingerbreadVM = new GingerbreadVM(ord.Gingerbread);
        }

        public OrderVM()
        { }
    }
}