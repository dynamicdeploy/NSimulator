#region

using System;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   �������� ������� ����� ����������� ����������.
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class InterfacePrefixAttribute : Attribute {
        /// <summary>
        ///   ������������� ��������, ����������� ������� ����� ����������.
        /// </summary>
        /// <param name = "prefix">������� ����� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "prefix" /> �������� <c>null</c>.</exception>
        public InterfacePrefixAttribute (string prefix) {
            if (prefix == null)
                throw new ArgumentNullException ("prefix");
            this.Prefix = prefix;
        }

        /// <summary>
        ///   �������� ������� ����� ����������.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        ///   �������� ������� ����� ����������, ��������� � ������� ������� ����������.
        /// </summary>
        /// <param name = "iface">������ ����������, ������� ����� �������� ����� ��������.</param>
        /// <returns>������� ����� ���������� ��� <c>null</c>, ���� ������� �� ������.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
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
