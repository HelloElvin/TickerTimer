/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/3
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Ticker
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public TaskList lTsk=new TaskList();
		public TimerHelper th;
		public String path;
		public TaskXmlRW tx;
		private DayOfWeek[] aWeek=new DayOfWeek[]{DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday,DayOfWeek.Sunday};
		public MainForm(string[] args)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			//DelayTimer dt=new DelayTimer(10);
			//dt.onTimeTick=new DelayTimer.timeTickHandler(timeTick);
			//dt.onTimeUp=new DelayTimer.timeUpHandler(timeUp);
			
			path=getPath()+"taskList.xml";
			if(args.Length>0)
			{
				path=getPath()+args[0];
			}
			tx=new TaskXmlRW(path);
			lTsk=tx.readTask();
			tasklistBox.DataSource=tx.nameList;
			th=new TimerHelper(lTsk);
			th.onTimeUp+=new TimerHelper.timeUpHandler(timeUp);
			th.onTimeChanged+=new TimerHelper.timeChangedHandler(timeChange);
			th.onListChange+=new TimerHelper.listChangedHandler(listChange);
			th.start();
			lList.Text=tx.tlCount.ToString();
			label1.Text=th.taskNow.ToString()+"<<<<<";
		}
		public void changeTaskList()
		{
			foreach (TaskList tls in tx.taskLists) {
				if (tls.isInDate(DateTime.Today)) {
					lTsk = tls;
					th.reInit(lTsk);
					if(this.InvokeRequired){
					this.Invoke(new EventHandler(delegate {
						lList.Text = tx.tlCount.ToString();
						label1.Text = th.taskNow.ToString() + "<<<<<";
						tasklistBox.Text=lTsk.name;
						                             }));}
					else{
						lList.Text = tx.tlCount.ToString();
						label1.Text = th.taskNow.ToString() + "<<<<<";
						tasklistBox.Text=lTsk.name;
					}
					break;
				}
			}
		}
		public void showTasks()
		{
			label1.Text+="\nFrom "+lTsk.startDate.ToShortDateString()+" to "+lTsk.endDate.ToShortDateString();
			foreach(Task t in lTsk.tasks)
				label1.Text+="\n"+t.ToString();
		}
		public String getPath()
		{
			String str=Application.StartupPath+@"\";
			return str;
		}
		
		void timeChange()
		{
			this.Invoke(new EventHandler(delegate
            {

			    label1.Text=th.taskNow.ToString()+"<<<<<";

            }));
			
		}
		void timeUp(Task t)
		{
			Executor ex=new Executor(t.taskCmd,t.taskMode);
			ex.Execute();
			this.Invoke(new EventHandler(delegate
            {

			    label1.Text+="完成\n"+th.taskNow.ToString()+"<<<<<";

            }));
		}
		void listChange()
		{
			if(csCbx.Checked)
			{
				changeTaskList();
			}
		}
		
		void timeTick(int t)
		{
			this.Invoke(new EventHandler(delegate

            {

			    label1.Text=t.ToString();

            }),t);
		}
		
		void csChanged(object sender, EventArgs e)
		{
			panel1.VerticalScroll.Value=panel1.VerticalScroll.Maximum;
		}
		void Button1Click(object sender, EventArgs e)
		{
			showTasks();
			label1.Text+="\n"+th.taskNow.ToString()+"<<<<<";
		}
		
		void tasklistBoxChanged(object sender, EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
			String st=cb.Text;
			lTsk=tx.readTask(st);
			th.reInit(lTsk);
			lList.Text=tx.tlCount.ToString();
			label1.Text=th.taskNow.ToString()+"<<<<<";
	
		}
		void csCbxCheckedChanged(object sender, EventArgs e)
		{
	
		}
	}
	
	
}
