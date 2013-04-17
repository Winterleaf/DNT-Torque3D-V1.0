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

using System.Globalization;
using WinterLeaf.Classes;
using WinterLeaf.Containers;
using WinterLeaf.Enums;

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    public partial class Main : TorqueScriptTemplate
        {
        //-----------------------------------------------------------------------------
        // Misc. server commands avialable to clients
        //-----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        // Debug commands
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "serverCmdNetSimulateLag", "( %client, %msDelay, %packetLossPercent )", 3, 12000, false)]
        public void ServerCmdNetSimulateLag(string client, string msDelay, string packetLossPercent)
            {
            if (console.GetVarBool(client + ".isAdmin"))
                NetConnection.setSimulatedNetParams(client, (float) (packetLossPercent.AsFloat()/100.0), msDelay.AsInt());
            }

        //----------------------------------------------------------------------------
        // Camera commands
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "serverCmdTogglePathCamera", "(%client, %val)", 2, 12000, false)]
        public void ServerCmdTogglePathCamera(string client, string val)
            {
            string control = val.AsBool() ? console.GetVarString(client + ".PathCamera") : console.GetVarString(client + ".camera");

            GameConnection.setControlObject(client, control);

            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdToggleCamera", "(%client)", 1, 12000, false)]
        public void ServerCmdToggleCamera(string client)
            {
            string control;
            if (GameConnection.getControlObject(client).AsString() == console.GetVarString(string.Format("{0}.player", client)))
                {
                Camera.setVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0"));
                control = console.GetVarString(string.Format("{0}.camera", client));
                }
            else
                {
                ShapeBase.setVelocity(console.GetVarString(string.Format("{0}.player", client)), new Point3F("0 0 0"));
                control = console.GetVarString(string.Format("{0}.player", client));
                }
            GameConnection.setControlObject(client, control);
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorCameraPlayer", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorCameraPlayer(string client)
            {
            ShapeBase.setVelocity(console.GetVarString(string.Format("{0}.player", client)), new Point3F("0 0 0"));

            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.player", client)));
            GameConnection.setFirstPerson(client, true);
            console.SetVar("$isFirstPersonVar", "1");
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorCameraPlayerThird", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorCameraPlayerThird(string client)
            {
            ShapeBase.setVelocity(console.GetVarString(string.Format("{0}.player", client)), new Point3F("0 0 0 "));

            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.player", client)));
            GameConnection.setFirstPerson(client, false);
            console.SetVar("$isFirstPersonVar", "0");
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdDropPlayerAtCamera", "(%client)", 1, 12000, false)]
        public void ServerCmdDropPlayerAtCamera(string client)
            {
            // If the player is mounted to something (like a vehicle) drop that at the
            // camera instead. The player will remain mounted.
            string obj = SceneObject.getObjectMount(console.GetVarString(string.Format("{0}.player", client))).AsString();
            if (!console.isObject(obj))
                obj = console.GetVarString(client + ".player");
            TransformF tf = SceneObject.getTransform(console.GetVarString(string.Format("{0}.camera", client)));
            SceneObject.setTransform(obj, tf);
            ShapeBase.setVelocity(obj, new Point3F("0 0 0 "));
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.player", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdDropCameraAtPlayer", "(%client)", 1, 12000, false)]
        public void ServerCmdDropCameraAtPlayer(string client)
            {
            TransformF tf = SceneObject.getTransform(console.GetVarString(string.Format("{0}.player", client)));
            SceneObject.setTransform(console.GetVarString(string.Format("{0}.camera", client)), tf);
            Camera.setVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0 "));

            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdCycleCameraFlyType", "(%client)", 1, 12000, false)]
        public void ServerCmdCycleCameraFlyType(string client)
            {
            if (Camera.getMode(console.GetVarString(string.Format("{0}.camera", client))) != Camera__CameraMotionMode.FlyMode)
                return;
            if (console.GetVarBool(string.Format("{0}.camera.newtonMode", client)) == false)
                {
                console.SetVar(string.Format("{0}.camera.newtonMode", client), 1);
                console.SetVar(string.Format("{0}.camera.newtonRotation", client), 0);
                Camera.setVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0 "));
                }
            else if (console.GetVarBool(string.Format("{0}.camera.newtonRotation", client)) == false)
                {
                console.SetVar(string.Format("{0}.camera.newtonMode", client), 1);
                console.SetVar(string.Format("{0}.camera.newtonRotation", client), 1);
                Camera.setAngularVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0 "));
                }
            else
                {
                console.SetVar(string.Format("{0}.camera.newtonMode", client), 0);
                console.SetVar(string.Format("{0}.camera.newtonRotation", client), 0);
                }
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorCameraStandard", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorCameraStandard(string client)
            {
            Camera.setFlyMode(console.GetVarString(string.Format("{0}.camera", client)));
            console.SetVar(string.Format("{0}.camera.newtonMode", client), 1);
            console.SetVar(string.Format("{0}.camera.newtonRotation", client), 0);
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorCameraNewton", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorCameraNewton(string client)
            {
            Camera.setFlyMode(console.GetVarString(string.Format("{0}.camera", client)));
            console.SetVar(string.Format("{0}.camera.newtonMode", client), 1);
            console.SetVar(string.Format("{0}.camera.newtonRotation", client), 0);
            Camera.setVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0 "));
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorCameraNewtonDamped", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorCameraNewtonDamped(string client)
            {
            Camera.setFlyMode(console.GetVarString(string.Format("{0}.camera", client)));
            console.SetVar(string.Format("{0}.camera.newtonMode", client), 1);
            console.SetVar(string.Format("{0}.camera.newtonRotation", client), 1);
            Camera.setVelocity(console.GetVarString(string.Format("{0}.camera", client)), new Point3F("0 0 0 "));
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorOrbitCamera", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorOrbitCamera(string client)
            {
            Camera.setEditOrbitMode(console.GetVarString(string.Format("{0}.camera", client)));
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSetEditorFlyCamera", "(%client)", 1, 12000, false)]
        public void ServerCmdSetEditorFlyCamera(string client)
            {
            Camera.setFlyMode(console.GetVarString(string.Format("{0}.camera", client)));
            GameConnection.setControlObject(client, console.GetVarString(string.Format("{0}.camera", client)));
            console.Call("clientCmdSyncEditorGui");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdEditorOrbitCameraSelectChange", "(%client, %size, %center)", 3, 12000, false)]
        public void ServerCmdEditorOrbitCameraSelectChange(string client, string size, string center)
            {
            if (size.AsInt() > 0)
                {
                Camera.setValidEditOrbitPoint(console.GetVarString(string.Format("{0}.camera", client)), true);
                Camera.setEditOrbitPoint(console.GetVarString(string.Format("{0}.camera", client)), new Point3F(center));
                }
            else
                {
                Camera.setValidEditOrbitPoint(console.GetVarString(string.Format("{0}.camera", client)), false);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdEditorCameraAutoFit", "(%client, %radius)", 2, 12000, false)]
        public void ServerCmdEditorCameraAutoFit(string client, string radius)
            {
            Camera.autoFitRadius(console.GetVarString(string.Format("{0}.camera", client)), radius.AsFloat());
            GameConnection.setControlObject(client, console.GetVarString(client + ".camera"));
            console.Call("clientCmdSyncEditorGui");
            }

        //----------------------------------------------------------------------------
        // Server admin
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSAD", "( %client, %password )", 2, 12000, false)]
        public void ServerCmdSAD(string client, string password)
            {
            if (password == "" || password != console.GetVarString("$Pref::Server::AdminPassword"))
                return;
            console.SetVar(string.Format("{0}.isAdmin", client), true);
            console.SetVar(string.Format("{0}.isSuperAdmin", client), true);
            string name = console.getTaggedString(console.GetVarString(string.Format("{0}.playerName", client)));
            MessageAll("MsgAdminForce", console.ColorEncode(string.Format(@"\c2{0} has become Admin by force.", name)), client);
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSADSetPassword", "( %client, %password )", 2, 12000, false)]
        public void ServerCmdSADSetPassword(string client, string password)
            {
            if (console.GetVarBool(string.Format("{0}.isSuperAdmin", client)))
                console.SetVar("$Pref::Server::AdminPassword", password);
            }

        //----------------------------------------------------------------------------
        // Server chat message handlers
        //----------------------------------------------------------------------------
        [Torque_Decorations.TorqueCallBack("", "", "serverCmdTeamMessageSent", "(%client, %text)", 2, 12000, false)]
        public void ServerCmdTeamMessageSent(string client, string text)
            {
            if (text.Trim().Length >= console.GetVarInt("$Pref::Server::MaxChatLen"))
                text = text.Substring(0, console.GetVarInt("$Pref::Server::MaxChatLen"));
            ChatMessageTeam(client, console.GetVarString(string.Format("{0}.team", client)), console.ColorEncode(@"\c3%1: %2"), console.GetVarString(string.Format("{0}.playerName", client)), text);
            }

        [Torque_Decorations.TorqueCallBack("", "", "ServerCmdMessageSent", "(%client, %text)", 2, 12000, false)]
        public void ServerCmdMessageSent(string client, string text)
            {
            if (text.Trim().Length >= console.GetVarInt("$Pref::Server::MaxChatLen"))
                text = text.Substring(0, console.GetVarInt("$Pref::Server::MaxChatLen"));
            ChatMessageAll(client, console.ColorEncode(@"\c4%1: %2"), console.GetVarString(string.Format("{0}.playerName", client)), text);
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdSuicide", "(%client)", 1, 100, false)]
        public void ServerCmdSuicide(string client)
            {
            if (console.isObject(client))
                PlayerKill(console.GetVarString(client + ".player"), "Suicide");
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdPlayCel", "(%client,%anim)", 2, 100, false)]
        public void ServerCmdPlayCel(string client, string anim)
            {
            if (console.isObject(client))
                PlayerPlayCelAnimation(console.GetVarString(client + ".player"), anim);
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdTestAnimation", "(%client,%anim)", 2, 100, false)]
        public void ServerCmdTestAnimation(string client, string anim)
            {
            if (console.isObject(client))
                console.Call(string.Format("{0}.player", client), "playTestAnimation", new[] {anim});
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdPlayDeath", "(%client)", 1, 100, false)]
        public void ServerCmdPlayDeath(string client, string anim)
            {
            if (console.isObject(client))
                PlayerPlayDeathAnimation(console.GetVarString(client + ".player"));
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdThrow", "(%client, %data))", 2, 100, false)]
        public void ServerCmdThrow(string client, string data)
            {
            string player = console.GetVarString(string.Format("{0}.player", client));
            if (!console.isObject(player) || (Player.getState(player) == "Dead") || !Game_Running)
                return;
            switch (data)
                {
                    case "Weapon":
                        {
                        string mountedimage = ShapeBase.getMountedImage(player, int.Parse(console.GetVarString("$WeaponSlot"))).ToString(CultureInfo.InvariantCulture);
                        string item;
                        if (mountedimage == "0")
                            item = string.Empty;
                        else
                            {
                            item = console.GetVarString(string.Format("{0}.item", mountedimage));
                            ShapeBaseShapeBaseThrow(player, item);
                            }
                        }
                        break;
                    case "Ammo":
                        {
                        string mountedimage = ShapeBase.getMountedImage(player, int.Parse(console.GetVarString("$WeaponSlot"))).ToString(CultureInfo.InvariantCulture);
                        string item;
                        if (mountedimage == "0")
                            item = string.Empty;
                        else
                            {
                            item = mountedimage;
                            if (console.GetVarString(string.Format("{0}.ammo", item)) != "")
                                ShapeBaseShapeBaseThrow(player, console.GetVarString(item + ".ammo"));
                            }
                        }
                        break;
                    default:
                        if (ShapeBaseShapeBaseHasInventory(client, data))
                            ShapeBaseShapeBaseThrow(player, data);
                        break;
                }
            }


        [Torque_Decorations.TorqueCallBack("", "", "serverCmdCycleWeapon", "(%client, %direction)", 2, 100, false)]
        public void ServerCmdCycleWeapon(string client, string direction)
            {
            ShapeBaseCycleWeapon(GameConnection.getControlObject(client).AsString(), direction);
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdUnmountWeapon", "(%client)", 1, 100, false)]
        public void ServerCmdUnmountWeapon(string client)
            {
            ShapeBase.unmountImage(client, console.GetVarInt("$WeaponSlot"));
            }

        [Torque_Decorations.TorqueCallBack("", "", "serverCmdReloadWeapon", "(%client)", 1, 100, false)]
        public void ServerCmdReloadWeapon(string client)
            {
            string player = GameConnection.getControlObject(client).AsString();

            string image = ShapeBase.getMountedImage(player, int.Parse("$WeaponSlot")).AsString();

            if (ShapeBaseShapeBaseGetInventory(player, console.GetVarString(string.Format("{0}.ammo", image))) == console.GetVarInt(string.Format("{0}.ammo.maxInventory", image)))
                {
                return;
                }

            if (int.Parse("0" + image) > 0)
                WeaponImageClearAmmoClip(player, player, WeaponSlot.AsString());
            }
        }
    }