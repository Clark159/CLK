using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Mvvm
{
    public interface IValueConverter
    {
        // Methods
        object ToSource(object value);

        object ToTarget(object value);
    }
}
