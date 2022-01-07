using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
using BlApi.BO;
using System.Threading;
using static BlApi.BO.BL;
using System.Linq;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// contains item updating functions
        /// </summary>

        class Simulator
        {
            BlApi.BO.BL bl;
            const double DroneSpeed = 0.2;
            //TimerCallback TimerCallback = new TimerCallback(drone);
            //Timer timer = new Timer(TimerCallback, null, 1000, 50000);
        
            private void Timer_Click(object sender, EventArgs e)
            {
            }
            Simulator(IBl _bl, int droneId, Action ui_Update, Func<bool> stopCheck)
            {
                bl = (BlApi.BO.BL)_bl;
                //Timer.Tick += new EventHandler(Timer_Click);
                //Timer.Interval = new TimeSpan(0, 0, 1);
                //Timer.Start();


            }




        }
    }
}
