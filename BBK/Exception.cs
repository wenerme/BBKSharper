using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBK
{
    /// <summary>
    /// 基础异常
    /// </summary>
    public class BBKException: Exception
    {
        public BBKException(string message = null,Exception innerException = null)
            : base(message, innerException)
        {}
    }
}
