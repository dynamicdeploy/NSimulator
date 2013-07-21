#region

using NModel.Execution;

#endregion

namespace NModel.Extension.Test {
    public static class Model1 {
        public static ModelProgram Make () {
            return new LibraryModelProgram (typeof (SimpleModel1.Model).Assembly, typeof (SimpleModel1.Model).Namespace);
        }
    }
}
