#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ���������� ����������� ����������.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     ���������� �������������� ��� ������� ������������� �����������
    ///     ����������, ������� � �� �����������.
    ///   </para>
    ///   <para>
    ///     <example>
    ///       ������ �����, �������������� ������ ����������, ����� ���:
    ///       <code>
    ///         /// &lt;summary&gt;
    ///         ///   ������ ����������� �� ��������������.
    ///         ///   ��� ������� ������ �������������� &lt;see cref="FeatureNotSupportedException"/&gt;.
    ///         /// &lt;/summary&gt;
    ///         /// &lt;exception cref="FeatureNotSupportedException"&gt;&lt;/exception&gt;
    ///         void SomeMethod () {
    ///         throw new FeatureNotSupportedException ();
    ///         }
    ///       </code>
    ///     </example>
    ///     � ������������ ����� <b>����</b> ��������� � �������� ����������, ��� ��� �������� � �������.
    ///   </para>
    /// </remarks>
    [Serializable]
    public sealed class FeatureNotSupportedException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "device">�������� ����������.</param>
        /// <param name = "feature">�������� �����������.</param>
        public FeatureNotSupportedException (string device, string feature)
            : base (string.Format (Strings.FeatureNotSupportedException, device, feature)) {
            this.Device = device;
            this.Feature = feature;
        }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, � ������� ����������� <see cref = "Feature" /> �� ��������������.
        /// </value>
        public string Device { get; private set; }

        /// <summary>
        ///   �������� �������� �����������.
        /// </summary>
        /// <value>
        ///   �����������, ������� �� �������������� � ���������� <see cref = "Device" />.
        /// </value>
        public string Feature { get; private set; }
    }
}
