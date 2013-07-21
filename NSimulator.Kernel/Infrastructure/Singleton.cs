namespace NSimulator.Kernel {
    internal static class Singleton <T>
        where T : new ( ) {
        static Singleton () {
            Instance = new T ();
        }

        public static T Instance { get; private set; }
        }
}
