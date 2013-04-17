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

using WinterLeaf.Classes;
using WinterLeaf.Containers;
using WinterLeaf.Enums;

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Client
    {
    public partial class Main : TorqueScriptTemplate
        {
        //-----------------------------------------------------------------------------
        // Torque
        // Copyright GarageGames, LLC 2011
        //-----------------------------------------------------------------------------

        // Game specific audio descriptions. Always declare SFXDescription's (the type of sound)
        // before SFXProfile's (the sound itself) when creating new ones


        private int movementSpeed = 1;

        [Torque_Decorations.TorqueCallBack("", "", "init_Default_Bind", "()", 0, 11000, true)]
        public void initDefaultBind()
            {
            if (console.isObject("moveMap"))
                console.Call("moveMap", "delete");


            new Torque_Class_Helper("ActionMap", "moveMap").Create(m_ts);

            if (console.GetVarString("$Player::CurrentFOV") == "")
                console.SetVar("$Player::CurrentFOV", console.GetVarFloat("$pref::Player::DefaultFOV")/(float) 2.0);

            console.SetVar("$MFDebugRenderMode", 0);

            if (console.isObject("vehicleMap"))
                console.Call("vehicleMap", "delete");
            new Torque_Class_Helper("ActionMap", "vehicleMap").Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "", "escapeFromGame", "()", 0, 11001, false)]
        public string escapeFromGame()
            {
            console.Call("MessageBoxYesNo", console.GetVarString("$Server::ServerType") == "SinglePlayer" ? new[] {"Exit", "Exit from this Mission?", "disconnect();", ""} : new[] {"Disconnect", "Disconnect from the server?", "disconnect();", ""});
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "showPlayerList", "(val)", 1, 11001, false)]
        public string showPlayerList(string val)
            {
            if (val.AsBool())
                PlayerListGuiToggle("PlayerListGui");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "showControlsHelp", "(val)", 1, 11001, false)]
        public string showControlsHelp(string val)
            {
            if (val.AsBool())
                console.Call("ControlsHelpDlg", "toggle");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "hideHUDs", "(val)", 1, 11001, false)]
        public string hideHUDs(string val)
            {
            if (val.AsBool())
                console.Call("HudlessPlayGui", "toggle");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "doScreenShotHudless", "(val)", 1, 11001, false)]
        public void doScreenShotHudless(string val)
            {
            if (val.AsBool())
                {
                GuiCanvas.setContent("canvas", "HudlessPlayGui");
                Util._schedule("10", "0", "doScreenShot", val);
                }
            else
                {
                GuiCanvas.setContent("canvas", "PlayGui");
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "setSpeed", "(Speed)", 1, 11001, false)]
        public void setSpeed(string speed)
            {
            if (speed.AsBool())
                movementSpeed = speed.AsInt();
            }

        [Torque_Decorations.TorqueCallBack("", "", "moveleft", "(val)", 1, 11001, false)]
        public void moveleft(string val)
            {
            console.SetVar("$mvLeftAction", val.AsInt()*movementSpeed);
            }

        [Torque_Decorations.TorqueCallBack("", "", "moveright", "(val)", 1, 11001, false)]
        public void moveright(string val)
            {
            console.SetVar("$mvRightAction", val.AsInt()*movementSpeed);
            }


        [Torque_Decorations.TorqueCallBack("", "", "moveforward", "(val)", 1, 11001, false)]
        public void moveforward(string val)
            {
            console.SetVar("$mvForwardAction", val.AsInt()*movementSpeed);
            }


        [Torque_Decorations.TorqueCallBack("", "", "movebackward", "(val)", 1, 11001, false)]
        public void movebackward(string val)
            {
            console.SetVar("$mvBackwardAction", val.AsInt()*movementSpeed);
            }

        [Torque_Decorations.TorqueCallBack("", "", "moveup", "(val)", 1, 11001, false)]
        public void moveup(string val)
            {
            string obj = GameConnection.getControlObject("ServerConnection");
            if (console.isInNamespaceHierarchy(obj, "Camera"))
                console.SetVar("$mvUpAction", val.AsInt()*movementSpeed);
            }

        [Torque_Decorations.TorqueCallBack("", "", "movedown", "(val)", 1, 11001, false)]
        public void movedown(string val)
            {
            string obj = GameConnection.getControlObject("ServerConnection");
            if (console.isInNamespaceHierarchy(obj, "Camera"))
                console.SetVar("$mvDownAction", val.AsInt()*movementSpeed);
            }

        [Torque_Decorations.TorqueCallBack("", "", "turnLeft", "(val)", 1, 11001, false)]
        public void turnLeft(string val)
            {
            console.SetVar("$mvYawRightSpeed", val.AsBool() ? console.GetVarInt("$Pref::Input::KeyboardTurnSpeed") : 0);
            }

        [Torque_Decorations.TorqueCallBack("", "", "turnRight", "(val)", 1, 11001, false)]
        public void turnRight(string val)
            {
            console.SetVar("$mvYawLeftSpeed", val.AsBool() ? console.GetVarInt("$Pref::Input::KeyboardTurnSpeed") : 0);
            }


        [Torque_Decorations.TorqueCallBack("", "", "panUp", "(val)", 1, 11001, false)]
        public void panUp(string val)
            {
            console.SetVar("$mvPitchDownSpeed", val.AsBool() ? console.GetVarInt("$Pref::Input::KeyboardTurnSpeed") : 0);
            }

        [Torque_Decorations.TorqueCallBack("", "", "panDown", "(val)", 1, 11001, false)]
        public void panDown(string val)
            {
            console.SetVar("$mvPitchUpSpeed", val.AsBool() ? console.GetVarInt("$Pref::Input::KeyboardTurnSpeed") : 0);
            }

        [Torque_Decorations.TorqueCallBack("", "", "getMouseAdjustAmount", "(val)", 1, 11001, false)]
        public string getMouseAdjustAmount(string val)
            {
            return ((val.AsDouble()*(console.GetVarFloat("$cameraFov")/90.0)*0.01)*console.GetVarFloat("$pref::Input::LinkMouseSensitivity")).AsString();
            }

        [Torque_Decorations.TorqueCallBack("", "", "getGamepadAdjustAmount", "(val)", 1, 11001, false)]
        public string getGamepadAdjustAmount(string val)
            {
            return ((val.AsFloat()*(console.GetVarFloat("$cameraFov")/90)*0.01)*10.0).AsString();
            }

        [Torque_Decorations.TorqueCallBack("", "", "yaw", "(val)", 1, 11001, false)]
        public void yaw(string val)
            {
            float yawAdj = getMouseAdjustAmount(val).AsFloat();
            if (GameConnection.isControlObjectRotDampedCamera("ServerConnection"))
                {
                yawAdj = Util.mClamp(yawAdj, (-Util.m2Pi() + (float) 0.01), (Util.m2Pi() - (float) 0.01));
                yawAdj *= (float) 0.5;
                }

            console.SetVar("$mvYaw", console.GetVarFloat("$mvYaw") + yawAdj);
            }

        [Torque_Decorations.TorqueCallBack("", "", "pitch", "(val)", 1, 11001, false)]
        public void pitch(string val)
            {
            float pitchAdj = getMouseAdjustAmount(val).AsFloat();
            if (GameConnection.isControlObjectRotDampedCamera("ServerConnection"))
                {
                pitchAdj = Util.mClamp(pitchAdj, (-Util.m2Pi() + (float) 0.01), (Util.m2Pi() - (float) 0.01));
                pitchAdj *= (float) 0.5;
                }

            console.SetVar("$mvPitch", console.GetVarFloat("$mvPitch") + pitchAdj);
            }

        [Torque_Decorations.TorqueCallBack("", "", "jump", "(val)", 1, 11001, false)]
        public void Jump(string val)
            {
            console.SetVar("$mvTriggerCount2", console.GetVarInt("$mvTriggerCount2") + 1);
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamePadMoveX", "(val)", 1, 11001, false)]
        public void gamePadMoveX(string val)
            {
            if (val.AsInt() > 0)
                {
                console.SetVar("$mvRightAction", (val.AsDouble()*movementSpeed).AsString());
                console.SetVar("$mvLeftAction", 0);
                }
            else
                {
                console.SetVar("$mvRightAction", 0);
                console.SetVar("$mvLeftAction", (-val.AsDouble()*movementSpeed).AsString());
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamePadMoveY", "(val)", 1, 11001, false)]
        public void gamePadMoveY(string val)
            {
            if (val.AsInt() > 0)
                {
                console.SetVar("$mvForwardAction", val.AsDouble()*movementSpeed);
                console.SetVar("$mvBackwardAction", 0);
                }
            else
                {
                console.SetVar("$mvForwardAction", 0);
                console.SetVar("$mvBackwardAction", -val.AsDouble()*movementSpeed);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamepadYaw", "(val)", 1, 11001, false)]
        public void gamepadYaw(string val)
            {
            float yawAdj = getGamepadAdjustAmount(val).AsFloat();
            if (GameConnection.isControlObjectRotDampedCamera("ServerConnection"))
                {
                yawAdj = Util.mClamp(yawAdj, (float) (-Util.m2Pi() + 0.01), (float) (Util.m2Pi() - 0.01));
                yawAdj *= (float) 0.5;
                }

            if (yawAdj > 0)
                {
                console.SetVar("$mvYawLeftSpeed", yawAdj);
                console.SetVar("$mvYawRightSpeed", 0);
                }
            else
                {
                console.SetVar("$mvYawLeftSpeed", 0);
                console.SetVar("$mvYawRightSpeed", -yawAdj);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamepadPitch", "(val)", 1, 11001, false)]
        public void gamepadPitch(string val)
            {
            float pitchAdj = getGamepadAdjustAmount(val).AsFloat();
            if (GameConnection.isControlObjectRotDampedCamera("ServerConnection"))
                {
                pitchAdj = Util.mClamp(pitchAdj, (float) (-Util.m2Pi() + 0.01), (float) (Util.m2Pi() - 0.01));
                pitchAdj *= (float) 0.5;
                }
            if (pitchAdj > 0)
                {
                console.SetVar("$mvPitchDownSpeed", pitchAdj);
                console.SetVar("$mvPitchUpSpeed", 0);
                }
            else
                {
                console.SetVar("$mvPitchDownSpeed", 0);
                console.SetVar("$mvPitchUpSpeed", -pitchAdj);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "doCrouch", "(val)", 1, 11001, false)]
        public void doCrouch(string val)
            {
            console.SetVar("$mvTriggerCount3", console.GetVarString("$mvTriggerCount3").AsInt() + 1);
            }

        [Torque_Decorations.TorqueCallBack("", "", "doSprint", "(val)", 1, 11001, false)]
        public void doSprint(string val)
            {
            console.SetVar("$mvTriggerCount5", console.GetVarString("$mvTriggerCount5").AsInt() + 1);
            }

        [Torque_Decorations.TorqueCallBack("", "", "mouseFire", "(val)", 1, 11001, false)]
        public void mouseFire(string val)
            {
            console.SetVar("$mvTriggerCount0", console.GetVarString("$mvTriggerCount0").AsInt() + 1);
            }

        [Torque_Decorations.TorqueCallBack("", "", "altTrigger", "(val)", 1, 11001, false)]
        public void altTrigger(string val)
            {
            console.SetVar("$mvTriggerCount1", console.GetVarString("$mvTriggerCount1").AsInt() + 1);
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamepadFire", "(val)", 1, 11001, false)]
        public void gamepadFire(string val)
            {
            if (val.AsDouble() > .1 && !console.GetVarBool("$gamepadFireTriggered"))
                {
                console.SetVar("$gamepadFireTriggered", true);
                console.SetVar("$mvTriggerCount0", console.GetVarString("$mvTriggerCount0").AsInt() + 1);
                }
            else
                {
                console.SetVar("$gamepadFireTriggered", false);
                console.SetVar("$mvTriggerCount0", console.GetVarString("$mvTriggerCount0").AsInt() + 1);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "gamepadAltTrigger", "(val)", 1, 11001, false)]
        public void gamepadAltTrigger(string val)
            {
            if (val.AsDouble() > .1 && !console.GetVarBool("$gamepadFireTriggered"))
                {
                console.SetVar("$gamepadAltTriggerTriggered", true);
                console.SetVar("$mvTriggerCount1", console.GetVarString("$mvTriggerCount1").AsInt() + 1);
                }
            else
                {
                console.SetVar("$gamepadAltTriggerTriggered", false);
                console.SetVar("$mvTriggerCount1", console.GetVarString("$mvTriggerCount1").AsInt() + 1);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleZoomFOV", "(val)", 0, 11001, false)]
        public void toggleZoomFOV()
            {
            float cfov = console.GetVarFloat("$Player::CurrentFOV")/(float) 2.0;

            console.SetVar("$Player::CurrentFOV", cfov);

            if (cfov < 5)
                resetCurrentFOV();

            if (console.GetVarBool("ServerConnection.zoomed"))

                console.Call("setFov", new[] {cfov.AsString()});
            else
                console.Call("setFov", new[] {console.GetVarString("ServerConnection.getControlCameraDefaultFov()")});
            }

        [Torque_Decorations.TorqueCallBack("", "", "resetCurrentFOV", "()", 0, 11001, false)]
        public void resetCurrentFOV()
            {
            console.SetVar("$Player::CurrentFOV", console.GetVarFloat("ServerConnection.getControlCameraDefaultFov")/(float) 2.0);
            }

        [Torque_Decorations.TorqueCallBack("", "", "turnOffZoom", "()", 0, 11001, false)]
        public void turnOffZoom()
            {
            console.SetVar("ServerConnection.zoomed", false);
            console.Call("setFov", new[] {GameConnection.getControlCameraDefaultFov("ServerConnection").AsString()});
            GuiControl.setVisible("Reticle", true);
            GuiControl.setVisible("zoomReticle", false);


            ppOptionsUpdateDOFSettings();
            }

        [Torque_Decorations.TorqueCallBack("", "", "setZoomFOV", "()", 1, 11001, false)]
        public void setZoomFOV(string val)
            {
            if (val.AsBool())
                toggleZoomFOV();
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleZoom", "()", 1, 11001, false)]
        public void toggleZoom(string val)
            {
            if (val.AsBool())
                {
                console.SetVar("ServerConnection.zoomed", true);
                console.Call("setFov", new[] {console.GetVarString("$Player::CurrentFOV")});
                GuiControl.setVisible("Reticle", false);
                GuiControl.setVisible("zoomReticle", true);

                DOFPostEffectsetAutoFocus("DOFPostEffect", true);


                DOFPostEffectsetFocusParams("DOFPostEffect", 0.5f, 0.5f, 50f, 500f, -5f, 5f);

                console.Call("DOFPostEffect", "enabled");
                }
            else
                turnOffZoom();
            }

        [Torque_Decorations.TorqueCallBack("", "", "mouseButtonZoom", "(val)", 1, 11001, false)]
        public void mouseButtonZoom(string val)
            {
            toggleZoom(val);
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleFreeLook", "(val)", 1, 11001, false)]
        public void toggleFreeLook(string val)
            {
            console.SetVar("$mvFreeLook", val.AsBool());
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleFirstPerson", "(val)", 1, 11001, false)]
        public void toggleFirstPerson(string val)
            {
            if (val.AsBool())
                GameConnection.setFirstPerson("ServerConnection", !GameConnection.isFirstPerson("ServerConnection"));
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleCamera", "(val)", 1, 11001, false)]
        public void toggleCamera(string val)
            {
            if (val.AsBool())
                console.commandToServer("ToggleCamera");
            }

        [Torque_Decorations.TorqueCallBack("", "", "unmountWeapon", "(val)", 1, 11001, false)]
        public void unmountWeapon(string val)
            {
            if (val.AsBool())
                console.commandToServer("unmountWeapon");
            }

        [Torque_Decorations.TorqueCallBack("", "", "throwWeapon", "(val)", 1, 11001, false)]
        public void throwWeapon(string val)
            {
            if (val.AsBool())
                console.commandToServer("Throw", new[] {"Weapon"});
            }

        [Torque_Decorations.TorqueCallBack("", "", "tossAmmo", "(val)", 1, 11001, false)]
        public void tossAmmo(string val)
            {
            if (val.AsBool())
                console.commandToServer("Throw", new[] {"Ammo"});
            }


        [Torque_Decorations.TorqueCallBack("", "", "nextWeapon", "(val)", 1, 11001, false)]
        public void nextWeapon(string val)
            {
            if (val.AsBool())
                console.commandToServer("cycleWeapon", new[] {"next"});
            }

        [Torque_Decorations.TorqueCallBack("", "", "prevWeapon", "(val)", 1, 11001, false)]
        public void prevWeapon(string val)
            {
            if (val.AsBool())
                console.commandToServer("cycleWeapon", new[] {"prev"});
            }

        [Torque_Decorations.TorqueCallBack("", "", "mouseWheelWeaponCycle", "(val)", 1, 11001, false)]
        public void mouseWheelWeaponCycle(string val)
            {
            if (val.AsDouble() < 0)
                console.commandToServer("cycleWeapon", new[] {"next"});
            else if (val.AsDouble() > 0)
                console.commandToServer("cycleWeapon", new[] {"prev"});
            }

        [Torque_Decorations.TorqueCallBack("", "", "pageMessageHudUp", "(val)", 1, 11001, false)]
        public void pageMessageHudUp(string val)
            {
            if (val.AsBool())
                PageUpMessageHud();
            }

        [Torque_Decorations.TorqueCallBack("", "", "pageMessageHudDown", "(val)", 1, 11001, false)]
        public void pageMessageHudDown(string val)
            {
            if (val.AsBool())
                PageDownMessageHud();
            }

        [Torque_Decorations.TorqueCallBack("", "", "resizeMessageHud", "(val)", 1, 11001, false)]
        public void resizeMessageHud(string val)
            {
            if (val.AsBool())
                CycleMessageHudSize();
            }

        [Torque_Decorations.TorqueCallBack("", "", "startRecordingDemo", "(val)", 1, 11001, false)]
        public void startRecordingDemo(string val)
            {
            if (val.AsBool())
                StartDemoRecord();
            }

        [Torque_Decorations.TorqueCallBack("", "", "stopRecordingDemo", "(val)", 1, 11001, false)]
        public void stopRecordingDemo(string val)
            {
            if (val.AsBool())
                StopDemoRecord();
            }

        [Torque_Decorations.TorqueCallBack("", "", "dropCameraAtPlayer", "(val)", 1, 11001, false)]
        public void dropCameraAtPlayer(string val)
            {
            if (val.AsBool())
                console.commandToServer("dropCameraAtPlayer");
            }

        [Torque_Decorations.TorqueCallBack("", "", "dropPlayerAtCamera", "(val)", 1, 11001, false)]
        public void dropPlayerAtCamera(string val)
            {
            if (val.AsBool())
                console.commandToServer("DropPlayerAtCamera");
            }

        [Torque_Decorations.TorqueCallBack("", "", "bringUpOptions", "(val)", 1, 11001, false)]
        public void bringUpOptions(string val)
            {
            if (val.AsBool())
                {
                //Util._lockMouse("false");
                showCursor();
                GuiCanvas.pushDialog("Canvas", "OptionsDlg");
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "cycleDebugRenderMode", "(val)", 1, 11001, false)]
        public void cycleDebugRenderMode(string val)
            {
            if (val.AsBool())
                return;
            console.SetVar("$MFDebugRenderMode", console.GetVarInt("$MFDebugRenderMode") + 1);

            if (console.GetVarInt("$MFDebugRenderMode") > 16)
                console.SetVar("$MFDebugRenderMode", 0);
            if (console.GetVarInt("$MFDebugRenderMode") == 15)
                console.SetVar("$MFDebugRenderMode", 16);

            Util.setInteriorRenderMode(console.GetVarInt("$MFDebugRenderMode"));

            if (!console.isObject("ChatHud"))
                return;
            string message = "Setting Interior debug render mode to ";
            string debugMode = "Unknown";
            switch (console.GetVarInt("$MFDebugRenderMode"))
                {
                    case 0:
                        debugMode = "NormalRender";
                        break;
                    case 1:
                        debugMode = "NormalRenderLines";
                        break;
                    case 2:
                        debugMode = "ShowDetail";
                        break;
                    case 3:
                        debugMode = "ShowAmbiguous";
                        break;
                    case 4:
                        debugMode = "ShowOrphan";
                        break;
                    case 5:
                        debugMode = "ShowLightmaps";
                        break;
                    case 6:
                        debugMode = "ShowTexturesOnly";
                        break;
                    case 7:
                        debugMode = "ShowPortalZones";
                        break;
                    case 8:
                        debugMode = "ShowOutsideVisible";
                        break;
                    case 9:
                        debugMode = "ShowCollisionFans";
                        break;
                    case 10:
                        debugMode = "ShowStrips";
                        break;
                    case 11:
                        debugMode = "ShowNullSurfaces";
                        break;
                    case 12:
                        debugMode = "ShowLargeTextures";
                        break;
                    case 13:
                        debugMode = "ShowHullSurfaces";
                        break;
                    case 14:
                        debugMode = "ShowVehicleHullSurfaces";
                        break;
                    case 15:
                        debugMode = "";
                        break;
                    case 16:
                        debugMode = "ShowDetailLevel";
                        break;
                }
            ChatHudAddLine("ChatHud", message + debugMode);
            }

        [Torque_Decorations.TorqueCallBack("", "", "doProfile", "(val)", 1, 11001, false)]
        public void doProfile(string val)
            {
            if (val.AsBool())
                {
                console.print("Starting profile session...");
                Util.profilerReset();
                Util.profilerEnable(true);
                }
            else
                {
                console.print("Ending profile session...");
                Util.profilerDumpToFile("profilerDumpToFile" + console.Call("getSimTime") + ".txt");
                Util.profilerEnable(false);
                }
            }

        [Torque_Decorations.TorqueCallBack("", "", "carjack", "()", 0, 11001, false)]
        public void carjack()
            {
            string player = GameConnection.getControlObject("LocalClientConnection");
            if (console.GetClassName(player) != "Player")
                return;
            Point3F eyeVec = ShapeBase.getEyeVector(player);

            Point3F startPos = ShapeBase.getEyePoint(player);

            Point3F endPos = startPos + eyeVec.vectorScale(1000);

            string target = Util.containerRayCast(startPos, endPos, (uint) SceneObjectTypesAsUint.VehicleObjectType, "", false);
            if (!target.AsBool())
                return;
            int mount = SceneObject.getMountNodeObject(target, 0);
            if (mount.AsBool() && console.GetClassName(mount.AsString()) == "AIPlayer")
                console.commandToServer("carUnmountObj", new[] {mount.AsString()});
            }

        [Torque_Decorations.TorqueCallBack("", "", "getOut", "()", 0, 11001, false)]
        public void getOut()
            {
            ActionMap.pop("vehicleMap");
            ActionMap.push("moveMap");
            console.commandToServer("dismountVehicle");
            }

        [Torque_Decorations.TorqueCallBack("", "", "brakeLights", "()", 0, 11001, false)]
        public void brakeLights()
            {
            console.commandToServer("toggleBrakeLights");
            }

        [Torque_Decorations.TorqueCallBack("", "", "brake", "(val)", 1, 11001, false)]
        public void brake(string val)
            {
            console.commandToServer("toggleBrakeLights");
            console.SetVar("$mvTriggerCount2", console.GetVarInt("$mvTriggerCount2") + 1);
            }


        [Torque_Decorations.TorqueCallBack("", "", "init_Default_Bind1", "()", 0, 11004, true)]
        public void init_Default_Bind()
            {
            ActionMap.bind("moveMap", "keyboard", "F2", "showPlayerList");
            ActionMap.bind("moveMap", "keyboard", "h", "showControlsHelp");
            ActionMap.bind("moveMap", "keyboard", "ctrl h", "hideHUDs");
            ActionMap.bind("moveMap", "keyboard", "alt p", "doScreenShotHudless");
            ActionMap.bind("moveMap", "keyboard", "a", "moveleft");
            ActionMap.bind("moveMap", "keyboard", "d", "moveright");
            ActionMap.bind("moveMap", "keyboard", "left", "moveleft");
            ActionMap.bind("moveMap", "keyboard", "right", "moveright");
            ActionMap.bind("moveMap", "keyboard", "w", "moveforward");
            ActionMap.bind("moveMap", "keyboard", "s", "movebackward");
            ActionMap.bind("moveMap", "keyboard", "up", "moveforward");
            ActionMap.bind("moveMap", "keyboard", "down", "movebackward");
            ActionMap.bind("moveMap", "keyboard", "e", "moveup");
            ActionMap.bind("moveMap", "keyboard", "c", "movedown");
            ActionMap.bind("moveMap", "keyboard", "space", "jump");
            ActionMap.bind("moveMap", "mouse", "xaxis", "yaw");
            ActionMap.bind("moveMap", "mouse", "yaxis", "pitch");
            ActionMap.bind("moveMap", "mouse", "button0", "mouseFire");
            ActionMap.bind("moveMap", "mouse", "button1", "mouseButtonZoom");
            ActionMap.bind("moveMap", "mouse", "zaxis", "mouseWheelWeaponCycle");
            ActionMap.bind("moveMap", "gamepad", "thumbrx", "D", "-0.23 0.23", "gamepadYaw");
            ActionMap.bind("moveMap", "gamepad", "thumbry", "D", "-0.23 0.23", "gamepadPitch");
            ActionMap.bind("moveMap", "gamepad", "thumblx", "D", "-0.23 0.23", "gamePadMoveX");
            ActionMap.bind("moveMap", "gamepad", "thumbly", "D", "-0.23 0.23", "gamePadMoveY");
            ActionMap.bind("moveMap", "gamepad", "btn_a", "jump");
            ActionMap.bind("moveMap", "keyboard", "lcontrol", "doCrouch");
            ActionMap.bind("moveMap", "gamepad", "btn_b", "doCrouch");
            ActionMap.bind("moveMap", "keyboard", "lshift", "doSprint");
            ActionMap.bind("moveMap", "gamepad", "triggerr", "gamepadFire");
            ActionMap.bind("moveMap", "gamepad", "triggerl", "gamepadAltTrigger");
            ActionMap.bind("moveMap", "keyboard", "f", "setZoomFOV");
            ActionMap.bind("moveMap", "keyboard", "z", "toggleZoom");
            ActionMap.bind("moveMap", "keyboard", "v", "toggleFreeLook");
            ActionMap.bind("moveMap", "keyboard", "tab", "toggleFirstPerson");
            ActionMap.bind("moveMap", "keyboard", "alt c", "toggleCamera");
            ActionMap.bind("moveMap", "gamepad", "btn_start", "toggleCamera");
            ActionMap.bind("moveMap", "gamepad", "btn_x", "toggleFirstPerson");
            ActionMap.bind("moveMap", "keyboard", "0", "unmountWeapon");
            ActionMap.bind("moveMap", "keyboard", "alt w", "throwWeapon");
            ActionMap.bind("moveMap", "keyboard", "alt a", "tossAmmo");
            ActionMap.bind("moveMap", "keyboard", "q", "nextWeapon");
            ActionMap.bind("moveMap", "keyboard", "ctrl q", "prevWeapon");
            ActionMap.bind("moveMap", "keyboard", "u", "toggleMessageHud");
            ActionMap.bind("moveMap", "keyboard", "pageUp", "pageMessageHudUp");
            ActionMap.bind("moveMap", "keyboard", "pageDown", "pageMessageHudDown");
            ActionMap.bind("moveMap", "keyboard", "p", "resizeMessageHud");
            ActionMap.bind("moveMap", "keyboard", "F3", "startRecordingDemo");
            ActionMap.bind("moveMap", "keyboard", "F4", "stopRecordingDemo");
            ActionMap.bind("moveMap", "keyboard", "F8", "dropCameraAtPlayer");
            ActionMap.bind("moveMap", "keyboard", "F7", "dropPlayerAtCamera");
            ActionMap.bindCmd("moveMap", "keyboard", "escape", "", "handleEscape();");
            ActionMap.bindCmd("moveMap", "gamepad", "btn_back", "disconnect();", "");
            ActionMap.bindCmd("moveMap", "gamepad", "dpadl", "toggleLightColorViz();", "");
            ActionMap.bindCmd("moveMap", "gamepad", "dpadu", "toggleDepthViz();", "");
            ActionMap.bindCmd("moveMap", "gamepad", "dpadd", "toggleNormalsViz();", "");
            ActionMap.bindCmd("moveMap", "gamepad", "dpadr", "toggleLightSpecularViz();", "");
            ActionMap.bindCmd("moveMap", "keyboard", "ctrl k", "commandToServer('suicide');", "");
            ActionMap.bindCmd("moveMap", "keyboard", "1", "commandToServer('use',\"Ryder\");", "");
            ActionMap.bindCmd("moveMap", "keyboard", "2", "commandToServer('use',\"Lurker\");", "");
            ActionMap.bindCmd("moveMap", "keyboard", "3", "commandToServer('use',\"LurkerGrenadeLauncher\");", "");
            ActionMap.bindCmd("moveMap", "keyboard", "4", "commandToServer('use',\"ProxMine\");", "");
            ActionMap.bindCmd("moveMap", "keyboard", "5", "commandToServer('use',\"DeployableTurret\");", "");
            ActionMap.bindCmd("moveMap", "keyboard", "r", "commandToServer('reloadWeapon');", "");
            ActionMap.bindCmd("moveMap", "keyboard", "n", "toggleNetGraph();", "");
            ActionMap.bindCmd("moveMap", "keyboard", "ctrl z", "carjack();", "");
            ActionMap.bind("GlobalActionMap", "keyboard", "ctrl o", "bringUpOptions");
            ActionMap.bind("GlobalActionMap", "keyboard", "F9", "cycleDebugRenderMode");
            ActionMap.bind("GlobalActionMap", "keyboard", "ctrl F3", "doProfile");
            ActionMap.bind("GlobalActionMap", "keyboard", "tilde", "toggleConsole");
            ActionMap.bindCmd("GlobalActionMap", "keyboard", "alt k", "cls();", "");
            ActionMap.bindCmd("GlobalActionMap", "keyboard", "alt enter", "", "Canvas.attemptFullscreenToggle();");
            ActionMap.bindCmd("GlobalActionMap", "keyboard", "F1", "", "contextHelp();");
            ActionMap.bindCmd("vehicleMap", "keyboard", "ctrl x", "commandToServer(\'flipCar\');", "");
            ActionMap.bindCmd("vehicleMap", "keyboard", "ctrl f", "getout();", "");
            ActionMap.bindCmd("vehicleMap", "keyboard", "l", "brakeLights();", "");
            ActionMap.bindCmd("vehicleMap", "keyboard", "escape", "", "handleEscape();");
            ActionMap.bind("vehicleMap", "keyboard", "w", "moveforward");
            ActionMap.bind("vehicleMap", "keyboard", "s", "movebackward");
            ActionMap.bind("vehicleMap", "keyboard", "up", "moveforward");
            ActionMap.bind("vehicleMap", "keyboard", "down", "movebackward");
            ActionMap.bind("vehicleMap", "mouse", "xaxis", "yaw");
            ActionMap.bind("vehicleMap", "mouse", "yaxis", "pitch");
            ActionMap.bind("vehicleMap", "mouse", "button0", "mouseFire");
            ActionMap.bind("vehicleMap", "mouse", "button1", "altTrigger");
            ActionMap.bind("vehicleMap", "keyboard", "space", "brake");
            ActionMap.bind("vehicleMap", "keyboard", "h", "showControlsHelp");
            ActionMap.bind("vehicleMap", "keyboard", "v", "toggleFreeLook");
            ActionMap.bind("vehicleMap", "keyboard", "alt c", "toggleCamera");
            }
        }
    }