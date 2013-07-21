﻿namespace NSimulator.Kernel {
    internal sealed class Triple <T1, T2, T3> {
        public Triple (T1 first, T2 second, T3 third) {
            this.First = first;
            this.Second = second;
            this.Third = third;
        }

        public T1 First { get; set; }
        public T2 Second { get; set; }
        public T3 Third { get; set; }
    }
}
