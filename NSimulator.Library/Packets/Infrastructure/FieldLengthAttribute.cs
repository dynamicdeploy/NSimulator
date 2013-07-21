#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Отмечает длину поля.
    /// </summary>
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class FieldLengthAttribute : Attribute {
        private static readonly Dictionary <Enum, byte> cache;

        static FieldLengthAttribute () {
            cache = new Dictionary <Enum, byte> ();
        }

        /// <summary>
        ///   Инициализация длины поля.
        /// </summary>
        /// <param name = "length">Длина поля.</param>
        public FieldLengthAttribute (byte length) {
            this.Length = length;
        }

        /// <summary>
        ///   Получает длину поля.
        /// </summary>
        /// <value>
        ///   Длина поля.
        /// </value>
        public byte Length { get; private set; }

        /// <summary>
        ///   Получает значение атрибута у элемента перечисления.
        /// </summary>
        /// <param name = "item">Элемент перечисления.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "item" /> является <c>null</c>.</exception>
        /// <returns>Значение атрибута</returns>
        public static byte GetLength (Enum item) {
            if (item == null)
                throw new ArgumentNullException ("item");

            if (cache.ContainsKey (item))
                return cache [item];

            var result =
                (from FieldLengthAttribute _ in
                     item.GetType ().GetCustomAttributes (typeof (FieldLengthAttribute), false)
                 select _.Length).FirstOrDefault ();

            cache.Add (item, result);
            return result;
        }
    }
}
