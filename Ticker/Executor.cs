/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/23
 * Time: 12:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace Ticker
{
	/// <summary>
	/// Description of Executor.
	/// </summary>
	public class Executor
	{
		public Process process = new Process();
		public String cmd="";
		private int mode=1;
		public Executor(String str,int i)
		{
			cmd=str;
			mode=i;
		}
		public Executor()
		{
		}
		public void Execute()
		{
			if(mode>0)
			{
				cmdStructor(cmd);
				process.Start();
			}
			else if(mode==0)
			{
				close(cmd);
			}
			else if(mode<0)
			{
				kill(cmd);
			}
		}
		private void start()
		{
			process.Start();
		}
		public void start(String str)
		{
			cmdStructor(str);
			process.Start();
		}
		private void cmdStructor(String str)
		{
			String[] ss=str.Split(' ');
			process.StartInfo.FileName=ss[0];
			if(ss.Length>1)
			{
				String s=ss[1];
				for(int i=2;i<ss.Length;i++)
				{
					s+=" "+s[i];
				}
				process.StartInfo.Arguments=s;
			}
		}
		public void close(String name)
		{
			Process[] pro=Process.GetProcessesByName(name);
			foreach(Process p in pro)
			{
				p.CloseMainWindow();
			}
		}
		public void kill(String name)
		{
			Process[] pro=Process.GetProcessesByName(name);
			foreach(Process p in pro)
			{
				p.Kill();
			}
		}
	}
}
