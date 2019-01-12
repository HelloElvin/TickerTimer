/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/21
 * Time: 18:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ticker
{
	/// <summary>
	/// Description of TaskList.
	/// </summary>
	public class TaskList
	{
		public List<Task> tasks=new List<Task>();
		private DateTime _startDate;
		private DateTime _endDate;
		private	String _name;
		public int Count
		{
			get{return tasks.Count;}
		}
		public String name
		{
			get{return _name;}
			set{_name=value;}
		}
		public DateTime startDate
		{
			get
			{
				DateTime dt=new DateTime(DateTime.Today.Year,_startDate.Month,_startDate.Day);
				return dt;
			}
			set{_startDate=value;}
		}
		public DateTime endDate
		{
			get
			{
				DateTime dt;	
				dt=new DateTime(DateTime.Today.Year,_endDate.Month,_endDate.Day);	
				return dt;
			}
			set{_endDate=value;}
		}
		public TaskList()
		{
		}
		public TaskList(String str)
		{
			_name=str;
		}
		public void Add(Task tsk)
		{
			tasks.Add(tsk);
		}
		public bool isInDate(DateTime dt)  //判断是否在期限内
		{
			if(CompareDate(startDate,endDate.AddDays(1))) //如果起始日期大于结束日期，则判定为永久起效。
			{
				return true;
			}
			if(CompareDate(dt,startDate)&&CompareDate(endDate,dt)) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool CompareDate(DateTime d1,DateTime d2)  //日期比较方法，仅比较月日
		{
			if(d1.Month>d2.Month)
			{
				return true;
			}
			else if(d1.Month==d2.Month)
			{
				if(d1.Day>=d2.Day)
				{
					return true;
				}
				else {return false;}
			}
			else{return false;}
		}
	}
}
