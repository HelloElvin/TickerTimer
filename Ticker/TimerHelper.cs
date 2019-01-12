/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/3
 * Time: 20:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Timers;
using System.Collections.Generic;

namespace Ticker
{
	/// <summary>
	/// Description of TimerHelper.
	/// </summary>
	public class TimerHelper
	{
		public delegate void timeUpHandler(Task tk);   //到达指定时间事件委托
		public delegate void timeChangedHandler();
		public delegate void listChangedHandler();     //任务列表到期事件委托
		public timeUpHandler onTimeUp;
		public timeChangedHandler onTimeChanged;
		public listChangedHandler onListChange;
		public TaskList timeTsk;
		public bool enable=true;     //在当前任务结束后，TickerTimer的enable值等于该值
		public List<Task> taskToday;
		private int _pauseDays=0;  //暂停几日,暂无用
		private TickerTimer tt;
		private Task nullTask=new Task(new TimeSpan(0,0,0),"No Task",1);   //定义一个空任务
		
		public Task taskNow;     //保存当前任务
		public int pauseDays
		{
			get{return _pauseDays;}
		}
		private int t=0;   //保存当前任务在列表中的计数
		public TimerHelper(TaskList tsk)
		{
		    timeTsk=tsk;
		    taskNow=nullTask;
		    tt=new TickerTimer();
		    tt.onTimeUp+=new TickerTimer.timeUpHandler(timeUp);
		    tt.onTimeChanged+=new TickerTimer.timeChangedHandler(timeChanged);
		    tt.enable=false; 
		}
		public void start()
		{
			if(timeTsk.isInDate(DateTime.Today))   //判断任务是否在期限内
		    {
		    	t=init();
		    }  
		    else
		    {
		    	noTask();
		    	onListChange();
		    }
		}
		public int init()    //初始化
		{
			DateTime dt=DateTime.Now;
			taskToday=new List<Task>();
			for(int i=0;i<timeTsk.Count;i++)    //通过Task类中的isWorkDay重构出当天可用的任务列表
			{
				if(timeTsk.tasks[i].isWorkDay(dt))
				{
					taskToday.Add(timeTsk.tasks[i]);				
				}
			}
			taskToday.Sort();    //以防万一排个序
			taskNow=nullTask;
			int ts=0;
			for(int i=0;i<taskToday.Count;i++)     //在列表中比对当前时间获取下一个时间点的任务
			{
				ts=dt.TimeOfDay.CompareTo(taskToday[i].taskTime);
				if(ts<=0)
				{
					taskNow=taskToday[i];
					tt.defTime=taskNow.taskTime;
					tt.enable=enable;
					return i;				
				}
			}
			tt.defTime=new TimeSpan(0,0,0);
			tt.enable=false;
			return -1;
			
		}
		
		void timeUp(TimeSpan dtin)  //到达指定时间的任务处理
		{
			
			if(t<taskToday.Count-1)
			{			
				taskNow=taskToday[t+1];	
			    tt.defTime=taskNow.taskTime;
                tt.enable=enable;		    
			}
			else
			{
				noTask();
			}
			onTimeUp(taskToday[t]);
			t++;
		}		
		void timeChanged()   //此事件触发时计时器已停止
		{
			
			if (timeTsk.isInDate(DateTime.Today)) {  //当出现时间变更，判断是否还在期限内
				reStart();
				onTimeChanged();     //时间发生更改，或跨天。
			} 
			else {
				noTask();		
				onListChange();     //此时计时器已停止
				tt.initTimer();
			}
			
		}
		
		public void pauseForDays(int d)   //暂停几日
		{
			taskNow=nullTask;
			tt.defTime=taskNow.taskTime;
			enable= false;
			_pauseDays=d;
		}
		
		public void stop()   //计时器停止
		{
			enable=false;
			tt.enable=enable;
			tt.Dispose();
		}
		public void noTask()    //计时器的无任务状态（仍可触发onTimeChanged事件），可在列表到期事件后供调用
		{
			taskNow=nullTask;
			tt.defTime=new TimeSpan(0,0,0);	
			tt.enable=false;
		}
		
		private void reStart()  //内部调用重启计时器，计时器处于停止状态
		{
			if(timeTsk.isInDate(DateTime.Today))   //判断任务是否在期限内
		    {
		    	t=init();
		    }
			else{
				noTask();
				onListChange();	
			}
			tt.initTimer();  //计时器启动
		}
		public void reInit(TaskList tl)   //外部调用重新初始化计时器
		{
			timeTsk=tl;
			if(timeTsk.isInDate(DateTime.Today))   //判断任务是否在期限内
		    {
		    	t=init();
		    } 
			else{
				noTask();
				onListChange();	
			}
			
		}
		public void clear()
		{
			tt.Dispose();
		}
	}
}
