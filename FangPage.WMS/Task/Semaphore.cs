using System;
using System.Threading;

namespace FangPage.WMS.Task
{
	// Token: 0x02000039 RID: 57
	public class Semaphore
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00009C8A File Offset: 0x00007E8A
		public Semaphore() : this(1)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009C98 File Offset: 0x00007E98
		public Semaphore(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("Semaphore must have a count of at least 0.", "count");
			}
			this._count = count;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009CDB File Offset: 0x00007EDB
		public void AddOne()
		{
			this.V();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009CE5 File Offset: 0x00007EE5
		public void WaitOne()
		{
			this.P();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009CF0 File Offset: 0x00007EF0
		public void P()
		{
			lock (this._semLock)
			{
				while (this._count <= 0)
				{
					Monitor.Wait(this._semLock, -1);
				}
				this._count--;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009D58 File Offset: 0x00007F58
		public void V()
		{
			lock (this._semLock)
			{
				this._count++;
				Monitor.Pulse(this._semLock);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009DAC File Offset: 0x00007FAC
		public void Reset(int count)
		{
			lock (this._semLock)
			{
				this._count = count;
			}
		}

		// Token: 0x04000134 RID: 308
		private int _count;

		// Token: 0x04000135 RID: 309
		private object _semLock = new object();
	}
}
