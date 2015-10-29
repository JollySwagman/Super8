using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmScanner
{
    public interface IDigitalIO
    {
        bool IsHigh();
        bool IsLow();

        StateType State { get; set; }


    }
}
