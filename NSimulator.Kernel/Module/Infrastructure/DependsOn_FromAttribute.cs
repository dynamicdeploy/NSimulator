#region

using System;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Отмечает типы модулей, зависимых от данного при обработке данных
    ///   в направлении "от интерфейса".
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class DependsOn_FromAttribute : Attribute {
        /// <summary>
        ///   Инициализирует атрибут.
        /// </summary>
        /// <param name = "types">Типы модулей.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "types" /> является <c>null</c>.</exception>
        /// <exception cref = "InvalidModuleClassException"><paramref name = "types" /> содержит тип, не являющийся реализацией <seealso cref = "IModule" />.</exception>
        public DependsOn_FromAttribute (params Type [] types) {
            if (types == null)
                throw new ArgumentNullException ("types");

            foreach (var type in
                types.Where (type => ! (type.IsInstanceOfType (typeof (IModule)) && type.IsClass && type.IsPublic)))
                throw new InvalidModuleClassException (type.FullName);

            this.Types = types;
        }

        /// <summary>
        ///   Возвращает список типов модулей.
        /// </summary>
        /// <value>
        ///   Список типов модулей.
        /// </value>
        public Type [] Types { get; private set; }
    }
}
