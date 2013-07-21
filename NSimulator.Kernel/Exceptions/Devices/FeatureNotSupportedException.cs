#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при отсутствии возможности устройства.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Исключение пробрасывается при попытке использования возможности
    ///     устройства, которая в нём отсутствует.
    ///   </para>
    ///   <para>
    ///     <example>
    ///       Обычно метод, пробрасывающий данное исключение, имеет вид:
    ///       <code>
    ///         /// &lt;summary&gt;
    ///         ///   Данная возможность не поддерживается.
    ///         ///   При попытке вызова пробрасывается &lt;see cref="FeatureNotSupportedException"/&gt;.
    ///         /// &lt;/summary&gt;
    ///         /// &lt;exception cref="FeatureNotSupportedException"&gt;&lt;/exception&gt;
    ///         void SomeMethod () {
    ///         throw new FeatureNotSupportedException ();
    ///         }
    ///       </code>
    ///     </example>
    ///     В документации нужно <b>явно</b> указывать о пробросе исключения, как это показано в примере.
    ///   </para>
    /// </remarks>
    [Serializable]
    public sealed class FeatureNotSupportedException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "device">Название устройства.</param>
        /// <param name = "feature">Название возможности.</param>
        public FeatureNotSupportedException (string device, string feature)
            : base (string.Format (Strings.FeatureNotSupportedException, device, feature)) {
            this.Device = device;
            this.Feature = feature;
        }

        /// <summary>
        ///   Получает название устройства.
        /// </summary>
        /// <value>
        ///   Название устройства, в которой возможность <see cref = "Feature" /> не поддерживается.
        /// </value>
        public string Device { get; private set; }

        /// <summary>
        ///   Получает название возможности.
        /// </summary>
        /// <value>
        ///   Возможность, которая не поддерживается в устройстве <see cref = "Device" />.
        /// </value>
        public string Feature { get; private set; }
    }
}
