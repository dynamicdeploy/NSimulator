#region

using System;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Отмечает префикс имени физического интерфейса.
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class InterfacePrefixAttribute : Attribute {
        /// <summary>
        ///   Инициализация атрибута, отмечающего префикс имени интерфейса.
        /// </summary>
        /// <param name = "prefix">Префикс имени интерфейса.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "prefix" /> является <c>null</c>.</exception>
        public InterfacePrefixAttribute (string prefix) {
            if (prefix == null)
                throw new ArgumentNullException ("prefix");
            this.Prefix = prefix;
        }

        /// <summary>
        ///   Получает префикс имени интерфейса.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        ///   Получает префикс имени интерфейса, связанный с классом объекта интерфейса.
        /// </summary>
        /// <param name = "iface">Объект интерфейса, префикс имени которого нужно получить.</param>
        /// <returns>Префикс имени интерфейса или <c>null</c>, если атрибут не найден.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        public static string GetPrefix (IInterfaceView iface) {
            if (iface == null)
                throw new ArgumentNullException ("iface");
            return
                (from InterfacePrefixAttribute attr in
                     iface.GetType ().GetCustomAttributes (typeof (InterfacePrefixAttribute), false)
                 select attr.Prefix).FirstOrDefault ();
        }
    }
}
