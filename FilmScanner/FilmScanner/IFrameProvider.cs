using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmScanner
{
    public interface IFrameProvider
    {
        Image CaptureFrame();

    }
}
