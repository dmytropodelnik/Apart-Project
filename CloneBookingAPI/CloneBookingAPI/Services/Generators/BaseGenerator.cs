using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class BaseGenerator
    {
        protected string _code = default;

        public string Code
        {
            get => _code;
            set
            {
                if (string.IsNullOrWhiteSpace(_code))
                {
                    throw new ArgumentNullException("Incorrect value");
                }
                _code = value;
            }
        }
    }
}
