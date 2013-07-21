#region

using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    internal sealed class UniqueIdGenerator {
        private readonly SortedSet <ulong> ids;
        private ulong? id;

        public UniqueIdGenerator () {
            this.ids = new SortedSet <ulong> ();
            this.id = null;
        }

        public UniqueIdGenerator (IEnumerable <ulong> ids) {
            this.ids = new SortedSet <ulong> (ids);
            this.id = null;
        }

        public ulong GetNext () {
            if (this.id == null)
                this.id = ulong.MinValue;

            while (this.id <= this.ids.Max && this.ids.Contains ((ulong) this.id))
                ++ this.id;

            this.ids.Add ((ulong) this.id);

            return (ulong) this.id;
        }
    }
}
