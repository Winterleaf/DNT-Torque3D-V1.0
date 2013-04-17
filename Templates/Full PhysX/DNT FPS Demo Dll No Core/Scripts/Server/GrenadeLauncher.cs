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

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    public partial class Main : TorqueScriptTemplate
        {
        [Torque_Decorations.TorqueCallBack("", "", "enableManualDetonation", "(%obj)", 1, 2200, false)]
        public void EnableManualDetonation(string thisobj)
            {
            console.SetVar(string.Format("{0}.detonadeEnabled", thisobj), true);
            }

        [Torque_Decorations.TorqueCallBack("", "", "doManualDetonation", "(%obj)", 1, 2200, false)]
        public void DoManualDetonation(string obj)
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("Item", "");
            tch.Props.Add("dataBlock", "Detonade");
            string nade = tch.Create(m_ts).ToString(CultureInfo.InvariantCulture);
            SimSet.pushToBack("MissionCleanUp", nade);
            SceneObject.setTransform(nade, SceneObject.getTransform(obj));
            console.SetVar("sourceObject", console.GetVarString(string.Format("{0}.sourceObject", obj)));
            SimObject.schedule(nade, "50", "setDamageState", "Destroyed");
            console.deleteVariables(obj);
            }

        [Torque_Decorations.TorqueCallBack("", "Detonade", "onDestroyed", "(%this, %object, %lastState)", 3, 2200, false)]
        public void DetonadeOnDestroyed(string thisobj, string obj, string laststate)
            {
            RadiusDamage(obj, SceneObject.getTransform(obj).AsString(), "10", "25", "DetonadeDamage", "2000");
            }

        [Torque_Decorations.TorqueCallBack("", "GrenadeLauncherImage", "onMount", "(%this, %obj, %slot,nameSpaceDepth)", 4, 2200, false)]
        public void GrenadeLauncherImageOnMount(string thisobj, string obj, string slot, string nameSpaceDepth)
            {
            // Make it ready
            console.SetVar(string.Format("{0}.detonadeEnabled", obj), true);
            int nsd = (nameSpaceDepth.AsInt() + 1);
            console.ParentExecute(thisobj, "onMount", nsd, new[] {thisobj, obj, slot});
            }

        [Torque_Decorations.TorqueCallBack("", "GrenadeLauncherImage", "onAltFire", "(%this, %obj, %slot)", 3, 2200, false)]
        public void GrenadeLauncherImageOnAltFire(string thisobj, string obj, string slot)
            {
            }
        }
    }