#region

using NModel.Attributes;

#endregion

namespace NModel.Extension.Test.SimpleModel1 {
    internal static class Model {
        private static int x;

        [Action]
        public static void Turn ([Domain ("Numbers")] int i) {
            x = i;
        }

        public static bool TurnEnabled ([Domain ("Numbers")] int i) {
            return i == x + 1;
        }

        public static Set <int> Numbers () {
            return new Set <int> (0, 1, 2, 3, 4);
        }
    }
}
