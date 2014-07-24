using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BatchCommanding.Model 
{
    public class AsynchronusCommand : BaseCommand
    {
        /// <summary>
        /// This is the delegate definition to post progress back to caller via the event named
        /// EHProgressChanged
        /// </summary>
        /// <param name="progress">Hold the progress integer value (from  0-100)</param>
        public delegate void DlgProgressChanged(int progress);
        /// <summary>
        /// This is the delegate deifnition to post a Observable collection back to the caller via the
        /// evnet EHDataReady
        /// </summary>
        /// <param name="data">The signature needed for the callback method</param>
        public delegate void DlgDataReady(ObservableCollection<string> data);
        //Static event allows for wiring up to event before class is instanciated
        /// <summary>
        /// This is the Event others can subscribe to, to get the post back  of Progress Changed
        /// </summary>
        public static event DlgProgressChanged EHProgressChanged;
        //Static event to wire up to prior to class instanciation
        /// <summary>
        /// This is the event of which others can subscribe to receive the data when it's ready
        /// </summary>
        public static event DlgDataReady EHDataReady; 
 
        /// <summary>
        /// The Entry point for a WPF Command implementation
        /// </summary>
        /// <param name="parameter">Any parameter passed in by the Commanding Architecture</param>
        public override void Execute(object parameter)
        {
            if (parameter.ToString() == "CancelJob") {   
                //This is a flag that the "other thread" sees and supports
                this.CancelAsync(); 
                return;
            }
           canexecute = false;          
          this.RunWorkerAsync(GetBackGroundWorkerHelper());
        }
        /// <summary>
        /// A helper class that allow one to encapsulate everything needed to pass into and out of the 
        /// Worker thread
        /// </summary>
        /// <returns>BackGroundWorkerHelper</returns>
        public BGWH GetBackGroundWorkerHelper() {
            //The BGWH class can be anything one wants it to be..
            //all of the work performed in background thread can be stored here
            //additionally any cross thread communication can be passed into that background thread too.
            BGWH bgwh = new BGWH
            {
                obj1 = 1,
                obj2 = 2,
                SleepIteration = 200,
            };
            return bgwh;
        }
        /// <summary>
        /// This is the implementation of the Asynchronous logic
        /// </summary>
        /// <param name="sender">Caller of this method</param>
        /// <param name="e">The DoWorkEvent Arguments</param>
        public override void BWDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Ahh!  Now we are running on a separate asynchronous thread
            BGWH bgwh = e.Argument as BGWH;         
            //always good to put in validation
            if (bgwh != null)
            {
                //we are able to simulate a long outstand work item here
                Simulate.Work(ref bgwh, this);
            }
            //All the work is done make sure to store result
            e.Result = bgwh;
        }
        /// <summary>
        /// BackGround Work Progress Changed, runs on this object's main thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void BWProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //allow for a Synchronous update to the WPF Gui Layer
            int progress = e.ProgressPercentage;
            //notify others that the progress has increased.
            EHProgressChanged(progress);
            EHDataReady((ObservableCollection<string>)e.UserState);
        }
        /// <summary>
        /// Handles the completion of the background worker thread.  This method is running on the current thread and
        /// can be used to update the execute method with information as needed
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The Run WorkerCompleted Event Args</param>
        public override void BWRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //ideally this method would fire an event to the view model to update the data
            BGWH bgwh = e.Result as BGWH;
            var data = bgwh.Data;
            //notify others that the data has changed.
            EHDataReady(data);
            EHProgressChanged(0);
            canexecute = true;
        }

    }
}
