using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace BatchCommanding.Model
{
    public static class Simulate
    {
        public static void Work( ref Model.BGWH bgwh, BaseCommand TheAsynchCommand) {
            //shows how the BGWH can have many different control mechanisms
            //note that we pass it in as a reference which means all updates here are 
            //automatically reflected to any object that has this reference.
            int iteration = bgwh.SleepIteration;
            //This is iterative value to determine total progress. 
            double perIteration = .005;
            //simulate reading 200 records with a small delay in each..
            Random r = new Random();
            for (int i = 0; i < iteration + 1; i++)
            {
                System.Threading.Thread.Sleep(r.Next(250));
                //Update the data element in the BackGroundWorkerHelper
                bgwh.Data.Add("This would have been the data from the SQL Query etc. " + i);
                //currentstate is used to report progress
                var currentState = new ObservableCollection<string>();
                currentState.Add("The Server is busy... Iteration " + i);
                double fraction = (perIteration) * i;
                double percent = fraction * 100;
                //here a call is made to report the progress to the other thread
                TheAsynchCommand.ReportProgress((int)percent, currentState);
                //did the user want to cancel this job? If so get out.
                if (TheAsynchCommand.CancellationPending == true)
                {
                    //get out of dodge
                    break;
                }
            }
        }
    }
}
