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

using System.Collections.Generic;
using System.Linq;
using WinterLeaf.Classes;
using WinterLeaf.Containers;
using WinterLeaf.Enums;

#endregion

namespace DNT_FPS_Demo_Game_Dll.Scripts.Server
    {
    //-----------------------------------------------------------------------------
    // Torque
    // Copyright GarageGames, LLC 2011
    //-----------------------------------------------------------------------------

    //-----------------------------------------------------------------------------
    // Trigger-derrived teleporter object. Teleports an object from it's entrance to 
    // it's exit if one is defined.  

    public partial class Main : TorqueScriptTemplate
        {
        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "onAdd", "( %this, %teleporter )", 2, 1800, false)]
        public void TeleporterTriggerOnAdd(string thisobj, string teleporter)
            {
            if (console.GetVarString(string.Format("{0}.exit", teleporter)) == "")
                console.SetVar(string.Format("{0}.exit", teleporter), "NameOfTeleporterExit");

            if (console.GetVarString(string.Format("{0}.teleporterCooldown", teleporter)) == "")
                console.SetVar(string.Format("{0}.teleporterCooldown", teleporter), console.GetVarString(thisobj + ".teleporterCooldown"));

            if (console.GetVarString(string.Format("{0}.exitVelocityScale", teleporter)) == "")
                console.SetVar(string.Format("{0}.exitVelocityScale", teleporter), console.GetVarString(thisobj + ".exitVelocityScale"));

            if (console.GetVarString(string.Format("{0}.reorientPlayer", teleporter)) == "")
                console.SetVar(string.Format("{0}.reorientPlayer", teleporter), console.GetVarString(thisobj + ".reorientPlayer"));

            if (console.GetVarString(string.Format("{0}.oneSided", teleporter)) == "")
                console.SetVar(string.Format("{0}.oneSided", teleporter), console.GetVarString(thisobj + ".oneSided"));

            if (console.GetVarString(string.Format("{0}.entranceEffect", teleporter)) == "")
                console.SetVar(string.Format("{0}.entranceEffect", teleporter), console.GetVarString(thisobj + ".entranceEffect"));

            if (console.GetVarString(string.Format("{0}.exitEffect", teleporter)) == "")
                console.SetVar(string.Format("{0}.exitEffect", teleporter), console.GetVarString(thisobj + ".exitEffect"));
            // We do not want to save this variable between levels, 
            // clear it out every time the teleporter is added 
            // to the scene.

            console.SetVar(string.Format("{0}.timeOfLastTeleport", teleporter), "");
            }

        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "onLeaveTrigger", "(%this,%trigger,%obj)", 3, 1800, false)]
        public void TeleporterTriggerOnLeaveTrigger(string thisobj, string trigger, string obj)
            {
            console.SetVar(string.Format("{0}.isTeleporting", obj), false);
            }

        //ARGS:
        // %this - The teleporter datablock.
        // %entrance - The teleporter the player has entered (The one calling this function).
        // %obj - The object that entered the teleporter.
        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "onEnterTrigger", "(%this, %entrance, %obj)", 3, 1800, false)]
        public void TeleporterTriggerOnEnterTrigger(string thisobj, string entrance, string obj)
            {
            //if (!console.isMemberOfClass(obj, "Player"))
            //    return;

            if (console.GetVarBool(string.Format("{0}.isTeleporting", obj)))
                return;
            // Get the location of our target position
            string exit = console.GetVarString(string.Format("{0}.exit", entrance));

            bool valid = TeleporterTriggerVerifyObject(thisobj, obj, entrance, exit);
            if (!valid)
                return;

            TeleporterTriggerTeleFrag(thisobj, obj, exit);
            // Create our entrance effects on all clients.
            if (console.isObject(console.GetVarString(string.Format("{0}.entranceEffect", entrance))))
                for (uint idx = 0; idx < ClientGroup.Count; idx++)
                    console.commandToClient(ClientGroup__GetItem(idx).AsString(), "PlayTeleportEffect", new[] {console.GetVarString(string.Format("{0}.position", entrance)), console.Call(entrance + ".entranceEffect", "getId")});


            TeleporterTriggerTeleportPlayer(thisobj, obj, exit);
            // Create our exit effects on all clients.
            if (console.isObject(string.Format("{0}.exitEffect", exit)))
                for (uint idx = 0; idx < ClientGroup.Count; idx++)
                    console.commandToClient(ClientGroup__GetItem(idx).AsString(), "PlayTeleportEffect", new[] {console.GetVarString(string.Format("{0}.position", exit)), console.Call(exit + ".entranceEffect", "getId")});

            // Record what time we last teleported so we can determine if enough
            // time has elapsed to teleport again
            int tolt = console.getSimTime();
            console.SetVar(string.Format("{0}.timeOfLastTeleport", entrance), tolt);
            // If this is a bidirectional teleporter, log it's exit too.
            if (console.GetVarString(string.Format("{0}.exit", exit)) == console.GetVarString(string.Format("{0}.name", entrance)))
                console.SetVar(string.Format("{0}.timeOfLastTeleport", exit), tolt);


            // Tell the client to play the 2D sound for the player that teleported.
            if (console.isObject(string.Format("{0}.teleportSound", thisobj)) && console.isObject(string.Format("{0}.client", obj)))
                {
                GameConnection.play2D(obj, console.GetVarString(string.Format("{0}.teleportSound", thisobj)));
                }
            }

        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "verifyObject", "(%this, %obj, %entrance, %exit)", 4, 1800, false)]
        public bool TeleporterTriggerVerifyObject(string thisobj, string obj, string entrance, string exit)
            {
            // Bail out early if we couldn't find an exit for this teleporter.
            if (!console.isObject(exit))
                {
                console.error(string.Format("Cound not find an exit for {0}", console.GetVarString(entrance + ".name")));
                return false;
                }


            if (!SimObject.SimObject_isMemberOfClass(obj, "Player"))
                return false;

            // If the entrance is once sided, make sure the object
            // approached it from it's front.
            if (console.GetVarBool(string.Format("{0}.oneSided", entrance)))
                {
                TransformF forwardvector = new TransformF(SceneObject.getForwardVector(entrance));

                Point3F velocity = ShapeBase.getVelocity(obj);
                float dotProduct = TransformF.vectorDot(forwardvector, velocity);
                if (dotProduct > 0)
                    return false;
                // If we are coming directly from another teleporter and it happens
                // to be bidirectional, We need to avoid ending sending objects through
                // an infinite loop.

                if (console.GetVarBool(string.Format("{0}.isTeleporting", obj)))
                    return false;
                // We only want to teleport players
                // So bail out early if we have found any 
                // other object.


                if (console.GetVarInt(string.Format("{0}.timeOfLastTeleport", entrance)) > 0 && console.GetVarInt(string.Format("{0}.teleporterCooldown", entrance)) > 0)
                    {
                    int currentTime = console.getSimTime();
                    int timedifference = currentTime - console.GetVarInt(string.Format("{0}.timeOfLastTeleport", entrance));
                    uint db = console.getDatablock(entrance);
                    if (timedifference <= console.GetVarInt(string.Format("{0}.teleporterCooldown", db.AsString())))
                        return false;
                    }
                }
            return true;
            }

        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "teleFrag", "(%this, %player, %exit)", 3, 1800, false)]
        public void TeleporterTriggerTeleFrag(string thisobj, string player, string exit)
            {
            // When a telefrag happens, there are two cases we have to consider.
            // The first case occurs when the player's bounding box is much larger than the exit location, 
            // it is possible to have players colide even though a player is not within the bounds 
            // of the trigger Because of this we first check a radius the size of a player's bounding 
            // box around the exit location.

            // Get the bounding box of the player

            Point3F boundingBoxSize = new Point3F(console.GetVarString(string.Format("{0}.boundingBox", console.getDatablock(player))));
            float radius = boundingBoxSize.x;
            float boxSizeY = boundingBoxSize.y;
            float boxSizeZ = boundingBoxSize.z;

            // Use the largest dimention as the radius to check
            if (boxSizeY > radius)
                radius = boxSizeY;
            if (boxSizeZ > radius)
                radius = boxSizeZ;

            Point3F position = SceneObject.getTransform(exit).MPosition; // new TransformF(con.getTransform(exit));
            uint mask = (uint) SceneObjectTypesAsUint.PlayerObjectType;

            // Check all objects within the found radius of the exit location, and telefrag
            // any players that meet the conditions.

            Dictionary<uint, float> r = console.initContainerRadiusSearch(position, radius, mask);
            foreach (uint objectNearExit in r.Keys.Where(objectNearExit => SimObject.SimObject_isMemberOfClass(objectNearExit.AsString(), "Player")).Where(objectNearExit => objectNearExit.AsString() != player))
                {
                ShapeBaseDamage(objectNearExit.AsString(), player, SceneObject.getTransform(exit).AsString(), // con.getTransform(exit), 
                                "10000", "Telefrag");
                }
            // The second case occurs when the bounds of the trigger are much larger
            // than the bounding box of the player. (So multiple players can exist within the
            // same trigger). For this case we check all objects contained within the trigger
            // and telefrag all players.

            int objectsInExit = Trigger.getNumObjects(exit);
            // Loop through all objects in the teleporter exit
            // And kill any players
            for (int i = 0; i < objectsInExit; i++)
                {
                string objectInTeleporter = console.Call(exit, "getObject", new[] {i.AsString()});
                if (SimObject.SimObject_isMemberOfClass(objectInTeleporter, "Player"))
                    continue;
                // Avoid killing the player that is teleporting in the case of two
                // Teleporters near eachother.
                if (objectInTeleporter == player)
                    continue;

                ShapeBaseDamage(objectInTeleporter, player, SceneObject.getTransform(exit).AsString(), // con.getTransform(exit), 
                                "10000", "Telefrag");
                }
            }

        [Torque_Decorations.TorqueCallBack("", "TeleporterTrigger", "teleportPlayer", "(%this, %player, %exit)", 3, 1800, false)]
        public void TeleporterTriggerTeleportPlayer(string thisobj, string player, string exit)
            {
            TransformF targetPosition;
            if (console.GetVarBool(string.Format("{0}.reorientPlayer", exit)))
                {
                targetPosition = SceneObject.getTransform(exit);
                }
            else
                {
                targetPosition = SceneObject.getTransform(exit);
                TransformF playerrot = SceneObject.getTransform(player);
                targetPosition.MOrientation.x = playerrot.MOrientation.x;
                targetPosition.MOrientation.y = playerrot.MOrientation.y;
                targetPosition.MOrientation.z = playerrot.MOrientation.z;
                targetPosition.MAngle = playerrot.MAngle;
                }
            SceneObject.setTransform(player, targetPosition);
            Point3F playervelocity = ShapeBase.getVelocity(player);
            playervelocity = playervelocity.vectorScale(console.GetVarFloat(string.Format("{0}.exitVelocityScale", exit)));
            ShapeBase.setVelocity(player, playervelocity);
            // Prevent the object from doing an immediate second teleport
            // In the case of a bidirectional teleporter
            console.SetVar(string.Format("{0}.isTeleporting", player), true);
            }
        }
    }