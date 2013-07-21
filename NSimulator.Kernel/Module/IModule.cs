namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс модуля прошивки.
    /// </summary>
    public interface IModule : INamedElement, IXMLSerializable {
        /// <summary>
        /// </summary>
        /// <param name = "packet"></param>
        /// <param name = "iface"></param>
        /// <returns></returns>
        bool CanProcessPacket (IPacket packet, IInterfaceView iface);

        /// <summary>
        /// </summary>
        /// <param name = "packet"></param>
        /// <param name = "iface"></param>
        void ProcessPacket (IPacket packet, IInterfaceView iface);
    }
}
