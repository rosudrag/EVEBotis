using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace BatchCommanding.Model
{
   public  class BaseCommand :  BackgroundWorker, ICommand
   {
       public bool canexecute = true;
       public BaseCommand()
       {
           this.WorkerSupportsCancellation = true;
           this.WorkerReportsProgress = true;
           this.DoWork += new DoWorkEventHandler(BWDoWork);
           this.ProgressChanged += new ProgressChangedEventHandler(BWProgressChanged);
           this.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWRunWorkerCompleted);
       }
      
      public virtual void BWRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {
            
       }
      public virtual  void BWProgressChanged(object sender, ProgressChangedEventArgs e)
       {
           
       }
      public virtual  void BWDoWork(object sender, DoWorkEventArgs e)
       {
           
       }


      public virtual bool CanExecute(object parameter)
      {
          return true;
      }

      public event EventHandler CanExecuteChanged;

      public virtual void Execute(object parameter)
      {
           
      }
   }
}
