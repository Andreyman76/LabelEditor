﻿namespace LabelApi;

/// <summary>
/// Встроенные переменные этикетки
/// </summary>
public class BuiltInVariables
{
    /// <summary>
    /// Текущие дата и время
    /// </summary>
    public DateTime CurrentDateTime { get => DateTime.Now; }

    /// <summary>
    /// Разделитель ASCII 29 (используется для сериализации XML)
    /// </summary>
    public string GS { get => "\u001d"; }

    /// <summary>
    /// Перенос строки
    /// </summary>
    public string Endl { get => "\n"; }
}