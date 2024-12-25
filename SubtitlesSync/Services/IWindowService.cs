using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesSync.Services
{
    interface IWindowService
    {
        void OpenWindow(object dataContext);
        void CloseWindow();
    }
}
