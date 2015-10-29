
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class TimeoutException : ApplicationException
    {

        public TimeoutException(string message) : base(message)
        {

        }

    }

}