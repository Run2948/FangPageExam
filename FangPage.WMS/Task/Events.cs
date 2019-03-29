using System;
using FangPage.Data;

namespace FangPage.WMS.Task
{
	// Token: 0x02000037 RID: 55
	public class Events
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000993C File Offset: 0x00007B3C
		public IEvent IEventInstance
		{
			get
			{
				this.LoadIEvent();
				return this._ievent;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000995C File Offset: 0x00007B5C
		private void LoadIEvent()
		{
			if (this._ievent == null)
			{
				if (string.IsNullOrEmpty(this.ScheduleType))
				{
					throw new Exception(string.Format("【{0}】没有定义其【执行类型】属性", this.Name));
				}
				Type type = Type.GetType(this.ScheduleType);
				if (type == null)
				{
					throw new Exception(string.Format("【{0}】的【{1}】无法被正确识别", this.Name, this.ScheduleType));
				}
				this._ievent = (IEvent)Activator.CreateInstance(type);
				if (this._ievent == null)
				{
					throw new Exception(string.Format("【{0}】的【{0}】未能正确加载", this.Name, this.ScheduleType));
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009A1C File Offset: 0x00007C1C
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00009A34 File Offset: 0x00007C34
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009A40 File Offset: 0x00007C40
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009A58 File Offset: 0x00007C58
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009A64 File Offset: 0x00007C64
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00009A7C File Offset: 0x00007C7C
		public int TimeOfDay
		{
			get
			{
				return this._timeOfDay;
			}
			set
			{
				this._timeOfDay = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009A88 File Offset: 0x00007C88
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00009ABD File Offset: 0x00007CBD
		public int Minutes
		{
			get
			{
				int result;
				if (this._minutes < TaskManager.TimerMinutesInterval)
				{
					result = TaskManager.TimerMinutesInterval;
				}
				else
				{
					result = this._minutes;
				}
				return result;
			}
			set
			{
				this._minutes = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009AC8 File Offset: 0x00007CC8
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public string ScheduleType
		{
			get
			{
				return this._scheduleType;
			}
			set
			{
				this._scheduleType = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009AEC File Offset: 0x00007CEC
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00009B04 File Offset: 0x00007D04
		public DateTime LastCompleted
		{
			get
			{
				return this._lastCompleted;
			}
			set
			{
				this.dateWasSet = true;
				this._lastCompleted = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009B18 File Offset: 0x00007D18
		public bool ShouldExecute
		{
			get
			{
				if (!this.dateWasSet)
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("key", this.Key);
					string value = DbHelper.ExecuteMax<TaskInfo>("lastexecuted", new SqlParam[]
					{
						sqlParam
					}).ToString();
					if (string.IsNullOrEmpty(value))
					{
						this.LastCompleted = DateTime.MinValue;
					}
					else
					{
						this.LastCompleted = Convert.ToDateTime(value);
					}
				}
				bool result;
				if (this.TimeOfDay > -1)
				{
					DateTime now = DateTime.Now;
					DateTime dateTime = new DateTime(now.Year, now.Month, now.Day);
					result = (this.LastCompleted < dateTime.AddMinutes((double)this.TimeOfDay) && dateTime.AddMinutes((double)this.TimeOfDay) <= DateTime.Now);
				}
				else
				{
					result = (this.LastCompleted.AddMinutes((double)this.Minutes) < DateTime.Now);
				}
				return result;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009C2C File Offset: 0x00007E2C
		public void UpdateTime()
		{
			this.LastCompleted = DateTime.Now;
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeSet("lastexecuted", this.LastCompleted.ToString()),
				DbHelper.MakeAndWhere("key", this.Key)
			};
			DbHelper.ExecuteUpdate<TaskInfo>(sqlparams);
		}

		// Token: 0x0400012C RID: 300
		private IEvent _ievent = null;

		// Token: 0x0400012D RID: 301
		private string _name;

		// Token: 0x0400012E RID: 302
		private string _key;

		// Token: 0x0400012F RID: 303
		private int _timeOfDay = -1;

		// Token: 0x04000130 RID: 304
		private int _minutes = 60;

		// Token: 0x04000131 RID: 305
		private string _scheduleType;

		// Token: 0x04000132 RID: 306
		private DateTime _lastCompleted;

		// Token: 0x04000133 RID: 307
		private bool dateWasSet = false;
	}
}
