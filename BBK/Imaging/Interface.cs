using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace BBK.Imaging
{

    public interface IImageContainer
    {
        IList<Image> ImageList { get; }
    }
}
