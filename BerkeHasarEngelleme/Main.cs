using System;
using System.Collections.Generic;
using Rocket.Core.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using SDG.Unturned;
using UnityEngine;
using Rocket.Unturned.Player;
using Rocket.API;
using Rocket.Unturned.Chat;

namespace OPlugins_Saganetwork_HasarEngelleme
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            Console.WriteLine("Bu Plugin Omer#0760 Tarafından Yapılmıştır ");
            DamageTool.damagePlayerRequested += DamageTool_damagePlayerRequested;
            UnturnedPlayerEvents.OnPlayerUpdatePosition += UnturnedPlayerEvents_OnPlayerUpdatePosition;
        }

        private void UnturnedPlayerEvents_OnPlayerUpdatePosition(UnturnedPlayer player, Vector3 position)
        {
            var oyuncununbulundugualan = Configuration.Instance.Madenler.FirstOrDefault(x => Vector3.Distance(new Vector3(x.X, x.Y, x.Z), player.Position) <= x.YarıÇap);
            if(oyuncununbulundugualan != null)
            {
                if (Vector3.Distance(position, player.Position) < oyuncununbulundugualan.YarıÇap)
                {
                    player.GodMode = true;
                    if (!Configuration.Instance.Madendekiler.Contains(player.CSteamID.m_SteamID))
                    {
                        Configuration.Instance.Madendekiler.Add(player.CSteamID.m_SteamID);
                        Configuration.Save();
                        UnturnedChat.Say(player, "Madene girdin!");
                    }
                }
                else
                {
                    player.GodMode = false;
                    if (Configuration.Instance.Madendekiler.Contains(player.CSteamID.m_SteamID))
                    {
                        Configuration.Instance.Madendekiler.Remove(player.CSteamID.m_SteamID);
                        Configuration.Save();
                        UnturnedChat.Say(player, "Madene Çıktın!");
                    }
                }
            }
        }

        private void DamageTool_damagePlayerRequested(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(parameters.player);
            var oyuncununbulundugualan = Configuration.Instance.Madenler.FirstOrDefault(x => Vector3.Distance(new Vector3(x.X, x.Y, x.Z), player.Position) <= x.YarıÇap);
            if(oyuncununbulundugualan != null)
            {
                if (Vector3.Distance(new Vector3(oyuncununbulundugualan.X, oyuncununbulundugualan.Y, oyuncununbulundugualan.Z), player.Position) < oyuncununbulundugualan.YarıÇap)
                {
                    shouldAllow = false;
                }
                else
                    return;
            }
        }

        [RocketCommand("madenolustur", "Madeni ayarlar!", "/madenolustur <yarıçap>")]
        [RocketCommandPermission("saga.madenolustur")]
        public void example(IRocketPlayer caller, string[] args)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            var yarıçap = args[0];
            var Name = args[1];
            var realyarıçap = Convert.ToUInt32(yarıçap);
            Configuration.Instance.Madenler.Add(new Maden
            {
                Name = Name,
                X = player.Position.x,
                Y = player.Position.y,
                Z = player.Position.z,
                YarıÇap = (float)realyarıçap
                
            });
            Configuration.Save();
            UnturnedChat.Say(player, "Maden oluşturuldu!");
        }
    }
}
