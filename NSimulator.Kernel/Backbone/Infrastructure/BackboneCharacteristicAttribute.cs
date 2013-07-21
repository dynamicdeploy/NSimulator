#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Отмечает, что свойство является характеристикой среды передачи данных.
    /// </summary>
    [AttributeUsage (AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class BackboneCharacteristicAttribute : Attribute {}
}
