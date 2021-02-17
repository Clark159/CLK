using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Settings
{
    public sealed class MemorySettingContext : SettingContext
    {
        // Constructors
        public MemorySettingContext() : this(new MemorySettingRepository(), new MemorySettingRepository()) { }

        public MemorySettingContext(ISettingRepository appSettingRepository, ISettingRepository connectionStringRepository) : base(appSettingRepository, connectionStringRepository) { }
    }
}
