#region

using System;
using System.Linq;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Алгоритм проверки отношения переходов модели
    ///   на тотальность.
    /// </summary>
    /// <seealso cref = "ModelClosure" />
    /// <seealso cref = "IModel" />
    public static class ModelTotalityChecker {
        /// <summary>
        ///   Выполняет проверку отношения переходов модели <paramref name = "model" />
        ///   на тотальность.
        /// </summary>
        /// <remarks>
        ///   Отношение переходов модели называется <i>тотальным</i>, если
        ///   из каждого состояния существует переход в некоторое состояние.
        /// </remarks>
        /// <param name = "model">Модель.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "model" /> является <c>null</c>.</exception>
        /// <returns><c>true</c>, если отношение переходов тотально.</returns>
        public static bool Check (IModel model) {
            if (model == null)
                throw new ArgumentNullException ("model");

            return model.States.All (s => model.Transitions (s).Count != 0);
        }
    }
}
