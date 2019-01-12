/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/3
 * Time: 15:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Timers;

namespace Ticker
{
	/// <summary>
	/// Description of TickerTimer.
	/// </summary>
	public class TickerTimer
	{	
		public delegate void timeChangedHandler();    //跨天或者时间变更事件
		public delegate void timeUpHandler(TimeSpan dtime);    //到达指定时间事件
		public timeChangedHandler onTimeChanged;
		public timeUpHandler onTimeUp;
		private DateTime dtn = DateTime.Now;
		private DateTime dtp;    //记录上一次的时间，用于比对来判断是否跨天或时间变更
		public TimeSpan defTime;  //指定时间
		public Timer t;
		public bool enable=true;   //是否可用，置false依然可以触发onTimeChanged事件
		int interval=1000;      //事件间隔
		public TickerTimer(TimeSpan ddt)
		{
			defTime=ddt;
			initTimer();
		}
		public TickerTimer()
		{
			defTime=new TimeSpan(0,0,0);
			initTimer();		
		}
		
		public void initTimer()    //初始化timer
		{		
			dtp=DateTime.Now;
			enable=true;
			t=new Timer();
			t.Interval=interval;
			t.AutoReset=true;
			t.Enabled=true;
			t.Elapsed+=new ElapsedEventHandler(timerTick);
			t.Start();
		}
		
		private void timerTick(object sender, System.Timers.ElapsedEventArgs e) //事件处理
        {
			 Timer tx=(Timer)sender;
             dtn=DateTime.Now;
             TimeSpan ts=dtn-dtp;
             if(ts.TotalSeconds<5&&ts.TotalSeconds>=0&&dtn.Day==dtp.Day)  //判断是否跨天或时间变更大于五秒
             {
             	dtp=dtn;
             	ts=dtn.TimeOfDay-defTime;
             	if(enable&&ts.TotalSeconds>=0&&ts.TotalSeconds<2)  //到达指定时间则enable置false，触发onTimeUp事件
             	{   
             		enable=false;
             		onTimeUp(defTime);       		
             	}          	
             }
             else   //跨天或时间变更则销毁timer并触发onTimeChanged事件
             {
             	tx.Dispose();
             	onTimeChanged();
             	dtp=DateTime.Now;
             }
        }
		public void Dispose()
		{
			t.Dispose();
		}
		 private bool timeComp(DateTime dtn,DateTime dt)
		 {
		 	return true;
		 }
	}
}
