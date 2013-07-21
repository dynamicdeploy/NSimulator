#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при попытке доступа к несуществующей в топологии
    ///   сети сущности.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке удаления некой сущности по описателю,
    ///   о котором не существует информации в топологии.
    /// </remarks>
    [Serializable]
    public sealed class EntityHandlerNotFoundException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "entity"></param>
        public EntityHandlerNotFoundException (NetworkEntity entity)
            : base (string.Format (Strings.EntityHandlerNotFoundException, entity)) {
            this.Entity = entity;
        }

        /// <summary>
        ///   Получает описатель сущности.
        /// </summary>
        /// <value>
        ///   Описатель сущности, которой нет в топологии.
        /// </value>
        public NetworkEntity Entity { get; private set; }
    }
}
