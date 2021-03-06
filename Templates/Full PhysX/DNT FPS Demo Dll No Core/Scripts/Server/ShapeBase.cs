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
using WinterLeaf.Classes;

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    // This file contains ShapeBase methods used by all the derived classes

    //-----------------------------------------------------------------------------
    // ShapeBase object
    //-----------------------------------------------------------------------------

    // A raycast helper function to keep from having to duplicate code everytime
    // that a raycast is needed.
    //  %this = the object doing the cast, usually a player
    //  %range = range to search
    //  %mask = what to look for


    public partial class Main : TorqueScriptTemplate
        {
        [Torque_Decorations.TorqueCallBack("", "ShapeBase", "doRaycast", "(%this, %range, %mask)", 3, 1300, false)]
        public void ShapeBaseDoRayCast(string shapebase, string range, string mask)
            {
            throw new Exception("Not Implemented.");

            //I didn't implement this because rarely would you call it from the script, and it would be
            //better implemented only in csharp.
            }

        [Torque_Decorations.TorqueCallBack("", "ShapeBase", "damage", "(%this, %sourceObject, %position, %damage, %damageType)", 5, 1300, false)]
        public void ShapeBaseDamage(string shapebase, string sourceobject, string position, string damage, string damagetype)
            {
            // All damage applied by one object to another should go through this method.
            // This function is provided to allow objects some chance of overriding or
            // processing damage values and types.  As opposed to having weapons call
            // ShapeBase::applyDamage directly. Damage is redirected to the datablock,
            // this is standard procedure for many built in callbacks.
            if (console.isObject(shapebase))
                console.Call(console.getDatablock(shapebase).AsString(), "damage", new[] {shapebase, sourceobject, position, damage, damagetype});
            }

        [Torque_Decorations.TorqueCallBack("", "ShapeBase", "setDamageDt", "(%this, %damageAmount, %damageType)", 3, 1300, false)]
        public void ShapeBaseSetDamageDT(string shapebase, string damageAmount, string damageType)
            {
            // This function is used to apply damage over time.  The damage is applied
            // at a fixed rate (50 ms).  Damage could be applied over time using the
            // built in ShapBase C++ repair functions (using a neg. repair), but this
            // has the advantage of going through the normal script channels.

            if (ShapeBase.getDamageState(shapebase) != "Dead")
                {
                ShapeBaseDamage(shapebase, "0", "0 0 0", damageAmount, damageType);
                console.SetVar(string.Format("{0}.damageSchedule", shapebase), SimObject.schedule(shapebase, "50", "setDamageDt", damageAmount, damageType));
                }
            else
                {
                console.SetVar(string.Format("{0}.damageSchedule", shapebase), "");
                }
            }

        [Torque_Decorations.TorqueCallBack("", "ShapeBase", "clearDamageDt", "(%this)", 1, 1300, false)]
        public void ShapeBaseClearDamageDt(string shapebase)
            {
            //I could think of soo much better ways of doing this... even if my grammar blows.
            if (console.GetVarString(shapebase + ".damageSchedule") == "")
                return;
            //con.Eval("cancel(" + con.GetVarString(thisobj + ".damageSchedule") + ");");
            console.Call("cancel", new[] {console.GetVarString(shapebase + ".damageSchedule")});
            console.SetVar(shapebase + ".damageSchedule", "");
            }

        //-----------------------------------------------------------------------------
        // ShapeBase datablock
        //-----------------------------------------------------------------------------

        [Torque_Decorations.TorqueCallBack("", "ShapeBaseData", "damage", "(%this, %obj, %position, %source, %amount, %damageType)", 6, 1300, false)]
        public void ShapeBaseDataDamage(string thisobj, string obj, string position, string source, string amount, string damageType)
            {
            // Ignore damage by default. This empty method is here to
            // avoid console warnings.
            }
        }
    }