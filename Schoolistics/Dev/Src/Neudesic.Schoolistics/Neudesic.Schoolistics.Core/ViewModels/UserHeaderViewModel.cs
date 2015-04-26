using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neudesic.Schoolistics.Core.ViewModels
{
    public class UserHeaderViewModel : BaseViewModel
    {
       
        private MvxCommand _profileNavigate;
        public ICommand ProfileNavigate
        {
            get
            {
                return this._profileNavigate ?? (this._profileNavigate = new MvxCommand(() => this.ShowViewModel<MyprofileViewModel>()));
            }
        }
    }
}
