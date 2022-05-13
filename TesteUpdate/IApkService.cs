using System;
using System.Collections.Generic;
using System.Text;

namespace TesteUpdate
{
    public interface IApkService
    {
        void install(string pPath);
        string GetPath();
    }
}
