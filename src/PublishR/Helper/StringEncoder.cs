using System;
using System.Text;

namespace PublishR.Helper
{
    public static class StringEncoder
    {
        public static string ConvertBase64String(string strOriginal)
        {
            byte[] byt = Encoding.ASCII.GetBytes(strOriginal);
            return Convert.ToBase64String(byt);
        }
    }
}