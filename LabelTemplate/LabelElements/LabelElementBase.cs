using System.ComponentModel;
using System.Drawing;

namespace LabelTemplate;

public abstract class LabelElementBase : ICloneable
{
    /// <summary>
    /// Получить границы элемента этикетки
    /// </summary>
    /// <param name="g"></param>
    /// <returns>Границы элемента этикетки</returns>
    public abstract RectangleF GetComputedBounds(Graphics g);

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
    /// Задать значение для переменной по ее имени
    /// </summary>
    /// <param name="from">Имя переменной</param>
    /// <param name="to">Значение переменной</param>
    public abstract void Replace(string from, string to);

    public abstract object Clone();
}