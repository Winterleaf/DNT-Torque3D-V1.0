﻿// Copyright (C) 2012 Winterleaf Entertainment L,L,C.
// 
// THE SOFTW ARE IS PROVIDED ON AN “ AS IS” BASIS, WITHOUT W ARRANTY OF ANY KIND,
// INCLUDING WITHOUT LIMIT ATION THE W ARRANTIES OF MERCHANT ABILITY, FITNESS
// FOR A PARTICULAR PURPOSE OR NON-INFRINGEMENT . THE ENTIRE RISK AS TO THE
// QUALITY AND PERFORMANCE OF THE SOFTW ARE IS THE RESPONSIBILITY OF LICENSEE.
// SHOULD THE SOFTW ARE PROVE DEFECTIVE IN ANY RESPECT , LICENSEE AND NOT LICEN -
// SOR OR ITS SUPPLIERS OR RESELLERS ASSUMES THE ENTIRE COST OF AN Y SERVICE AND
// REPAIR. THIS DISCLAIMER OF W ARRANTY CONSTITUTES AN ESSENTIAL PART OF THIS
// AGREEMENT. NO USE OF THE SOFTW ARE IS AUTHORIZED HEREUNDER EXCEPT UNDER
// THIS DISCLAIMER.
// 
// The use of the WinterLeaf Entertainment LLC DotNetT orque (“DNT ”) and DotNetT orque
// Customizer (“DNTC”)is governed by this license agreement (“ Agreement”).
// 
// R E S T R I C T I O N S
// 
// (a) Licensee may not: (i) create any derivative works of DNTC, including but not
// limited to translations, localizations, technology add-ons, or game making software
// other than Games; (ii) reverse engineer , or otherwise attempt to derive the algorithms
// for DNT or DNTC (iii) redistribute, encumber , sell, rent, lease, sublicense, or otherwise
// transfer rights to  DNTC; or (iv) remove or alter any tra demark, logo, copyright
// or other proprietary notices, legends, symbols or labels in DNT or DNTC; or (iiv) use
// the Software to develop or distribute any software that compete s with the Software
// without WinterLeaf Entertainment’s prior written consent; or (i iiv) use the Software for
// any illegal purpose.
// (b) Licensee may not distribute the DNTC in any manner.
// 
// LI C E N S E G R A N T .
// This license allows companies of any size, government entities or individuals to cre -
// ate, sell, rent, lease, or otherwise profit commercially from, games using executables
// created from the source code of DNT
// 
// **********************************************************************************
// **********************************************************************************
// **********************************************************************************
// THE SOURCE CODE GENERATED BY DNTC CAN BE  DISTRIBUTED PUBLICLY PROVIDED THAT THE 
// DISTRIBUTOR PROVIDES  THE GENERATE SOURCE CODE FREE OF CHARGE.
// 
// THIS SOURCE CODE (DNT) CAN BE DISTRIBUTED PUBLICLY PROVIDED THAT THE DISTRIBUTOR 
// PROVIDES  THE SOURCE CODE (DNT) FREE OF CHARGE.
// **********************************************************************************
// **********************************************************************************
// **********************************************************************************
// 
// Please visit http://www.winterleafentertainment.com for more information about the project and latest updates.
// 
// Last updated: 10/18/2012
// 

#region

using WinterLeaf;
using WinterLeaf.Classes;

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Client
    {
    public partial class Main : TorqueScriptTemplate
        {
        //-----------------------------------------------------------------------------
        // Torque
        // Copyright GarageGames, LLC 2011
        //-----------------------------------------------------------------------------

        //-----------------------------------------------------------------------------
        // Server Admin Commands
        //-----------------------------------------------------------------------------


        [Torque_Decorations.TorqueCallBack("", "", "SAD", "(%password)", 1, 6000, false)]
        public string Sad(string password)
            {
            if (password.Trim() != "")
                console.commandToServer("SAD", new[] {password});
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "SADSetPassword", "(%password)", 1, 6000, false)]
        public string SadSetPassword(string password)
            {
            console.commandToServer("SADSetPassword", new[] {password});
            return string.Empty;
            }

        //----------------------------------------------------------------------------
        // Misc server commands
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "clientCmdSyncClock", "(%time)", 1, 6000, false)]
        public string ClientCmdSyncClock(string time)
            {
            // Time update from the server, this is only sent at the start of a mission
            // or when a client joins a game in progress.
            return string.Empty;
            }

        //-----------------------------------------------------------------------------
        // Numerical Health Counter
        //-----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "clientCmdSetNumericalHealthHUD", "(%curHealth)", 1, 6000, false)]
        public void ClientCmdSetNumericalHealthHud(string curHealth)
            {
            // Skip if the hud is missing.
            if (!console.isObject("numericalHealthHUD"))
                return;

            //console.Call("numericalHealthHUD", "setValue", new string[] {curHealth});

            // The server has sent us our current health, display it on the HUD
            GuiTextCtrl.setText("numericalHealthHUD", curHealth);

            // Ensure the HUD is set to visible while we have health / are alive
            GuiControl.setVisible("HealthHUD", curHealth.AsBool() ? true : false);
            //console.Call("HealthHUD", "setVisible",new string[]{ (curHealth.AsBool() ? true : false).AsString()});
            }


        //-----------------------------------------------------------------------------
        // Damage Direction Indicator
        //-----------------------------------------------------------------------------

        [Torque_Decorations.TorqueCallBack("", "", "clientCmdSetDamageDirection", "(%direction)", 1, 6000, false)]
        public void ClientCmdSetDamageDirection(string direction)
            {
            string ctrl = SimSet.findObjectByInternalName("DamageHUD", "damage_" + direction, true);
            if (!console.isObject(ctrl))
                return;

            console.Call("cancelAll", new[] {ctrl});
            //console.Call(ctrl, "setVisible", new string[] { "true" });
            GuiControl.setVisible(ctrl, true);
            //using (var bwr = new BackgroundWorker())
            //    {
            //    bwr.DoWork += bwr_DamageDirectionTurnOff;
            //    bwr.RunWorkerAsync((object)ctrl);
            //    }
            ////This is no longer needed since we are using a thread.
            SimObject.schedule(ctrl, "500", "setVisible", "false");
            }

        //public void bwr_DamageDirectionTurnOff(object sender, DoWorkEventArgs e)
        //    {
        //    Thread.Sleep(500);
        //    //Since this is happening out of process, we need prevent the torque
        //    //engine from ticking while we do this.  If we don't the memory will
        //    //get corrupt if two processes enter the Torque DLL at the same time.
        //    //So we use a Monitor and lock an object which prevents an engine tick
        //    //from occuring.
        //    lock (m_ts.tick)
        //        GuiControl.setVisible((string)e.Argument, false);
        //    }


        //-----------------------------------------------------------------------------
        // Teleporter visual effect
        //-----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "clientCmdPlayTeleportEffect", "(%position, %effectDataBlock)", 2, 6000, false)]
        public string ClientCmdPlayTeleportEffect(string position, string effectDataBlock)
            {
            if (console.isObject(effectDataBlock))
                {
                Torque_Class_Helper tch = new Torque_Class_Helper("Explosion");
                tch.Props.Add("position", '"' + position + '"');
                tch.Props.Add("datablock", effectDataBlock);
                tch.Create(m_ts);
                }
            return string.Empty;
            }


        // ----------------------------------------------------------------------------
        // WeaponHUD
        // ----------------------------------------------------------------------------

        // Update the Ammo Counter with current ammo, if not any then hide the counter.
        [Torque_Decorations.TorqueCallBack("", "", "clientCmdSetAmmoAmountHud", "(%amount, %amountInClips)", 2, 6000, false)]
        public string ClientCmdSetAmmoAmountHud(string amount, string amountInClips)
            {
            if (!amount.AsBool())
                GuiControl.setVisible("AmmoAmount", false);
            else
                {
                GuiControl.setVisible("AmmoAmount", true);
                GuiTextCtrl.setText("AmmoAmount", "Ammo: " + amount + "/" + amountInClips);
                }
            return string.Empty;
            }

        // Here we update the Weapon Preview image & reticle for each weapon.  We also
        // update the Ammo Counter (just so we don't have to call it separately).
        // Passing an empty parameter ("") hides the HUD component.


        [Torque_Decorations.TorqueCallBack("", "", "clientCmdRefreshWeaponHUD", "(%amount, %preview, %ret, %zoomRet, %amountInClips)", 5, 6000, false)]
        public string ClientCmdRefreshWeaponHud(string amount, string preview, string ret, string zoomret, string amountInClips)
            {
            amount = Util.detag(amount);
            preview = Util.detag(preview);
            ret = Util.detag(ret);
            zoomret = Util.detag(zoomret);
            amountInClips = Util.detag(amountInClips);
            if (!amount.AsBool())
                GuiControl.setVisible("AmmoAmount", false);
            else
                {
                if (console.isObject("AmmoAmount"))
                    {
                    GuiControl.setVisible("AmmoAmount", true);
                    GuiTextCtrl.setText("AmmoAmount", "Ammo: " + amount + "/" + amountInClips);
                    }
                }

            if (preview.Trim() == "")
                console.Call("WeaponHUD", "setVisible", new[] {"false"});
            else
                {
                GuiControl.setVisible("WeaponHUD", true);
                GuiBitmapCtrl.setBitmap("PreviewImage", "art/gui/weaponHud/" + preview);
                }

            if (ret.Trim() == "")
                GuiControl.setVisible("Reticle", false);
            else
                {
                GuiControl.setVisible("Reticle", true);
                GuiBitmapCtrl.setBitmap("Reticle", "art/gui/weaponHud/" + ret);
                }

            if (console.isObject("ZoomReticle"))
                {
                GuiBitmapCtrl.setBitmap("ZoomReticle", zoomret == "" ? "" : "art/gui/weaponHud/" + zoomret);
                }
            return string.Empty;
            }

        // ----------------------------------------------------------------------------
        // Vehicle Support
        // ----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "clientCmdtoggleVehicleMap", "(%toggle)", 1, 6000, false)]
        public string ClientCmdtoggleVehicleMapToggle(string toggle)
            {
            if (toggle.AsBool())
                {
                ActionMap.pop("moveMap");
                ActionMap.push("vehicleMap");
                }
            else
                {
                ActionMap.push("moveMap");
                ActionMap.pop("vehicleMap");
                }
            return string.Empty;
            }

        // ----------------------------------------------------------------------------
        // Turret Support
        // ----------------------------------------------------------------------------

        // Call by the Turret class when a player mounts or unmounts it.
        // %turret = The turret that was mounted
        // %player = The player doing the mounting
        // %mounted = True if the turret was mounted, false if it was unmounted
        [Torque_Decorations.TorqueCallBack("", "", "turretMountCallback", "(%turret, %player, %mounted)", 3, 6000, false)]
        public string TurretMountCallback(string turret, string player, string mounted)
            {
            if (mounted.AsBool())
                ActionMap.push("turretMap");
            else
                ActionMap.pop("turretMap");
            return string.Empty;
            }


        [Torque_Decorations.TorqueCallBack("", "", "initBaseClient", "", 0, 70000, false)]
        public void initBaseClient()
            {
            /*We are going to inject 
             * message              50000
             * mission              51000
             * missionDownload      52000
             * actionMap            53000
             * rendermanager        54000
             * lighting             55000
             */
            dnt.DoScriptInjection(ScriptType.Client, 50000, 55999);
            initRenderManager();
            initLightingSystems();
            }

        /// A helper function which will return the ghosted client object
        /// from a server object when connected to a local server.
        /// 
        [Torque_Decorations.TorqueCallBack("", "", "serverToClientObject", "%serverObject", 1, 70100, false)]
        public string serverToClientObject(string serverObject)
            {
            console.Eval(@"assert( isObject( LocalClientConnection ), ""serverToClientObject() - No local client connection found!"" );");
            console.Eval(@"assert( isObject( ServerConnection ), ""serverToClientObject() - No server connection found!"" );      ");

            int ghostID = NetObject.getGhostID(serverObject);
            return ghostID == -1 ? "0" : console.Call("ServerConnection", "resolveGhostID", new[] {ghostID.AsString()});
            }

        //----------------------------------------------------------------------------
        // Debug commands
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "netSimulateLag", " %msDelay, %packetLossPercent", 2, 70200, false)]
        public void netSimulateLag(string msDelay, string packetLossPercent)
            {
            if (packetLossPercent == "")
                packetLossPercent = "0";

            console.commandToServer("NetSimulateLag", new[] {msDelay, packetLossPercent});
            }
        }
    }