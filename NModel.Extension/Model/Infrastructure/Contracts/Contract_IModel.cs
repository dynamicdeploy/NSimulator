#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NModel.Extension {
    [ContractClassFor (typeof (IModel))]
    internal abstract class Contract_IModel : IModel {
        #region IModel Members

        public List <string> this [int index] {
            get {
                Contract.Requires <ArgumentOutOfRangeException> (index >= 0 && index < this.StatesCount);
                Contract.Ensures (Contract.Result <List <string>> () != null);
                return default (List <string>);
            }
        }

        public List <int> States {
            get {
                Contract.Ensures (Contract.Result <List <int>> () != null);
                Contract.Ensures (Contract.Result <List <int>> ().Count == this.StatesCount);
                return default (List <int>);
            }
        }

        public int StatesCount {
            get {
                Contract.Ensures (Contract.Result <int> () >= 0);
                return default (int);
            }
        }

        [Pure]
        public List <int> Transitions (int from) {
            Contract.Requires <ArgumentOutOfRangeException> (from >= 0 && from < this.StatesCount);
            Contract.Ensures (Contract.Result <List <int>> () != null);
            return default (List <int>);
        }

        [Pure]
        public bool HasTransition (int from, int to) {
            Contract.Requires <ArgumentOutOfRangeException> (from >= 0 && from < this.StatesCount);
            Contract.Requires <ArgumentOutOfRangeException> (to >= 0 && to < this.StatesCount);
            Contract.Ensures (! (Contract.Result <bool> () ^ Contract.Exists (this.Transitions (from), x => x == to)));
            return default (bool);
        }

        #endregion
    }
}
