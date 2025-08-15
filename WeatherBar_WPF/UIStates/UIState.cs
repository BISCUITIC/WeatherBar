using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WeatherBar_WPF.UIStates
{
    abstract class UIState
    {
        public abstract void Apply(UIComponents ui);
    }
}
