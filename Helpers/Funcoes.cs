using System.Text.RegularExpressions;

namespace WebApiSmartClinic.Helpers
{
    public class Funcoes
    {
        public static string? RemoverCaracteres(string? caracteres)
        {
            if (!string.IsNullOrEmpty(caracteres))
            {
                return Regex.Replace(caracteres, @"[^a-zA-Z0-9\s]", "");
            }
            else
            {
                return "";
            }
        }
    }
}
