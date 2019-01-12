/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/3
 * Time: 18:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Timers;

namespace Ticker
{
	/// <summary>
	/// Description of DelayTimer.
	/// </summary>
	public class DelayTimer
	{
		public delegate void timeUpHandler();
		public delegate void timeTickHandler(int t);
		int interval=1000;
		int time=0;
		public timeUpHandler onTimeUp;
		public timeTickHandler onTimeTick;
		public DelayTimer(int ti)
		{
			time=ti;
			Timer t=new Timer();
			t.Interval=interval;
			t.AutoReset=true;
			t.Enabled=true;
			t.Elapsed+=new ElapsedEventHandler(timeUp);
			t.Start();
		}
		
		private void timeUp(object sender, ElapsedEventArgs e)
		{
			Timer t=(Timer)sender;
			if(time<=0)
			{
				t.Dispose();
				onTimeUp();
				//t.Stop();
				
			}
			else
			{
				time--;	
				onTimeTick(time);
					
			}
		
		}
	}
}
