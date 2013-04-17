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

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WinterLeaf;

#endregion

namespace DNT_FPS_Demo
    {
    internal static class Program
        {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        /// 
        private static dnTorque dnt_torque;

        [STAThread]
        private static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new main_window());
            //If we are running dedicated, there is no reason to show a form.
            /*DialogResult result = MessageBox.Show("Launch Dedicated", "Dedicated?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                {
                dnt_torque = new dnTorque(IntPtr.Zero);
#if DEBUG
                dnt_torque.InitializeTorque(new[] {"-dedicated", "-mission", @"levels/Deathball_Desert.mis"}, "DNT_FPS_Demo_Game_Dll.Scripts.Server.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Client.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Main", "", "", "", Path.GetDirectoryName(Application.ExecutablePath) + "\\", @"DNT FPS Demo Game Dll.dll", "FPS Example_DEBUG.dll");
#else
                dnt_torque.InitializeTorque(new[] {"-dedicated", "-mission", @"levels/Deathball_Desert.mis"}, "DNT_FPS_Demo_Game_Dll.Scripts.Server.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Client.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Main", "", "", "", Path.GetDirectoryName(Application.ExecutablePath) + "\\", @"DNT FPS Demo Game Dll.dll", "FPS Example.dll");
#endif
                }
            else
                {
*/
                dnt_torque = new dnTorque(Process.GetCurrentProcess().Handle);
                //Initialize Torque, pass a handle to this form into T3D so it knows where to rendor the screen to.
                //If you don't do this, you can't pass the mouse and key strokes, w/out the mouse and keystrokes
                //being redirected the application will hang intermittently.
#if DEBUG
                dnt_torque.InitializeTorque(new[] { "" }, "DNT_FPS_Demo_Game_Dll.Scripts.Server.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Client.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Main", "", "", "", Path.GetDirectoryName(Application.ExecutablePath) + "\\", @"DNT FPS Demo Game Dll.dll", "<!!__PROJECTNAME__!!>_DEBUG.dll");
#else
                dnt_torque.InitializeTorque(new[] {""}, "DNT_FPS_Demo_Game_Dll.Scripts.Server.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Client.Main", "DNT_FPS_Demo_Game_Dll.Scripts.Main", "", "", "", Path.GetDirectoryName(Application.ExecutablePath) + "\\", @"DNT FPS Demo Game Dll.dll", "<!!__PROJECTNAME__!!>.dll");
#endif
                //Let's prepare the T3D display,
/*                }
*/
            dnt_torque.WindowIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            //Turn verbose debugging off.
            dnt_torque.Debugging = false;
            while (dnt_torque.IsRunning)
                Thread.Sleep(1000);
            dnt_torque = null;
            Application.Exit();
            }
        }
    }