#region

using System;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Отмечает типы физических интерфейсов, пакеты с которых обрабатывает модуль.
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class InterfaceTypeAttribute : Attribute {
        /// <summary>
        ///   Инициализирует атрибут.
        /// </summary>
        /// <param name = "types">Типы физических интерфейсов.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "types" /> является <c>null</c>.</exception>
        /// <exception cref = "InvalidInterfaceClassException"><paramref name = "types" /> содержит тип, не являющийся реализацией <seealso cref = "IInterfaceView" />.</exception>
        public InterfaceTypeAttribute (params Type [] types) {
            if (types == null)
                throw new ArgumentNullException ("types");

            foreach (var type in
                types.Where (
                    type => ! (type.IsInstanceOfType (typeof (IInterfaceView)) && type.IsClass && type.IsPublic)))
                throw new InvalidInterfaceClassException (type.FullName);

            this.Types = types;
        }

        /// <summary>
        ///   Возвращает список типов физических интерфейсов.
        /// </summary>
        /// <value>
        ///   Список типов физических интерфейсов.
        /// </value>
        public Type [] Types { get; private set; }
    }
}
