using System;
using System.Collections.Generic;
using System.Threading;
using FangPage.Data;

namespace FangPage.WMS.Task
{
	// Token: 0x0200003D RID: 61
	public class TaskManager
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000A36C File Offset: 0x0000856C
		static TaskManager()
		{
			if (SysConfigs.TaskInterval > 0)
			{
				TaskManager.TimerMinutesInterval = SysConfigs.TaskInterval;
			}
			TaskManager.Reset();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A3A2 File Offset: 0x000085A2
		public static void Reset()
		{
			TaskManager.tasklist = DbHelper.ExecuteList<TaskInfo>();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public static void Execute()
		{
			List<Events> list = new List<Events>();
			foreach (TaskInfo taskInfo in TaskManager.tasklist)
			{
				if (taskInfo.enabled != 0 && !(taskInfo.type == ""))
				{
					list.Add(new Events
					{
						Name = taskInfo.name,
						Key = taskInfo.key,
						Minutes = taskInfo.minutes,
						ScheduleType = taskInfo.type,
						TimeOfDay = taskInfo.timeofday
					});
				}
			}
			Events[] array = list.ToArray();
			if (array != null)
			{
				foreach (Events events in array)
				{
					if (events.ShouldExecute)
					{
						events.UpdateTime();
						IEvent ieventInstance = events.IEventInstance;
						ManagedThreadPool.QueueUserWorkItem(new WaitCallback(ieventInstance.Execute));
					}
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public static void Execute(TaskInfo taskinfo)
		{
			Events events = new Events();
			events.Name = taskinfo.name;
			events.Key = taskinfo.key;
			events.Minutes = taskinfo.minutes;
			events.ScheduleType = taskinfo.type;
			events.TimeOfDay = taskinfo.timeofday;
			events.UpdateTime();
			IEvent ieventInstance = events.IEventInstance;
			ManagedThreadPool.QueueUserWorkItem(new WaitCallback(ieventInstance.Execute));
		}

		// Token: 0x04000146 RID: 326
		public static readonly int TimerMinutesInterval = 5;

		// Token: 0x04000147 RID: 327
		private static List<TaskInfo> tasklist;
	}
}
