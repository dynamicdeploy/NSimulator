namespace NSimulator.TestMocks {
    internal static class MathHelper {
        public static ulong CalculateTime (ulong length, ulong speed) {
            if (speed == 0)
                return ulong.MaxValue;

            return length / speed + (ulong) (length % speed == 0 ? 0 : 1);
        }

        public static ulong CalculateTime (int length, ulong speed) {
            return CalculateTime ((ulong) length, speed);
        }
    }
}
