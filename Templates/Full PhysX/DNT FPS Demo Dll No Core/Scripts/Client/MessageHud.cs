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

namespace DNT_FPS_Demo_Game_Dll.Scripts.Client
    {
    public partial class Main : TorqueScriptTemplate
        {
        //-----------------------------------------------------------------------------
        // Torque
        // Copyright GarageGames, LLC 2011
        //-----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        // Enter Chat Message Hud
        //----------------------------------------------------------------------------

        //------------------------------------------------------------------------------


        [Torque_Decorations.TorqueCallBack("", "MessageHud", "open", "(this)", 1, 5000, false)]
        public string MessageHudOpen(string thisobj)
            {
            const int offset = 6;


            if (GuiControl.isVisible(thisobj))
                return string.Empty;

            string text = console.GetVarBool(thisobj + ".isTeamMsg") ? "TEAM:" : "GLOBAL:";

            GuiControl.setValue("MessageHud_Text", text);

            string windowPos = "0 " + console.GetVarString("outerChatHud.position").Split(' ')[1] + (console.GetVarString("outerChatHud.extent").Split(' ')[1].AsInt() + 1).AsString();
            string windowExt = string.Format("{0} {1}", console.GetVarString("OuterChatHud.extent").Split(' ')[0], console.GetVarString("MessageHud_Frame.extent").Split(' ')[1]);

            int textExtent = console.GetVarString("MessageHud_Text.extent").Split(' ')[0].AsInt() + 14;
            int ctrlExtent = console.GetVarString("MessageHud_Frame.extent").Split(' ')[0].AsInt();

            GuiCanvas.pushDialog("Canvas", thisobj);

            console.SetVar("messageHud_Frame.position", windowPos);
            console.SetVar("messageHud_Frame.extent", windowExt);

            console.SetVar("MessageHud_Edit.position", Util.setWord(console.GetVarString("MessageHud_Edit.position"), 0, (textExtent + offset).AsString()));
            console.SetVar("MessageHud_Edit.extent", Util.setWord(console.GetVarString("MessageHud_Edit.extent"), 0, ((ctrlExtent - textExtent - (2*offset))).AsString()));

            GuiControl.setVisible(thisobj, true);


            console.Call("deactivateKeyboard");

            GuiControl.makeFirstResponder("MessageHud_Edit", "true");

            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "MessageHud", "close", "(this)", 1, 5000, false)]
        public string MessageHudClose(string thisobj)
            {
            if (!GuiControl.isVisible(thisobj))
                return string.Empty;

            GuiCanvas.popDialog("Canvas", thisobj);
            GuiControl.setVisible(thisobj, false);

            if (console.GetVarBool("$enableDirectInput"))
                console.Call("activateKeyboard");

            GuiControl.setValue("MessageHud_Edit", "");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "MessageHud", "toggleState", "(this)", 1, 5000, false)]
        public string MessageHudToggleState(string thisobj)
            {
            console.Call(thisobj, GuiControl.isVisible(thisobj) ? "close" : "open");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "MessageHud_Edit", "onEscape", "(this)", 1, 5000, false)]
        public string MessageHudEditOnEscape(string thisobj)
            {
            console.Call("MessageHud", "close");
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "MessageHud_Edit", "eval", "(this)", 1, 5000, false)]
        public string MessageHudEditEval(string thisobj)
            {
            string text = Util.collapseEscape(console.Call(thisobj, "getValue").Trim());
            if (text != "")
                {
                console.commandToServer(console.GetVarString("MessageHud.isTeamMsg").AsBool() ? "teamMessageSent" : "messageSent", new[] {text});
                }
            console.Call("MessageHud", "close");
            return string.Empty;
            }

        //----------------------------------------------------------------------------
        // MessageHud key handlers
        [Torque_Decorations.TorqueCallBack("", "", "toggleMessageHud", "(make)", 1, 5000, false)]
        public string ToggleMessageHud(string make)
            {
            if (make.AsBool())
                {
                console.SetVar("MessageHud.isTeamMsg", false);
                console.Call("MessageHud", "toggleState");
                }
            return string.Empty;
            }

        [Torque_Decorations.TorqueCallBack("", "", "teamMessageHud", "(make)", 1, 5000, false)]
        public string TeamMessageHud(string make)
            {
            if (make.AsBool())
                {
                console.SetVar("MessageHud.isTeamMsg", true);
                console.Call("MessageHud", "toggleState");
                }
            return string.Empty;
            }
        }
    }