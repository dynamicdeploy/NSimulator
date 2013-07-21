#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Интерфейс модели.
    /// </summary>
    [ContractClass (typeof (Contract_IModel))]
    public interface IModel {
        /// <summary>
        ///   Получает список атомарных высказываний, выполняющихся
        ///   в состоянии с номером <paramref name = "index" />.
        /// </summary>
        /// <param name = "index">Номер состояния.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Состояния с номером <paramref name = "index" /> не существует.</exception>
        /// <returns>Список атомарных высказываний.</returns>
        List <string> this [int index] { get; }

        /// <summary>
        ///   Получает список состояний.
        /// </summary>
        /// <value>
        ///   Список номеров состояний.
        /// </value>
        List <int> States { get; }

        /// <summary>
        ///   Получает количество состояний.
        /// </summary>
        /// <value>
        ///   Количество состояний.
        /// </value>
        int StatesCount { get; }

        /// <summary>
        ///   Получает состояния, в которые можно осуществить
        ///   переход из состояния с номером <paramref name = "from" />.
        /// </summary>
        /// <param name = "from">Номер состояния.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Состояния с номером <paramref name = "from" /> не существует.</exception>
        /// <returns>Список смежных состояний.</returns>
        List <int> Transitions (int from);

        /// <summary>
        ///   Проверяет, существует ли переход между состоянием с номером
        ///   <paramref name = "from" /> в состояние с номером <paramref name = "to" />.
        /// </summary>
        /// <param name = "from">Номер состояния-источника.</param>
        /// <param name = "to">Номер состояния-назначения.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Состояния с номером <paramref name = "from" /> не существует.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">Состояния с номером <paramref name = "to" /> не существует.</exception>
        /// <returns><c>true</c>, если переход существует.</returns>
        bool HasTransition (int from, int to);
    }
}
