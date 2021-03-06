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
        [Torque_Decorations.TorqueCallBack("", "", "Initialize_AdvanceLight_LightViz", "", 0, 61100, true)]
        public void Initialize_AdvanceLight_LightViz()
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("GFXStateBlockData", "AL_DepthVisualizeState");
            tch.Props.Add("zDefined", "true");
            tch.Props.Add("zEnable", "false");
            tch.Props.Add("zWriteEnable", "false");

            tch.Props.Add("samplersDefined", "true");
            tch.Props.Add("samplerStates[0]", "SamplerClampPoint"); // depth    
            tch.Props.Add("samplerStates[1]", "SamplerClampLinear"); // viz color lookup
            tch.Create(m_ts);

            tch = new Torque_Class_Helper("GFXStateBlockData", "AL_DefaultVisualizeState");
            tch.Props.Add("blendDefined", "true");
            tch.Props.Add("blendEnable", "true");
            tch.Props.Add("blendSrc", "GFXBlendSrcAlpha");
            tch.Props.Add("blendDest", "GFXBlendInvSrcAlpha");

            tch.Props.Add("zDefined", "true");
            tch.Props.Add("zEnable", "false");
            tch.Props.Add("zWriteEnable", "false");

            tch.Props.Add("samplersDefined", "true");
            tch.Props.Add("samplerStates[0]", "SamplerClampPoint"); // #prepass
            tch.Props.Add("samplerStates[1]", "SamplerClampLinear"); // depthviz
            tch.Create(m_ts);

            tch = new Torque_Class_Helper("ShaderData", "AL_DepthVisualizeShader");
            tch.PropsAddString("DXVertexShaderFile ", "shaders/common/postFx/postFxV.hlsl");
            tch.PropsAddString("DXPixelShaderFile", "shaders/common/lighting/advanced/dbgDepthVisualizeP.hlsl");

            tch.PropsAddString("OGLVertexShaderFile", "shaders/common/postFx/postFxV.glsl");
            tch.PropsAddString("OGLPixelShaderFile", "shaders/common/lighting/advanced/gl/dbgDepthVisualizeP.glsl");

            tch.PropsAddString("samplerNames[0]", "prepassBuffer");
            tch.PropsAddString("samplerNames[1]", "depthViz");

            tch.Props.Add("pixVersion", "2.0");
            tch.Create(m_ts);

            TorqueSingleton ts = new TorqueSingleton("PostEffect", "AL_DepthVisualize");
            ts.Props.Add("shader", "AL_DepthVisualizeShader");
            ts.Props.Add("stateBlock", "AL_DefaultVisualizeState");
            ts.PropsAddString("texture[0]", "#prepass");
            ts.PropsAddString("texture[1]", "depthviz");
            ts.PropsAddString("target", "$backBuffer");
            ts.Props.Add("renderPriority", "9999");
            ts.Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "AL_DepthVisualize", "onEnabled", "$this", 1, 61200, false)]
        public bool AL_DepthVisualizeonEnabled(string thisobj)
            {
            console.Call("AL_NormalsVisualize", "disable");
            console.Call("AL_LightColorVisualize", "disable");
            console.Call("AL_LightSpecularVisualize", "disable");
            console.SetVar("$AL_NormalsVisualizeVar", false);
            console.SetVar("$AL_LightColorVisualizeVar", false);
            console.SetVar("$AL_LightSpecularVisualizeVar", false);
            return true;
            }

        [Torque_Decorations.TorqueCallBack("", "", "Initialize_AdvanceLight_LightViz1", "", 0, 61300, true)]
        public void Initialize_AdvanceLight_LightViz1()
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("ShaderData", "AL_NormalsVisualizeShader");
            tch.PropsAddString("DXVertexShaderFile", "shaders/common/postFx/postFxV.hlsl");
            tch.PropsAddString("DXPixelShaderFile", "shaders/common/lighting/advanced/dbgNormalVisualizeP.hlsl");

            tch.PropsAddString("OGLVertexShaderFile", "shaders/common/postFx/postFxV.glsl");
            tch.PropsAddString("OGLPixelShaderFile", "shaders/common/lighting/advanced/gl/dbgNormalVisualizeP.glsl");

            tch.PropsAddString("samplerNames[0]", "prepassTex");

            tch.Props.Add("pixVersion", "2.0");
            tch.Create(m_ts);

            TorqueSingleton ts = new TorqueSingleton("PostEffect", "AL_NormalsVisualize");
            ts.Props.Add("shader", "AL_NormalsVisualizeShader");
            ts.Props.Add("stateBlock", "AL_DefaultVisualizeState");
            ts.PropsAddString("texture[0]", "#prepass");
            ts.PropsAddString("target", "$backBuffer");
            ts.Props.Add("renderPriority", "9999");
            ts.Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "AL_NormalsVisualize", "onEnabled", "%this", 1, 61400, false)]
        public bool AL_NormalsVisualizeonEnabled(string thisobj)
            {
            console.Call("AL_DepthVisualize", "disable");
            console.Call("AL_LightColorVisualize", "disable");
            console.Call("AL_LightSpecularVisualize", "disable");
            console.SetVar("$AL_DepthVisualizeVar", false);
            console.SetVar("$AL_LightColorVisualizeVar", false);
            console.SetVar("$AL_LightSpecularVisualizeVar", false);
            return true;
            }

        [Torque_Decorations.TorqueCallBack("", "", "Initialize_AdvanceLight_LightViz2", "", 0, 61500, true)]
        public void Initialize_AdvanceLight_LightViz2()
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("ShaderData", "AL_LightColorVisualizeShader");
            tch.PropsAddString("DXVertexShaderFile", "shaders/common/postFx/postFxV.hlsl");
            tch.PropsAddString("DXPixelShaderFile", "shaders/common/lighting/advanced/dbgLightColorVisualizeP.hlsl");

            tch.PropsAddString("OGLVertexShaderFile", "shaders/common/postFx/postFxV.glsl");
            tch.PropsAddString("OGLPixelShaderFile", "shaders/common/lighting/advanced/dl/dbgLightColorVisualizeP.glsl");

            tch.PropsAddString("samplerNames[0]", "lightInfoBuffer");
            tch.Props.Add("pixVersion", "2.0");
            tch.Create(m_ts);

            TorqueSingleton ts = new TorqueSingleton("PostEffect", "AL_LightColorVisualize");
            ts.Props.Add("shader", "AL_LightColorVisualizeShader");
            ts.Props.Add("stateBlock", "AL_DefaultVisualizeState");
            ts.PropsAddString("texture[0]", "#lightinfo");
            ts.PropsAddString("target", "$backBuffer");
            ts.Props.Add("renderPriority", "9999");
            ts.Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "AL_LightColorVisualize", "onEnabled", "%this", 1, 61600, false)]
        public bool AL_LightColorVisualize(string thisobj)
            {
            console.Call("AL_NormalsVisualize", "disable");
            console.Call("AL_DepthVisualize", "disable");
            console.Call("AL_LightSpecularVisualize", "disable");
            console.SetVar("$AL_NormalsVisualizeVar", false);
            console.SetVar("$AL_DepthVisualizeVar", false);
            console.SetVar("$AL_LightSpecularVisualizeVar", false);
            return true;
            }

        [Torque_Decorations.TorqueCallBack("", "", "Initialize_AdvanceLight_LightViz3", "", 0, 61700, true)]
        public void Initialize_AdvanceLight_LightViz3()
            {
            Torque_Class_Helper tch = new Torque_Class_Helper("ShaderData", "AL_LightSpecularVisualizeShader");
            tch.PropsAddString("DXVertexShaderFile", "shaders/common/postFx/postFxV.hlsl");
            tch.PropsAddString("DXPixelShaderFile", "shaders/common/lighting/advanced/dbgLightSpecularVisualizeP.hlsl");

            tch.PropsAddString("OGLVertexShaderFile", "shaders/common/postFx/postFxV.glsl");
            tch.PropsAddString("OGLPixelShaderFile", "shaders/common/lighting/advanced/dl/dbgLightSpecularVisualizeP.glsl");

            tch.PropsAddString("samplerNames[0]", "lightInfoBuffer");
            tch.Props.Add("pixVersion", "2.0");
            tch.Create(m_ts);

            TorqueSingleton ts = new TorqueSingleton("PostEffect", "AL_LightSpecularVisualize");
            ts.Props.Add("shader", "AL_LightSpecularVisualizeShader");
            ts.PropsAddString("stateBlock", "AL_DefaultVisualizeState");
            ts.PropsAddString("texture[0]", "#lightinfo");
            ts.PropsAddString("target", "$backBuffer");
            ts.Props.Add("renderPriority", "9999");
            ts.Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "AL_LightSpecularVisualize", "onEnabled", "%this", 1, 61800, false)]
        public string AL_LightSpecularVisualize(string thisobj)
            {
            console.Call("AL_NormalsVisualize", "disable");
            console.Call("AL_DepthVisualize", "disable");
            console.Call("AL_LightColorVisualize", "disable");
            console.SetVar("$AL_NormalsVisualizeVar", false);
            console.SetVar("$AL_DepthVisualizeVar", false);
            console.SetVar("$AL_LightColorVisualizeVar", false);
            return "true";
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleDepthViz", "%emabled", 1, 61900, false)]
        public void toggleDepthViz(string enabled)
            {
            if (enabled == "")
                {
                console.SetVar("$AL_DepthVisualizeVar", !console.Call("AL_DepthVisualize", "isEnabled").AsBool());
                console.Call("AL_DepthVisualize", "toggle");
                }
            else if (enabled.AsBool())
                console.Call("AL_DepthVisualize", "enable");
            else if (!enabled.AsBool())
                console.Call("AL_DepthVisualize", "disable");
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleNormalsViz", "%emabled", 1, 61910, false)]
        public void toggleNormalsViz(string enabled)
            {
            if (enabled == "")
                {
                console.SetVar("$AL_NormalsVisualizeVar", !console.Call("AL_NormalsVisualize", "isEnabled").AsBool());
                console.Call("AL_NormalsVisualize", "toggle");
                }
            else if (enabled.AsBool())
                console.Call("AL_NormalsVisualize", "enable");
            else if (!enabled.AsBool())
                console.Call("AL_NormalsVisualize", "disable");
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleLightColorViz", "%emabled", 1, 61920, false)]
        public void toggleLightColorViz(string enabled)
            {
            if (enabled == "")
                {
                console.SetVar("$AL_LightColorVisualizeVar", !console.Call("AL_LightColorVisualize", "isEnabled").AsBool());
                console.Call("AL_LightColorVisualize", "toggle");
                }
            else if (enabled.AsBool())
                console.Call("AL_LightColorVisualize", "enable");
            else if (!enabled.AsBool())
                console.Call("AL_LightColorVisualize", "disable");
            }

        [Torque_Decorations.TorqueCallBack("", "", "toggleLightSpecularViz", "%emabled", 1, 61930, false)]
        public void toggleLightSpecularViz(string enabled)
            {
            if (enabled == "")
                {
                console.SetVar("$AL_LightSpecularVisualizeVar", !console.Call("AL_LightSpecularVisualize", "isEnabled").AsBool());
                console.Call("AL_LightSpecularVisualize", "toggle");
                }
            else if (enabled.AsBool())
                console.Call("AL_LightSpecularVisualize", "enable");
            else if (!enabled.AsBool())
                console.Call("AL_LightSpecularVisualize", "disable");
            }
        }
    }