using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BatchCommanding.Model
{
    /// <summary>
    /// Background worker Class allows you to pass in as many objects as desired,
    /// Just change this class to suit your needs.  
    /// </summary>
    public class BGWH  
    {
        /// <summary>
        /// This demo chose a Object for the first "thing" to be passed in an out.
        /// </summary>
        public object obj1;
        /// <summary>
        /// This is the second thing and shows another "object"
        /// </summary>
        public object obj2;
        /// <summary>
        /// An arbitrary integer value named SleepIteration
        /// </summary>
        public int SleepIteration;
        /// <summary>
        /// An observable collection
        /// </summary>
        public ObservableCollection<string> Data = new ObservableCollection<string>();           
    }
}


