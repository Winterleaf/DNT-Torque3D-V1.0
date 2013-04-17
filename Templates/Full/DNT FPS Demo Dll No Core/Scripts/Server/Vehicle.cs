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

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    //-----------------------------------------------------------------------------
    // Torque
    // Copyright GarageGames, LLC 2011
    //-----------------------------------------------------------------------------

    // Parenting is in place for WheeledVehicleData to VehicleData.  This should
    // make it easier for people to simply drop in new (generic) vehicles.  All that
    // the user needs to create is a set of datablocks for the new wheeled vehicle
    // to use.  This means that no (or little) scripting should be necessary.

    // Special, or unique vehicles however will still require some scripting.  They
    // may need to override the onAdd() function in order to mount weapons,
    // differing tires/springs, etc., almost everything else is taken care of in the
    // WheeledVehicleData and VehicleData methods.  This helps us by not having to
    // duplicate the same code for every new vehicle.

    // In theory this would work for HoverVehicles and FlyingVehicles also, but
    // hasn't been tested or fully implemented for those classes -- yet.
    public partial class Main : TorqueScriptTemplate
        {
        [Torque_Decorations.TorqueCallBack("", "VehicleData", "onAdd", "(%this, %obj)", 2, 2600, false)]
        public void VehicleDataOnAdd(string thisobj, string obj)
            {
            ShapeBase.setRechargeRate(obj, console.GetVarFloat(string.Format("{0}.rechargeRate", thisobj)));
            ShapeBase.setEnergyLevel(obj, console.GetVarFloat(string.Format("{0}.MaxEnergy", thisobj)));
            ShapeBase.setRepairRate(obj, 0);

            if ((console.GetVarBool(string.Format("{0}.mountable", obj)) || console.GetVarString(string.Format("{0}.mountable", obj)) == ""))
                console.Call(thisobj, "isMountable", new[] {obj, "true"});
            else
                console.Call(thisobj, "isMountable", new[] {obj, "false"});

            string nametag = console.GetVarString(string.Format("{0}.nameTag", thisobj));


            if (nametag.Trim() != "")
                ShapeBase.setShapeName(obj, nametag);
            }

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "onRemove", "(%this, %obj)", 2, 2600, false)]
        public void VehicleDataOnRemove(string thisobj, string obj)
            {
            // if there are passengers/driver, kick them out
            for (int i = 0; i < console.GetVarInt(string.Format("{0}.numMountPoints", console.getDatablock(obj))); i++)
                {
                string passenger = SceneObject.getMountNodeObject(obj, i).ToString(CultureInfo.InvariantCulture);
                if (passenger != "0")
                    console.Call(console.getDatablock(passenger).AsString(), "doDismount", new[] {passenger, "true"});
                }
            }

        // ----------------------------------------------------------------------------
        // Vehicle player mounting and dismounting
        // ----------------------------------------------------------------------------

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "isMountable", "(%this, %obj, %val)", 3, 2600, false)]
        public void VehicleDataIsMountable(string thisobj, string obj, string val)
            {
            console.SetVar(string.Format("{0}.mountable", obj), val);
            }

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "mountPlayer", "(%this, %vehicle, %player)", 3, 2600, false)]
        public void VehicleDataMountPlayer(string thisobj, string vehicle, string player)
            {
            if (!console.isObject(vehicle) || ShapeBase.getDamageState(vehicle) == "Destroyed")
                return;
            ShapeBase.startFade(player, 1000, 0, true);
            SimObject.schedule(thisobj, "1000", "setMountVehicle", vehicle, player);
            SimObject.schedule(player, "1500", "startFade", "1000", "0", "false");
            }

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "setMountVehicle", "(%this, %vehicle, %player)", 3, 2600, false)]
        public void VehicleDataSetMountVehicle(string thisobj, string vehicle, string player)
            {
            if (!console.isObject(vehicle) || ShapeBase.getDamageState(vehicle) == "Destroyed")
                return;
            string node = console.Call(thisobj, "findEmptySeat", new[] {vehicle, player});
            if (node == "-1")
                return;
            SceneObject.mountObject(vehicle, player, int.Parse(node), new TransformF(true));
            console.SetVar(string.Format("{0}.mVehicle", player), vehicle);
            }

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "findEmptySeat", "(%this, %vehicle, %player)", 3, 2600, false)]
        public string VehicleDataFindEmptySeat(string thisobj, string vehicle, string player)
            {
            for (int i = 0; i < console.GetVarInt(string.Format("{0}.numMountPoints", thisobj)); i++)
                {
                string node = SceneObject.getMountNodeObject(vehicle, i).ToString(CultureInfo.InvariantCulture);
                if (node != "0")
                    return i.ToString(CultureInfo.InvariantCulture);
                }
            return "-1";
            }

        [Torque_Decorations.TorqueCallBack("", "VehicleData", "switchSeats", "(%this, %vehicle, %player)", 3, 2600, false)]
        public string VehicleDataSwitchSeats(string thisobj, string vehicle, string player)
            {
            for (int i = 0; i < console.GetVarInt(string.Format("{0}.numMountPoints", thisobj)); i++)
                {
                string node = SceneObject.getMountNodeObject(vehicle, i).ToString(CultureInfo.InvariantCulture);
                if (node == player || int.Parse(node) > 0)
                    continue;
                if (node == "0")
                    return i.ToString(CultureInfo.InvariantCulture);
                }
            return "-1";
            }
        }
    }