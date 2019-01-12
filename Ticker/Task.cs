/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/6
 * Time: 10:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ticker
{
	/// <summary>
	/// Description of Task.
	/// </summary>
	public class Task:IComparable<Task>
	{
		public const int KILL=-1;
		public const int CLOSE=0;
        public const int OPEN=1;
        
		private TimeSpan _taskTime;   //保存任务时间
		private String _taskCmd;     //保存任务命令的字符串
		private int _taskMode;
		private Dictionary<DayOfWeek,bool> _workDay =new Dictionary<DayOfWeek, bool>();  //通过DayOfWeek类型查询是否可用
		private DayOfWeek[] aWeek=new DayOfWeek[]{DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday,DayOfWeek.Sunday};
		public Task(String dt,String cmd,String mode)
		{
			setTaskMode(mode);
			setTaskTime(dt);
			setTaskCmd(cmd);
			setWorkDay("1111111");   //以7位字符串传入星期可用信息，1111111表示全星期可用
		}
		public Task(String dt,String cmd,String mode,String weekStr)
		{
			setTaskMode(mode);
			setTaskTime(dt);
			setTaskCmd(cmd);
			setWorkDay(weekStr);
		}
		public Task(TimeSpan dt,String cmd,int mode,String weekStr)
		{
			_taskMode=mode;
			_taskTime=dt;
			setTaskCmd(cmd);
			setWorkDay(weekStr);
		}
		public Task(TimeSpan dt,String cmd,int mode)
		{
			_taskMode=mode;
			_taskTime=dt;
			setTaskCmd(cmd);
			setWorkDay("1111111");
		}
		public TimeSpan taskTime
		{
			get{return _taskTime;}
			set{_taskTime=value;}
		}
		public String taskCmd
		{
			get{return _taskCmd;}	
		}
		public Dictionary<DayOfWeek,bool> workDay
		{
			get{return _workDay;}
		}
		public int taskMode
		{
			get{return _taskMode;}
		}
		public void setTaskTime(String str)
		{

			_taskTime=TimeSpan.Parse(str);
			
		}
		public void setTaskCmd(String str)
		{
			_taskCmd=str;
		}
	    public void setWorkDay(String str)   //将星期可用信息的字符串转化为字典类型
		{
			Dictionary<DayOfWeek,bool> wd=new Dictionary<DayOfWeek, bool>();
			if(str.Length!=7)
			{
				str="1111111";
			}
			char[] cWeek=str.ToCharArray();
			for(int i=0;i<7;i++)
			{
				if(cWeek[i].Equals('0'))
				{
					wd.Add(aWeek[i],false);
				}
				else
				{
					wd.Add(aWeek[i],true);
				}
			}
				
			_workDay=wd;
		}
	    public void setTaskMode(String str)
	    {
	    	if(str=="1"||str=="start"||str=="open")
	    	{
	    		_taskMode=1;
	    	}
	    	else if(str=="close")
	    	{
	    		_taskMode=0;
	    	}
	    	else if(str=="kill")
	    	{
	    		_taskMode=-1;
	    	}
	    }
		public bool isWorkDay(DateTime dt)  //判断该天时候需要启用该任务
		{
			
			return _workDay[dt.DayOfWeek];
			
		}
		public int CompareTo(Task other)   //实现IComparable接口，让Task类可以进行比对
		{
			return this.taskTime.CompareTo(other.taskTime);
		}
		public override string ToString()   //重载ToString方法
		{
			String str="";
			if(_taskMode==0)
			{
				str="Close";
			}
			else if(_taskMode>0)
			{
				str="Open";
			}
			else if(_taskMode<0)
			{
				str="Kill";
			}
			return string.Format("[{0}, {1}, {2}]",_taskTime,_taskCmd,str);
		}
 
	}
}
