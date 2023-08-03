using System.ComponentModel;

namespace LabelApi;

/// <summary>
/// Тип поворота элемента этикетки
/// </summary>
public enum LabelElementRotationType
{
    /// <summary>
    /// На 0 градусов
    /// </summary>
    Rotate0,

    /// <summary>
    /// На 90 градусов
    /// </summary>
    Rotate90,

    /// <summary>
    /// На 180 градусов
    /// </summary>
    Rotate180,

    /// <summary>
    /// На 270 градусов
    /// </summary>
    Rotate270
}