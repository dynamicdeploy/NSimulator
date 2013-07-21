#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Array <T> {
        #region Nested type: ArrayEnumerator

        internal sealed class ArrayEnumerator : IEnumerator <T> {
            private readonly IArrayView <T> array;
            private int position;

            public ArrayEnumerator (IArrayView <T> array) {
                this.array = array;
                this.Reset ();
            }

            #region IEnumerator<T> Members

            public void Dispose () {}

            public bool MoveNext () {
                ++this.position;
                return this.position < this.array.Length;
            }

            public void Reset () {
                this.position = -1;
            }

            public T Current {
                get { return this.array [this.position]; }
            }

            object IEnumerator.Current {
                get { return this.Current; }
            }

            #endregion
        }

        #endregion
    }
}
