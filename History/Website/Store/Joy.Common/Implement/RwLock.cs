using Joy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Joy.Common.Implement
{
	public class RwLock : JLock
	{
		protected bool isSlimVersion;
		protected ReaderWriterLock full = new ReaderWriterLock();
		protected ReaderWriterLockSlim slim = new ReaderWriterLockSlim();

		public RwLock(bool isSlim = true)
		{
			isSlimVersion = isSlim;
		}
		public override void EnterReadLock()
		{
			if (isSlimVersion)
			{
				slim.EnterReadLock();
			}
			else
			{
				full.AcquireReaderLock(int.MaxValue);
			}
		}

		public override void EnterWriteLock()
		{
			if (isSlimVersion)
			{
				slim.EnterWriteLock();
			}
			else
			{
				full.AcquireWriterLock(int.MaxValue);
			}
		}

		public override void ExitReadLock()
		{
			if (isSlimVersion)
			{
				slim.ExitReadLock();
			}
			else
			{
				full.ReleaseReaderLock();
			}
		}

		public override void ExitWriteLock()
		{
			if (isSlimVersion)
			{
				slim.ExitWriteLock();
			}
			else
			{
				full.ReleaseWriterLock();
			}
		}
	}
}
