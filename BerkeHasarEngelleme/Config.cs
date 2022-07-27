using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace OPlugins_Saganetwork_HasarEngelleme
{
    public class Config : IRocketPluginConfiguration
    {
        public List<Maden> Madenler = new List<Maden>();
        public List<ulong> Madendekiler = new List<ulong>();
        public void LoadDefaults()
        {
            Madenler = new List<Maden>();
            Madendekiler = new List<ulong>();
        }
    }
}
