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

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    public partial class Main : TorqueScriptTemplate
        {
        [Torque_Decorations.TorqueCallBack("", "", "server_defaults_init", "", 0, 90, false)]
        public void server_defaults_init()
            {
            // List of master servers to query, each one is tried in order
            // until one responds
            console.SetVar("$Pref::Server::RegionMask", 2);
            console.SetVar("$pref::Master[0]", "2:master.garagegames.com:28002");

            // Information about the server
            console.SetVar("$Pref::Server::Name", "Torque 3D Vince Server");
            console.SetVar("$Pref::Server::Info", "Vince's Server.");

            // The connection error message is transmitted to the client immediatly
            // on connection, if any further error occures during the connection
            // process, such as network traffic mismatch, or missing files, this error
            // message is display. This message should be replaced with information
            // usefull to the client, such as the url or ftp address of where the
            // latest version of the game can be obtained.
            console.SetVar("$Pref::Server::ConnectionError", "You do not have the correct version of the FPS starter kit or " + "the related art needed to play on this server, please contact " + "the server operator for more information.");

            // The network port is also defined by the client, this value 
            // overrides pref::net::port for dedicated servers
            console.SetVar("$Pref::Server::Port", 28003);

            // If the password is set, clients must provide it in order
            // to connect to the server
            console.SetVar("$Pref::Server::Password", "");

            // Password for admin clients
            console.SetVar("$Pref::Server::AdminPassword", "");

            //Not needed anymore.
            // Misc server settings.
            //console.SetVar("$Pref::Server::MaxPlayers", 64);


            console.SetVar("$Pref::Server::TimeLimit", 20); // In minutes
            console.SetVar("$Pref::Server::KickBanTime", 300); // specified in seconds
            console.SetVar("$Pref::Server::BanTime", 1800); // specified in seconds
            console.SetVar("$Pref::Server::FloodProtectionEnabled", 1);
            console.SetVar("$Pref::Server::MaxChatLen", 120);
            }

        [Torque_Decorations.TorqueCallBack("", "", "defaultcs_init", "", 0, 90, true)]
        public void DefaultcsInit()
            {
            //-----------------------------------------------------------------------------
            // Torque
            // Copyright GarageGames, LLC 2011
            //-----------------------------------------------------------------------------

            // First we execute the core default preferences.
            //dnt.DoScriptInjection(ScriptType.Core_Server, 13000, 13000);

            server_defaults_init();


            //Init_Server_Defaults

            // Now add your own game specific server preferences as
            // well as any overloaded core defaults here.

            // Finally load the preferences saved from the last
            // game execution if they exist.
            if (Util.isFile("./scripts/server/prefs.cs"))
                Util.exec("./scripts/server/prefs.cs", false, false);

            console.SetVar("$pref::Net::PacketRateToClient", 32);
            console.SetVar("$pref::Net::PacketSize", 200);
            }
        }
    }