#region

using NModel.Execution;

#endregion

namespace NModel.Extension.Test {
    public static class Model2 {
        public static ModelProgram Make () {
            return new LibraryModelProgram (typeof (SimpleModel2.Model).Assembly, typeof (SimpleModel2.Model).Namespace);
        }
    }
}
