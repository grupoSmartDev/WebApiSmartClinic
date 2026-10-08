namespace WebApiSmartClinic.Services.CRM;

internal static class CrmHelper
{
    // Datas vindas do front: Utc fica como está, Local é convertida, Unspecified é tratada como UTC
    public static DateTime ParaUtc(DateTime data) => data.Kind switch
    {
        DateTimeKind.Utc => data,
        DateTimeKind.Local => data.ToUniversalTime(),
        _ => DateTime.SpecifyKind(data, DateTimeKind.Utc)
    };

    public static DateTime? ParaUtc(DateTime? data) => data.HasValue ? ParaUtc(data.Value) : null;
}
