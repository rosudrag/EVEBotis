using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EVE.ISXEVE;

namespace GateCAmp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        public List<string> EntityNameBinding 
        {
            get
            {
                return GuiInterface.EntityNameList.ToList(); 
                
            }
        } 

        public MainWindow()
        {
            InitializeComponent();

            MainGateCamp.Init();
            MainGateCamp.Run();
        }




        private void mainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void OnClickRefreshEntity(object sender, RoutedEventArgs e)
        {
            if (MainGateCamp.CurrentState == MainGateCamp.BotState.Camping)
            {
                MainGateCamp.CurrentState = MainGateCamp.BotState.Idle;
            }
            else MainGateCamp.CurrentState = MainGateCamp.BotState.Camping;
        }
    }
}
