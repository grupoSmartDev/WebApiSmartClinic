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

        // usar tipo "F" para a data final, vai retornar por exemplo "dd/MM/yyyy 23:59:59" e tipo "I" para data inicial, vai retornar "dd/MM/yyyy 00:00:00" 
        public static DateTime? FormataDataTimeFiltros(DateTime? dataRecebida, string tipo)
        {
            switch (tipo)
            {
                case "F":
                    return DateTime.SpecifyKind(dataRecebida.Value, DateTimeKind.Utc).AddDays(1).AddSeconds(-1);
                case "I":
                    return DateTime.SpecifyKind(dataRecebida.Value, DateTimeKind.Utc);
                default:
                    return DateTime.SpecifyKind(dataRecebida.Value, DateTimeKind.Utc);
            }
        }
    }
}