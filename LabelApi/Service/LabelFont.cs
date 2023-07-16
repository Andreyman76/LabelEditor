using System.Drawing;

namespace LabelApi;

#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Класс для сериализации шрифта в XML
/// </summary>
public class LabelFont
{
    /// <summary>
    /// Семейство шрифта
    /// </summary>
    public string Family { get; set; } = "Calibri";

    /// <summary>
    /// Размер шрифта
    /// </summary>
    public float Size { get; set; } = 11;

    /// <summary>
    /// Стиль шрифта
    /// </summary>
    public FontStyle Style { get; set; } = FontStyle.Regular;
}

#pragma warning restore CA1416 // Проверка совместимости платформы