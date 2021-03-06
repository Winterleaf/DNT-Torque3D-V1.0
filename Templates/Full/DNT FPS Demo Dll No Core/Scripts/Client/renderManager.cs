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
        [Torque_Decorations.TorqueCallBack("", "", "initRenderManager", "", 0, 54000, false)]
        public void initRenderManager()
            {
            //con.error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!RENDOR_MANAGER_INITIALIZED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            console.Eval(@"assert(!isObject(""DiffuseRenderPassManager""), ""initRenderManager() - DiffuseRenderPassManager already initialized!"")");
            new Torque_Class_Helper("RenderPassManager", "DiffuseRenderPassManager").Create(m_ts);
            // This token, and the associated render managers, ensure that driver MSAA 
            // does not get used for Advanced Lighting renders.  The 'AL_FormatResolve' 
            // PostEffect copies the result to the backbuffer.	

            #region new RenderFormatToken(AL_FormatToken)

            Torque_Class_Helper tch = new Torque_Class_Helper("RenderFormatToken", "AL_FormatToken");
            tch.PropsAddString("enabled", "false");
            tch.PropsAddString("format", "GFXFormatR8G8B8A8");
            tch.PropsAddString("depthFormat", "GFXFormatD24S8");
            tch.Props.Add("aaLevel", "0"); // -1 = match backbuffer
            // The contents of the back buffer before this format token is executed
            // is provided in $inTex
            tch.PropsAddString("copyEffect", "AL_FormatCopy");
            // The contents of the render target created by this format token is
            // provided in $inTex
            tch.PropsAddString("resolveEffect", "AL_FormatCopy");
            tch.Create(m_ts);

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderPassStateBin() { renderOrder = 0.001; stateToken = AL_FormatToken; } );

            Torque_Class_Helper tchb = new Torque_Class_Helper("RenderPassStateBin");
            tchb.Props.Add("renderOrder", "0.001");
            tchb.Props.Add("stateToken", "AL_FormatToken");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // We really need to fix the sky to render after all the 
            // meshes... but that causes issues in reflections.

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr() { bintype = "Sky"; renderOrder = 0.1; processAddOrder = 0.1; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Sky");
            tchb.Props.Add("renderOrder", "0.1");
            tchb.Props.Add("processAddOrder", "0.1");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr()              { bintype = "Begin"; renderOrder = 0.2; processAddOrder = 0.2; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Begin");
            tchb.Props.Add("renderOrder", "0.2");
            tchb.Props.Add("processAddOrder", "0.2");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // Normal mesh rendering.

            #region DiffuseRenderPassManager.addManager( new RenderMeshMgr()                { bintype = "Interior"; renderOrder = 0.3; processAddOrder = 0.3; } );

            tchb = new Torque_Class_Helper("RenderMeshMgr");
            tchb.PropsAddString("bintype", "Interior");
            tchb.Props.Add("renderOrder", "0.3");
            tchb.Props.Add("processAddOrder", "0.3");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderTerrainMgr()             { renderOrder = 0.4; processAddOrder = 0.4; } );

            tchb = new Torque_Class_Helper("RenderTerrainMgr");
            tchb.Props.Add("renderOrder", "0.4");
            tchb.Props.Add("processAddOrder", "0.4");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region   DiffuseRenderPassManager.addManager( new RenderMeshMgr()                { bintype = "Mesh"; renderOrder = 0.5; processAddOrder = 0.5; } );

            tchb = new Torque_Class_Helper("RenderMeshMgr");
            tchb.PropsAddString("bintype", "Mesh");
            tchb.Props.Add("renderOrder", "0.5");
            tchb.Props.Add("processAddOrder", "0.5");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderImposterMgr()            { renderOrder = 0.56; processAddOrder = 0.56; } );

            tchb = new Torque_Class_Helper("RenderImposterMgr");
            tchb.Props.Add("renderOrder", "0.56");
            tchb.Props.Add("processAddOrder", "0.56");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr()              { bintype = "Object"; renderOrder = 0.6; processAddOrder = 0.6; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Object");
            tchb.Props.Add("renderOrder", "0.6");
            tchb.Props.Add("processAddOrder", "0.6");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr()              { bintype = "Shadow"; renderOrder = 0.7; processAddOrder = 0.7; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Shadow");
            tchb.Props.Add("renderOrder", "0.7");
            tchb.Props.Add("processAddOrder", "0.7");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderMeshMgr()                { bintype = "Decal"; renderOrder = 0.8; processAddOrder = 0.8; } );

            tchb = new Torque_Class_Helper("RenderMeshMgr");
            tchb.PropsAddString("bintype", "Decal");
            tchb.Props.Add("renderOrder", "0.8");
            tchb.Props.Add("processAddOrder", "0.8");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderOcclusionMgr()           { bintype = "Occluder"; renderOrder = 0.9; processAddOrder = 0.9; } );

            tchb = new Torque_Class_Helper("RenderOcclusionMgr");
            tchb.PropsAddString("bintype", "Occluder");
            tchb.Props.Add("renderOrder", "0.9");
            tchb.Props.Add("processAddOrder", "0.9");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // We now render translucent objects that should handle
            // their own fogging and lighting.

            // Note that the fog effect is triggered before this bin.

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr(ObjTranslucentBin) { bintype = "ObjectTranslucent"; renderOrder = 1.0; processAddOrder = 1.0; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr", "ObjTranslucentBin");
            tchb.PropsAddString("bintype", "ObjectTranslucent");
            tchb.Props.Add("renderOrder", "1");
            tchb.Props.Add("processAddOrder", "1");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr()              { bintype = "Water"; renderOrder = 1.2; processAddOrder = 1.2; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Water");
            tchb.Props.Add("renderOrder", "1.2");
            tchb.Props.Add("processAddOrder", "1.2");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr()              { bintype = "Foliage"; renderOrder = 1.3; processAddOrder = 1.3; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr");
            tchb.PropsAddString("bintype", "Foliage");
            tchb.Props.Add("renderOrder", "1.3");
            tchb.Props.Add("processAddOrder", "1.3");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderParticleMgr()            { renderOrder = 1.35; processAddOrder = 1.35; } );

            tchb = new Torque_Class_Helper("RenderParticleMgr");
            tchb.Props.Add("renderOrder", "1.35");
            tchb.Props.Add("processAddOrder", "1.35");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            #region DiffuseRenderPassManager.addManager( new RenderTranslucentMgr()         { renderOrder = 1.4; processAddOrder = 1.4; } );

            tchb = new Torque_Class_Helper("RenderTranslucentMgr");
            tchb.Props.Add("renderOrder", "1.4");
            tchb.Props.Add("processAddOrder", "1.4");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // Note that the GlowPostFx is triggered after this bin.

            #region DiffuseRenderPassManager.addManager( new RenderGlowMgr(GlowBin) { renderOrder = 1.5; processAddOrder = 1.5; } );

            tchb = new Torque_Class_Helper("RenderGlowMgr", "GlowBin");
            tchb.Props.Add("renderOrder", "1.5");
            tchb.Props.Add("processAddOrder", "1.5");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // We render any editor stuff from this bin.  Note that the HDR is
            // completed before this bin to keep editor elements from tone mapping. 

            #region DiffuseRenderPassManager.addManager( new RenderObjectMgr(EditorBin) { bintype = "Editor"; renderOrder = 1.6; processAddOrder = 1.6; } );

            tchb = new Torque_Class_Helper("RenderObjectMgr", "EditorBin");
            tchb.PropsAddString("bintype", "Editor");
            tchb.Props.Add("renderOrder", "1.6");
            tchb.Props.Add("processAddOrder", "1.6");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion

            // Resolve format change token last.

            #region DiffuseRenderPassManager.addManager( new RenderPassStateBin() { renderOrder = 1.7; stateToken = AL_FormatToken; } );

            tchb = new Torque_Class_Helper("RenderPassStateBin");
            tchb.Props.Add("renderOrder", "1.7");
            tchb.Props.Add("stateToken", "AL_FormatToken");
            RenderPassManager.addManager("DiffuseRenderPassManager", tchb.Create(m_ts).AsString());

            #endregion
            }

        /// This post effect is used to copy data from the non-MSAA back-buffer to the
        /// device back buffer (which could be MSAA). It must be declared here so that
        /// it is initialized when 'AL_FormatToken' is initialzed.
        [Torque_Decorations.TorqueCallBack("", "", "initRenderManager_Auto1", "", 0, 54000, true)]
        public void initRenderManager_Auto1()
            {
            TorqueSingleton ts = new TorqueSingleton("GFXStateBlockData", "AL_FormatTokenState : PFX_DefaultStateBlock");
            ts.Props.Add("samplersDefined", "true");
            ts.Props.Add("samplerStates[0]", "SamplerClampPoint");
            ts.Create(m_ts);

            ts = new TorqueSingleton("PostEffect", "AL_FormatCopy");
            ts.Props.Add("isEnabled", "false");
            ts.Props.Add("allowReflectPass", "true");
            ts.Props.Add("shader", "PFX_PassthruShader");
            ts.Props.Add("stateBlock", "AL_FormatTokenState");
            ts.PropsAddString("texture[0]", "$inTex");
            ts.PropsAddString("target", "$backbuffer");
            ts.Create(m_ts);
            }
        }
    }