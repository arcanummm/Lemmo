namespace Lemmo.WebApi.Entitys.Enums
{
    public enum ExceptionType
    {
        Cancelled,      // Урок отменен
        Rescheduled,    // Урок перенесен на другое время
        PriceChanged,   // Изменена цена (сохраняем как отдельный урок)
        Other           // Другое изменение
    }
}
