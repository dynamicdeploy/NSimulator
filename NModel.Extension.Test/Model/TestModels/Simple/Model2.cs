#region

using System.Collections.Generic;
using NModel.Attributes;

#endregion

namespace NModel.Extension.Test.SimpleModel2 {
    internal static class Model {
        private static int x = 1;

        [Action]
        public static void Turn ([Domain ("Numbers")] int i) {
            x = i;
        }

        public static bool TurnEnabled ([Domain ("Numbers")] int i) {
            return i % x == 0;
        }

        public static Set <int> Numbers () {
            var result = new List <int> ();
            for (var i = 1; i <= 100; ++ i)
                result.Add (i);

            return new Set <int> (result);
        }
    }
}
