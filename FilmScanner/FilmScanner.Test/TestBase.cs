
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace FilmScanner.Test
{

    public class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            foreach (var item in new DirectoryInfo(".").GetFiles("*.bmp"))
            {
                //item.Delete();
            }
        }

        [TearDown]
        public void TearDown()
        {

        }

    }

}
