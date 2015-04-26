using Cirrious.MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class NavigationMessage<T> : MvxMessage where T:class
    {
        public NavigationMessage(object sender)
            : base(sender)
        { }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
