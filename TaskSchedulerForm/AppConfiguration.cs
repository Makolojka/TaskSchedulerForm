﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    [Serializable]
    public class AppConfiguration
    {
        public string SelectedFolderPath { get; set; }
        public bool IsAppStartChecked { get; set; }
    }
}
