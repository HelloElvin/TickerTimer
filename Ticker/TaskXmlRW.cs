/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/22
 * Time: 11:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Ticker
{
	/// <summary>
	/// Description of TaskXmlRW.
	/// </summary>
	public class TaskXmlRW
	{
		public XmlDocument xDoc;
		public int tlCount;
		public List<TaskList> taskLists=new List<TaskList>();
		public List<String> nameList=new List<string>();
		public TaskXmlRW(String str)
		{
			xDoc=loadXml(str);
			readAllTask();
		}
		public void load(String str)
		{
			xDoc=loadXml(str);
		}
		public void save(String str)
		{
			saveXml(xDoc,str);
		}
		private XmlDocument loadXml(String str)
        {
            FileStream stream = new FileStream(str,
                FileMode.Open, FileAccess.Read);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            stream.Close();
            return doc;
        }
        private void saveXml(XmlDocument doc,String str)
        {
            FileStream stream = new FileStream(str,
                FileMode.Create, FileAccess.Write);
            doc.Save(stream);
            stream.Close();
        }
        public void readAllTask()  //读取所有任务列表
        {	
        	XmlNode xnRoot=xDoc.FirstChild;
        	tlCount=xnRoot.ChildNodes.Count;
        	foreach (XmlNode xn in xnRoot.ChildNodes)
        	{
        	TaskList tl=new TaskList();
			tl.name=xn.Attributes["name"].Value;
			nameList.Add(tl.name);
			tl.startDate=StrToDateTime(xn.Attributes["startDate"].Value);
			tl.endDate=StrToDateTime(xn.Attributes["endDate"].Value);
			XmlNodeList xl=xn.ChildNodes;
				foreach (XmlNode xNode in xl) {
					Task tsk;
					if (xNode.SelectSingleNode("Cmd").Attributes.Count > 0) {
						tsk = new Task(xNode.SelectSingleNode("Time").InnerText,
							xNode.SelectSingleNode("Cmd").InnerText,
							xNode.SelectSingleNode("Cmd").Attributes["action"].Value,
							xNode.SelectSingleNode("workDay").InnerText
						);
					} else {
						tsk = new Task(xNode.SelectSingleNode("Time").InnerText,
							xNode.SelectSingleNode("Cmd").InnerText,
							"1",
							xNode.SelectSingleNode("workDay").InnerText
						);
					}
					tl.Add(tsk);
				}
			taskLists.Add(tl);
			}
        }
        public TaskList readTask()  //读取第一个任务
        {
        	
        	return taskLists[0];
        	
        }
        public TaskList readTask(String name)
        {
			foreach (TaskList tl in taskLists) {
				if (tl.name == name) {
					return tl;
				}
			}
        	return taskLists[0];
        }
        private DateTime StrToDateTime(String str)
        {
        	String[] date=str.Split('.');
        	int m=Convert.ToInt32(date[0]);
        	int d=Convert.ToInt32(date[1]);
        	DateTime dt = new DateTime(DateTime.Today.Year,m,d);
        	return dt;
        }
	}
}
