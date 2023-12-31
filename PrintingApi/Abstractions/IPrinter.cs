﻿using LabelApi;

namespace PrintingApi;

/// <summary>
/// Интерфейс принтера
/// </summary>
public interface IPrinter : IDisposable
{
    /// <summary>
    /// Разрешение печати принтера
    /// </summary>
    Dpi Dpi { get; set; }

    /// <summary>
    /// Метод печати этикетки
    /// </summary>
    /// <param name="label">Этикетка</param>
    /// <returns></returns>
    bool Print(PrinterLabel label); 
}