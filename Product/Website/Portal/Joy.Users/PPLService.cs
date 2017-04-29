
using System;
using System.Collections.Generic;
using System.Threading;

namespace Joy.Users
{
    public class PPLService
    {
        public static readonly PPLCache Cache = new PPLCache();

        public static UserIdentity ThreadIdentity
        {
            get
            {
                var ppl = ThreadPPL;
                if (ppl != null)
                {
                    var id = ppl.GetUserIdentity();
                    if (id != null)
                    {
                        return id;
                    }
                }
                ppl = new UserPrincipal();
                Thread.CurrentPrincipal = ppl;
                return ppl.GetUserIdentity();
            }
        }

        public static UserPrincipal ThreadPPL
        {
            get
            {
                var p = Thread.CurrentPrincipal as UserPrincipal;
                if (p == null)
                {
                    p = new UserPrincipal();
                    Thread.CurrentPrincipal = p;
                }
                return p;
            }
        }

        public virtual UserPrincipal GetCurrent()
        {
            return ThreadPPL;
        }

        public virtual void Clear()
        {
            var ppl = ThreadPPL;
            ppl.UnAuthorize();
            Thread.CurrentPrincipal = null;
        }

        public virtual void SetPrincipal(UserPrincipal ppl)
        {
            Thread.CurrentPrincipal = ppl;
        }
    }

    public class PPLCache : Dictionary<string, UserPrincipal>
    {
        private readonly PPLCache self;
        protected int eraseMinutes = 30;
        public PPLCache()
        {
            self = this;
        }
        public UserPrincipal GetValue(string key)
        {
            if (self.ContainsKey(key))
            {
                var ppl = self[key];
                if (ppl == null)
                {
                    ppl = PPLService.ThreadPPL;
                }
                else
                {
                    Thread.CurrentPrincipal = ppl;
                }
                return ppl;
            }
            var u = PPLService.ThreadPPL;
            u.SetClientId(key);
            self[key] = u;
            return u;
        }

        public void SetDefaultEraseTime(int minutes = 30)
        {
            eraseMinutes = minutes;
        }

        public void Erase(string key)
        {
            Remove(key);
            Clean(TimeSpan.FromMinutes(eraseMinutes));
        }

        public void Clean(TimeSpan? span = null)
        {
            if (span == null)
            {
                Clear();
                return;
            }
            var s = this;
            var t = DateTime.Now;
            var ks = new List<string>();
            foreach (var i in Keys)
            {
                var item = s[i];
                if (span >= t - item.TimeCreated)
                {
                    ks.Add(i);
                }
            }
            foreach (var k in ks)
            {
                s.Remove(k);
            }
        }
    }
}
