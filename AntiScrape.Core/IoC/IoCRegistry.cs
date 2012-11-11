using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityConfiguration;
using AntiScrape.Core.Interfaces;

namespace AntiScrape.Core.IoC
{
    public class IoCRegistry : UnityRegistry
    {
        public IoCRegistry()
        {
            //scan for implementations
            Scan(scan =>
            {
                scan.AssembliesInBaseDirectory();
                scan.ForRegistries();
                scan.With<FirstInterfaceConvention>();
            });

            //configure the data storage as a singleton
            Configure<IDataStorage>().AsSingleton();
        }
    }
}
