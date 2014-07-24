using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BatchCommanding.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            //the model will send a notification to us when the data is ready
            Model.AsynchronusCommand.EHDataReady += new Model.AsynchronusCommand.DlgDataReady(AsynchronusCommandEhDataReady);
           //this is the event handler to update the current state of the background thread
            Model.AsynchronusCommand.EHProgressChanged += new Model.AsynchronusCommand.DlgProgressChanged(AsynchronusCommandEhProgressChanged);
            //set up a default value prior to running the model's code
            var defaultValues = new ObservableCollection<string>();
            defaultValues.Add("This is the default value for the list of items");
            Data = defaultValues;
            Progress = 0;
        }
        /// <summary>
        /// A flag to stop command execution it is a parameter of a commend that flags the Asynchronous command to stop.
        /// We place it here so that we can Bind to this via WPF Command and Command parameters
        /// </summary>
        public string InterruptCommandExecution
        {
            get { return "CancelJob"; }
        }
        //The event handler for when there's new progress to report
        void AsynchronusCommandEhProgressChanged(int progress)
        {
            Progress = progress;
        }
        //The event handler for when data is ready to show to the end user
        void AsynchronusCommandEhDataReady(ObservableCollection<string> data)
        {
            Data = data;
        }
        private ObservableCollection<string> _Data;
        /// <summary>
        /// An ObservableCollection of type string for the GUI Binding.
        /// </summary>
        public ObservableCollection<string> Data
        {
            get { return _Data; }
            set { _Data = value;
                PropChanged("Data");
            }
        }
        private int _Progress;
        /// <summary>
        /// The integer value that the progress bar value is bound.
        /// </summary>
        public int Progress
        {
            get { return _Progress; }
            set { _Progress = value;
                PropChanged("Progress");
            }
        }
    }
}
