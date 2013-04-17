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
        /// Shortcut for typi1ng dbgSetParameters with the default values torsion uses.
        [Torque_Decorations.TorqueCallBack("", "", "initmetrics", "", 0, 36000, true)]
        public void initmetrics()
            {
            //con.error("------------------------>LOADING FRAMEOVERLAYGUI!!!!!!!!!!!!!!!!!!!!!");
            Util.exec("core/art/gui/FrameOverlayGui.gui", false, false);
            }

        [Torque_Decorations.TorqueCallBack("", "", "fpsMetricsCallback", "", 0, 36000, false)]
        public string fpsMetricsCallback()
            {
            return "  | FPS |" + "  " + console.GetVarString("$fps::real") + "  max: " + console.GetVarString("$fps::realMax") + "  min: " + console.GetVarString("$fps::realMin") + "  mspf: " + (1000/(console.GetVarInt("$fps::real") > 0 ? console.GetVarInt("$fps::real") : 1));
            }

        [Torque_Decorations.TorqueCallBack("", "", "gfxMetricsCallback", "", 0, 36000, false)]
        public string gfxMetricsCallback()
            {
            return "  | GFX |" + "  PolyCount: " + console.GetVarString("$GFXDeviceStatistics::polyCount") + "  DrawCalls: " + console.GetVarString("$GFXDeviceStatistics::drawCalls") + "  RTChanges: " + console.GetVarString("$GFXDeviceStatistics::renderTargetChanges");
            }

        [Torque_Decorations.TorqueCallBack("", "", "terrainMetricsCallback", "", 0, 36000, false)]
        public string terrainMetricsCallback()
            {
            return "  | Terrain |" + "  Cells: " + console.GetVarString("$TerrainBlock::cellsRendered") + "  Override Cells: " + console.GetVarString("$TerrainBlock::overrideCells") + "  DrawCalls: " + console.GetVarString("$TerrainBlock::drawCalls");
            }

        [Torque_Decorations.TorqueCallBack("", "", "netMetricsCallback", "", 0, 36000, false)]
        public string netMetricsCallback()
            {
            return "  | Net |" + "  BitsSent: " + console.GetVarString("$Stats::netBitsSent") + "  BitsRcvd: " + console.GetVarString("$Stats::netBitsReceived") + "  GhostUpd: " + console.GetVarString("$Stats::netGhostUpdates");
            }

        [Torque_Decorations.TorqueCallBack("", "", "groundCoverMetricsCallback", "", 0, 36000, false)]
        public string groundCoverMetricsCallback()
            {
            return "  | GroundCover |" + "  Cells: " + console.GetVarString("$GroundCover::renderedCells") + "  Billboards: " + console.GetVarString("$GroundCover::renderedBillboards") + "  Batches: " + console.GetVarString("$GroundCover::renderedBatches") + "  Shapes: " + console.GetVarString("$GroundCover::renderedShapes");
            }

        [Torque_Decorations.TorqueCallBack("", "", "sfxMetricsCallback", "", 0, 36000, false)]
        public string sfxMetricsCallback()
            {
            return "  | SFX |" + "  Sounds: " + console.GetVarString("$SFX::numSounds") + "  Lists: " + (console.GetVarInt("$SFX::numSources") - console.GetVarInt("$SFX::numSounds") - console.GetVarInt("$SFX::Device::fmodNumEventSource")) + "  Events: " + console.GetVarString("$SFX::fmodNumEventSources") + "  Playing: " + console.GetVarString("$SFX::numPlaying") + "  Culled: " + console.GetVarString("$SFX::numCulled") + "  Voices: " + console.GetVarString("$SFX::numVoices") + "  Buffers: " + console.GetVarString("$SFX::Device::numBuffers") + "  Memory: " + (console.GetVarFloat("$SFX::Device::numBufferBytes")/1024.0/1024.0) + " MB" + "  Time/S: " + console.GetVarString("$SFX::sourceUpdateTime") + "  Time/P: " + console.GetVarString("$SFX::parameterUpdateTime") + "  Time/A: " + console.GetVarString("$SFX::ambientUpdateTime");
            }

        [Torque_Decorations.TorqueCallBack("", "", "sfxSourcesMetricsCallback", "", 0, 36000, false)]
        public string sfxSourcesMetricsCallback()
            {
            return console.Call("sfxDumpSourcesToString");
            }

        [Torque_Decorations.TorqueCallBack("", "", "sfxStatesMetricsCallback", "", 0, 36000, false)]
        public string sfxStatesMetricsCallback()
            {
            return "  | SFXStates |" + console.Call("sfxGetActiveStates");
            }

        [Torque_Decorations.TorqueCallBack("", "", "timeMetricsCallback", "", 0, 36000, false)]
        public string timeMetricsCallback()
            {
            return "  | Time |" + "  Sim Time: " + console.Call("getSimTime") + "  Mod: " + (console.Call("getSimTime").AsInt()%32);
            }

        [Torque_Decorations.TorqueCallBack("", "", "reflectMetricsCallback", "", 0, 36000, false)]
        public string reflectMetricsCallback()
            {
            return "  | REFLECT |" + "  Objects: " + console.Call("$Reflect::numObjects") + "  Visible: " + console.Call("$Reflect::numVisible") + "  Occluded: " + console.Call("$Reflect::numOccluded") + "  Updated: " + console.Call("$Reflect::numUpdated") + "  Elapsed: " + console.Call("$Reflect::elapsed") + "\n" + "  Allocated: " + console.Call("$Reflect::renderTargetsAllocated") + "  Pooled: " + console.Call("$Reflect::poolSize") + "\n" + "  " + console.GetVarString("$Reflect::textureStats").Split(' ')[1] + "\t" + "  " + console.GetVarString("$Reflect::textureStats").Split(' ')[2] + "MB" + "\t" + "  " + console.GetVarString("$Reflect::textureStats").Split(' ')[0];
            }

        [Torque_Decorations.TorqueCallBack("", "", "decalMetricsCallback", "", 0, 36000, false)]
        public string decalMetricsCallback()
            {
            return "  | DECAL |" + " Batches: " + console.GetVarString("$Decal::Batches") + " Buffers: " + console.GetVarString("$Decal::Buffers") + " DecalsRendered: " + console.GetVarString("$Decal::DecalsRendered");
            }

        [Torque_Decorations.TorqueCallBack("", "", "renderMetricsCallback", "", 0, 36000, false)]
        public string renderMetricsCallback()
            {
            return "  | Render |" + "  Int: " + console.GetVarString("$RenderMetrics::RIT_Interior") + "  IntDL: " + console.GetVarString("$RenderMetrics::RIT_InteriorDynamicLighting") + "  Mesh: " + console.GetVarString("$RenderMetrics::RIT_Mesh") + "  MeshDL: " + console.GetVarString("$RenderMetrics::RIT_MeshDynamicLighting") + "  Shadow: " + console.GetVarString("$RenderMetrics::RIT_Shadow") + "  Sky: " + console.GetVarString("$RenderMetrics::RIT_Sky") + "  Obj: " + console.GetVarString("$RenderMetrics::RIT_Object") + "  ObjT: " + console.GetVarString("$RenderMetrics::RIT_ObjectTranslucent") + "  Decal: " + console.GetVarString("$RenderMetrics::RIT_Decal") + "  Water: " + console.GetVarString("$RenderMetrics::RIT_Water") + "  Foliage: " + console.GetVarString("$RenderMetrics::RIT_Foliage") + "  Trans: " + console.GetVarString("$RenderMetris::RIT_Translucent") + "  Custom: " + console.GetVarString("$RenderMetrics::RIT_Custom");
            }

        [Torque_Decorations.TorqueCallBack("", "", "shadowMetricsCallback", "", 0, 36000, false)]
        public string shadowMetricsCallback()
            {
            return "  | Shadow |" + "  Active: " + console.GetVarString("$ShadowStats::activeMaps") + "  Updated: " + console.GetVarString("$ShadowStats::updatedMaps") + "  PolyCount: " + console.GetVarString("$ShadowStats::polyCount") + "  DrawCalls: " + console.GetVarString("$ShadowStats::drawCalls") + "   RTChanges: " + console.GetVarString("$ShadowStats::rtChanges") + "   PoolTexCount: " + console.GetVarString("$ShadowStats::poolTexCount") + "   PoolTexMB: " + console.GetVarString("$ShadowStats::poolTexMemory") + "MB";
            }

        [Torque_Decorations.TorqueCallBack("", "", "basicShadowMetricsCallback", "", 0, 36000, false)]
        public string basicShadowMetricsCallback()
            {
            return "  | Shadow |" + "  Active: " + console.GetVarString("$BasicLightManagerStats::activePlugins") + "  Updated: " + console.GetVarString("$BasicLightManagerStats::shadowsUpdated") + "  Elapsed Ms: " + console.GetVarString("$BasicLightManagerStats::elapsedUpdateMs");
            }

        [Torque_Decorations.TorqueCallBack("", "", "lightMetricsCallback", "", 0, 36000, false)]
        public string lightMetricsCallback()
            {
            return "  | Deferred Lights |" + "  Active: " + console.GetVarString("$lightMetrics::activeLights") + "  Culled: " + console.GetVarString("$lightMetrics::culledLights");
            }

        [Torque_Decorations.TorqueCallBack("", "", "particleMetricsCallback", "", 0, 36000, false)]
        public string particleMetricsCallback()
            {
            return "  | Particles |" + "  # Simulated " + console.GetVarString("$particle::numSimulated");
            }

        [Torque_Decorations.TorqueCallBack("", "", "partMetricsCallback", "", 0, 36000, false)]
        public string partMetricsCallback()
            {
            return console.Call("particleMetricsCallback");
            }

        [Torque_Decorations.TorqueCallBack("", "", "audioMetricsCallback", "", 0, 36000, false)]
        public string audioMetricsCallback()
            {
            return console.Call("sfxMetricsCallback");
            }

        [Torque_Decorations.TorqueCallBack("", "", "videoMetricsCallback", "", 0, 36000, false)]
        public string videoMetricsCallback()
            {
            return console.Call("gfxMetricsCallback");
            }

        // Add a metrics HUD.  %expr can be a vector of names where each element
        // must have a corresponding '<name>MetricsCallback()' function defined
        // that will be called on each update of the GUI control.  The results
        // of each function are stringed together.
        //
        // Example: metrics( "fps gfx" );
        [Torque_Decorations.TorqueCallBack("", "", "metrics", "%expr", 1, 36000, false)]
        public void metrics(string expr)
            {
            string metricsExpr = "";
            if (expr != "")
                {
                foreach (string name in expr.Split(' '))
                    {
                    string cb = name.Trim() + "MetricsCallback";
                    if (!console.isFunction(cb))
                        {
                        console.error("metrics - undefined callback: " + cb);
                        }
                    else
                        {
                        cb = cb + "()";
                        if (metricsExpr.Length > 0)
                            metricsExpr += "\r";
                        metricsExpr += cb;
                        break;
                        //right now, for some reason it can only handle one metric...
                        }
                    }
                //if (metricsExpr != "")
                //    metricsExpr += " @ " + '\\' + '"' + " " + '\\' + '"';
                }
            if (metricsExpr != "")
                {
                console.Call("Canvas", "pushDialog", new[] {"FrameOverlayGui", "1000"});
                console.Call("TextOverlayControl", "setValue", new[] {metricsExpr});
                }
            else
                {
                console.Call("Canvas", "popDialog", new[] {"FrameOverlayGui"});
                }
            }
        }
    }