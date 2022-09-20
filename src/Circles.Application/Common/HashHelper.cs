using System.Security.Cryptography;
using System.Text;

namespace Circles.Application.Common;

internal static class HashHelper
{
    internal static string GetHash(string str)
    {
        var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(str);
        var hash = md5.ComputeHash(inputBytes);
        var sb = new StringBuilder();
        foreach (var t in hash)
        {       
            sb.Append(t.ToString("X2"));
        }
        return sb.ToString();
    }
}