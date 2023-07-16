using System.ComponentModel;
using System.Drawing;

namespace LabelApi;

public abstract class LabelElementBase : ICloneable
{
    /// <summary>
    /// Имя элемента этикетки
    /// </summary>
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Основные свойства")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Координаты верхнего левого угла элемента этикетки
    /// </summary>
    [Browsable(true)]
    [Description("Положение объекта в мм")]
    [DisplayName("Расположение"), Category("Основные свойства")]
    public PrintingPosition Position { get; set; }

    /// <summary>
    /// Метод отрисовки элемента этикетки
    /// </summary>
    /// <param name="g"></param>
    public abstract void Draw(Graphics g);

    /// <summary>
    /// Заменить значение в текстовых данных элемента этикетки
    /// </summary>
    /// <param name="from">Что заменить</param>
    /// <param name="to">На что заменить</param>
    public abstract void Replace(string from, string to);

    /// <summary>
    /// Полное копирование элемента этикетки
    /// </summary>
    /// <returns>Полная копия элемента этикетки</returns>
    public abstract object Clone();
}