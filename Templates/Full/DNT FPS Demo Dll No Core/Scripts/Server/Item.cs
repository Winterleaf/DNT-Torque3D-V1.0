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

//-----------------------------------------------------------------------------
// Torque
// Copyright GarageGames, LLC 2011
//-----------------------------------------------------------------------------

// These scripts make use of dynamic attribute values on Item datablocks,
// these are as follows:
//
//    maxInventory      Max inventory per object (100 bullets per box, etc.)
//    pickupName        Name to display when client pickups item
//
// Item objects can have:
//
//    count             The # of inventory items in the object.  This
//                      defaults to maxInventory if not set.

// Respawntime is the amount of time it takes for a static "auto-respawn"
// object, such as an ammo box or weapon, to re-appear after it's been
// picked up.  Any item marked as "static" is automaticlly respawned.

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    public partial class Main : TorqueScriptTemplate
        {
        public int ITem_PopTime = 10*1000;
        public int Item_RespawnTime = 90*1000;

        [Torque_Decorations.TorqueCallBack("", "Item", "respawn", "(%this)", 1, 1400, false)]
        public void ItemRespawn(string item)
            {
            // This method is used to respawn static ammo and weapon items
            // and is usually called when the item is picked up.
            // Instant fade...
            ShapeBase.startFade(item, 0, 0, true);
            ShapeBase.setHidden(item, true);

            SimObject.schedule(item, Item_RespawnTime.AsString(), "setHidden", "false");
            SimObject.schedule(item, (Item_RespawnTime + 100).AsString(), "startFade", "1000", "0", "false");
            }

        [Torque_Decorations.TorqueCallBack("", "Item", "schedulePop", "(%this)", 1, 1400, false)]
        public void ItemschedulePop(string item)
            {
            // This method deletes the object after a default duration. Dynamic
            // items such as thrown or drop weapons are usually popped to avoid
            // world clutter.
            SimObject.schedule(item, (ITem_PopTime - 1000).AsString(), "startFade", "1000", "0", "true");
            SimObject.schedule(item, ITem_PopTime.AsString(), "delete");
            }

        [Torque_Decorations.TorqueCallBack("", "ItemData", "onThrow", "(%this, %user, %amount)", 3, 1400, false)]
        public string ItemDataOnThrow(string datablock, string player, string amount)
            {
            if (amount == "")
                amount = "1";

            if (console.GetVarString(datablock + ".maxInventory") != "")
                if (amount.AsInt() > console.GetVarInt(datablock + ".maxInventory"))
                    amount = console.GetVarString(datablock + ".maxInventory");
            if (!amount.AsBool())
                return "0";


            ShapeBaseShapeBaseDecInventory(player, datablock, amount);

            // Construct the actual object in the world, and add it to
            // the mission group so it's cleaned up when the mission is
            // done.  The object is given a random z rotation.
            Torque_Class_Helper tch = new Torque_Class_Helper("Item", "");
            tch.Props.Add("datablock", datablock);
            tch.Props.Add("rotation", @"""0 0 1 " + (new Random().Next(0, 360)) + @"""");
            tch.Props.Add("count", amount);

            string item = tch.Create(m_ts).AsString();
            SimSet.pushToBack("MissionGroup", item);
            ItemschedulePop(item);
            return item;
            }

        [Torque_Decorations.TorqueCallBack("", "ItemData", "onPickup", "(%this, %obj, %user, %amount)", 4, 1400, false)]
        public bool ItemDataOnPickUp(string datablock, string item, string player, string amount)
            {
            //console.error("Datablock:" + datablock + " item: " + item + " player:" + player + " amount:");
            //console.error("item name " + ShapeBase.getShapeName(item));
            //console.error("player name " + ShapeBase.getShapeName(player));

            string count = console.GetVarString(item + ".count");
            if (count == "")
                {
                count = console.GetVarString(datablock + ".count");
                if (count == "")
                    {
                    if (console.GetVarString(datablock + ".maxInventory") != "")
                        {
                        if (count != console.GetVarString(datablock + ".maxInventory"))
                            return false;
                        }
                    else
                        count = "1";
                    }
                }
            ShapeBaseShapeBaseIncInventory(player, datablock, count);

            if (console.GetVarBool(player + ".client"))
                MessageClient(console.GetVarString(player + ".client"), "MsgItemPickup", console.ColorEncode(@"\c0You picked up %1"), console.GetVarString(datablock + ".pickupName"));
            // If the item is a static respawn item, then go ahead and
            // respawn it, otherwise remove it from the world.
            // Anything not taken up by inventory is lost.

            if (Item.isStatic(item))
                {
                //console.error("respawning Item");
                console.Call(item, "repawn");
                }
            else
                {
                //console.error("Deleting Item");
                console.Call(item, "delete");
                }


            return true;
            }

        [Torque_Decorations.TorqueCallBack("", "ItemData", "createItem", "(%data)", 1, 1400, false)]
        public string ItemDataCreateItem(string datablock)
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("Item");
            tch.Props.Add("dataBlock", datablock);
            tch.Props.Add("static", "true");
            tch.Props.Add("rotate", "true");
            string obj = tch.Create(m_ts).AsString();
            return obj;
            }
        }
    }