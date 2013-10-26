using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBK.FileType
{
    /// <summary>
    /// 错误的文件格式异常
    /// </summary>
    public class BadFileFormatException: BBKException
    {
        public BadFileFormatException(string message = "错误的文件格式", Exception innerException = null)
            : base(message, innerException)
        {}
    }
}
