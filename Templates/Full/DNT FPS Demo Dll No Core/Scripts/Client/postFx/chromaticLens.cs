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
        [Torque_Decorations.TorqueCallBack("", "", "chromaticLens_init", "", 0, 90000, true)]
        public void chromaticLens_init()
            {
            console.SetVar("$CAPostFx::enabled", false);
            // The lens distortion coefficient.
            console.SetVar("$CAPostFx::distCoeffecient", -0.05);
            // The cubic distortion value.
            console.SetVar("$CAPostFx::cubeDistortionFactor", -0.1);
            // The amount and direction of the maxium shift for
            // the red, green, and blue channels.
            console.SetVar("$CAPostFx::colorDistortionFactor", "0.005 -0.005 0.01");


            TorqueSingleton ts = new TorqueSingleton("GFXStateBlockData", "PFX_DefaultChromaticLensStateBlock");

            ts.Props.Add("zDefined", "true");
            ts.Props.Add("zEnable", "false");
            ts.Props.Add("zWriteEnable", "false");
            ts.Props.Add("samplersDefined", "true");
            ts.Props.Add("samplerStates[0]", "SamplerClampPoint");
            ts.Create(m_ts);

            ts = new TorqueSingleton("ShaderData", "PFX_ChromaticLensShader");

            ts.PropsAddString("DXVertexShaderFile", "shaders/common/postFx/postFxV.hlsl");
            ts.PropsAddString("DXPixelShaderFile", "shaders/common/postFx/chromaticLens.hlsl");
            ts.Props.Add("pixVersion", "3.0");
            ts.Create(m_ts);


            ts = new TorqueSingleton("PostEffect", "ChromaticLensPostFX");
            ts.PropsAddString("renderTime", "PFXAfterDiffuse");
            ts.Props.Add("renderPriority", "0.2");
            ts.Props.Add("isEnabled", "false");
            ts.Props.Add("allowReflectPass", "false");

            ts.Props.Add("shader", "PFX_ChromaticLensShader");
            ts.Props.Add("stateBlock", "PFX_DefaultChromaticLensStateBlock");
            ts.PropsAddString("texture[0]", "$backBuffer");
            ts.PropsAddString("target", "backBuffer");
            ts.Create(m_ts);
            }

        [Torque_Decorations.TorqueCallBack("", "ChromaticLensPostFX", "setShaderConsts", "%this", 1, 90100, false)]
        public void ChromaticLensPostFXsetShaderConsts(string thisobj)
            {
            PostEffect.setShaderConst(thisobj, "$distCoeff", console.GetVarString("$CAPostFx::distCoeffecient"));
            PostEffect.setShaderConst(thisobj, "$cubeDistort", console.GetVarString("$CAPostFx::cubeDistortionFactor"));
            PostEffect.setShaderConst(thisobj, "$colorDistort", console.GetVarString("$CAPostFx::colorDistortionFactor"));
            }
        }
    }