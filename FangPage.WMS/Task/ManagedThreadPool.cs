using System;
using System.Collections;
using System.Threading;

namespace FangPage.WMS.Task
{
	// Token: 0x0200003A RID: 58
	public class ManagedThreadPool
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00009DEC File Offset: 0x00007FEC
		static ManagedThreadPool()
		{
			ManagedThreadPool.Initialize();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009E00 File Offset: 0x00008000
		private static void Initialize()
		{
			ManagedThreadPool._waitingCallbacks = new Queue();
			ManagedThreadPool._workerThreads = new ArrayList();
			ManagedThreadPool._inUseThreads = 0;
			ManagedThreadPool._workerThreadNeeded = new Semaphore(0);
			for (int i = 0; i < 10; i++)
			{
				Thread thread = new Thread(new ThreadStart(ManagedThreadPool.ProcessQueuedItems));
				ManagedThreadPool._workerThreads.Add(thread);
				thread.Name = "ManagedPoolThread #" + i.ToString();
				thread.IsBackground = true;
				thread.Start();
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009E8B File Offset: 0x0000808B
		public static void QueueUserWorkItem(WaitCallback callback)
		{
			ManagedThreadPool.QueueUserWorkItem(callback, null);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009E98 File Offset: 0x00008098
		public static void QueueUserWorkItem(WaitCallback callback, object state)
		{
			ManagedThreadPool.WaitingCallback obj = new ManagedThreadPool.WaitingCallback(callback, state);
			lock (ManagedThreadPool._poolLock)
			{
				ManagedThreadPool._waitingCallbacks.Enqueue(obj);
			}
			ManagedThreadPool._workerThreadNeeded.AddOne();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009EF0 File Offset: 0x000080F0
		public static void Reset()
		{
			lock (ManagedThreadPool._poolLock)
			{
				try
				{
					foreach (object obj in ManagedThreadPool._waitingCallbacks)
					{
						ManagedThreadPool.WaitingCallback waitingCallback = (ManagedThreadPool.WaitingCallback)obj;
						if (waitingCallback.State is IDisposable)
						{
							((IDisposable)waitingCallback.State).Dispose();
						}
					}
				}
				catch
				{
				}
				try
				{
					foreach (object obj2 in ManagedThreadPool._workerThreads)
					{
						Thread thread = (Thread)obj2;
						if (thread != null)
						{
							thread.Abort("reset");
						}
					}
				}
				catch
				{
				}
				ManagedThreadPool.Initialize();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A048 File Offset: 0x00008248
		public static int MaxThreads
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A05C File Offset: 0x0000825C
		public static int ActiveThreads
		{
			get
			{
				return ManagedThreadPool._inUseThreads;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A074 File Offset: 0x00008274
		public static int WaitingCallbacks
		{
			get
			{
				int count;
				lock (ManagedThreadPool._poolLock)
				{
					count = ManagedThreadPool._waitingCallbacks.Count;
				}
				return count;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A0B8 File Offset: 0x000082B8
		private static void ProcessQueuedItems()
		{
			for (;;)
			{
				ManagedThreadPool._workerThreadNeeded.WaitOne();
				ManagedThreadPool.WaitingCallback waitingCallback = null;
				lock (ManagedThreadPool._poolLock)
				{
					if (ManagedThreadPool._waitingCallbacks.Count > 0)
					{
						try
						{
							waitingCallback = (ManagedThreadPool.WaitingCallback)ManagedThreadPool._waitingCallbacks.Dequeue();
						}
						catch
						{
						}
					}
				}
				if (waitingCallback != null)
				{
					try
					{
						Interlocked.Increment(ref ManagedThreadPool._inUseThreads);
						waitingCallback.Callback(waitingCallback.State);
					}
					catch
					{
					}
					finally
					{
						Interlocked.Decrement(ref ManagedThreadPool._inUseThreads);
					}
				}
			}
		}

		// Token: 0x04000136 RID: 310
		private const int _maxWorkerThreads = 10;

		// Token: 0x04000137 RID: 311
		private static Queue _waitingCallbacks;

		// Token: 0x04000138 RID: 312
		private static Semaphore _workerThreadNeeded;

		// Token: 0x04000139 RID: 313
		private static ArrayList _workerThreads;

		// Token: 0x0400013A RID: 314
		private static int _inUseThreads;

		// Token: 0x0400013B RID: 315
		private static object _poolLock = new object();

		// Token: 0x0200003B RID: 59
		private class WaitingCallback
		{
			// Token: 0x060002B1 RID: 689 RVA: 0x0000A1A8 File Offset: 0x000083A8
			public WaitingCallback(WaitCallback callback, object state)
			{
				this._callback = callback;
				this._state = state;
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A1C4 File Offset: 0x000083C4
			public WaitCallback Callback
			{
				get
				{
					return this._callback;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000A1DC File Offset: 0x000083DC
			public object State
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x0400013C RID: 316
			private WaitCallback _callback;

			// Token: 0x0400013D RID: 317
			private object _state;
		}
	}
}
