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
        [Torque_Decorations.TorqueCallBack("", "", "postFXManagerPersistancesettingscs", "", 0, 105000, true)]
        public void postFXManagerPersistancesettingscs()
            {
            // Used to name the saved files.
            console.SetVar("$PostFXManager::fileExtension", ".postfxpreset.cs");
            // The filter string for file open/save dialogs.
            console.SetVar("$PostFXManager::fileFilter", "Post Effect Presets|*.postfxpreset.cs");
            // Enable / disable PostFX when loading presets or just apply the settings?
            console.SetVar("$PostFXManager::forceEnableFromPresets", true);
            }

        //Load a preset file from the disk, and apply the settings to the
        //controls. If bApplySettings is true - the actual values in the engine
        //will be changed to reflect the settings from the file.

        [Torque_Decorations.TorqueCallBack("", "PostFXManager", "loadPresetFile", "", 0, 105010, false)]
        public void PostFXManagerloadPresetFile()
            {
            console.Call("getLoadFilename", new[] {console.GetVarString("$PostFXManager::fileFilter"), "PostFXManager::loadPresetHandler"});
            }

        [Torque_Decorations.TorqueCallBack("", "PostFXManager", "loadPresetHandler", "%filename", 1, 105020, false)]
        public void PostFXManagerloadPresetHandler(string filename)
            {
            //Check the validity of the file
            if (console.Call("isScriptFile", new[] {filename}).AsBool())
                {
                filename = Util._expandFilename(filename);
                console.Call("postVerbose", new[] {"% - PostFX Manager - Executing " + filename});
                Util.exec(filename, false, false);
                console.Call("PostFXManager", "settingsApplyFromPreset");
                }
            }

        //Save a preset file to the specified file. The extension used
        //is specified by $PostFXManager::fileExtension for on the fly
        //name changes to the extension used. 
        [Torque_Decorations.TorqueCallBack("", "PostFXManager", "savePresetFile", "%this", 1, 105030, false)]
        public void PostFXManagersavePresetFile(string thisobj)
            {
            string defaultfile = Util.filePath(console.GetVarString("$Client::MissionFile")) + "/" + Util.filePath(console.GetVarString("$Client::MissionFile"));
            console.Call("getSaveFilename", new[] {console.GetVarString("$PostFXManager::fileFilter"), "PostFXManager::savePresetHandler", defaultfile});
            }

        //Called from the PostFXManager::savePresetFile() function
        [Torque_Decorations.TorqueCallBack("", "PostFXManager", "savePresetHandler", "%filename", 1, 105040, false)]
        public void PostFXManagersavePresetHandler(string filename)
            {
            filename = Util.makeRelativePath(filename, Util.getMainDotCsDir());
            if (Util.strstr(filename, ".") == -1)
                filename = filename + console.GetVarString("$PostFXManager::fileExtension");

            //Apply the current settings to the preset
            console.Call("PostFXManager", "settingsApplyAll");

            Util.export("$PostFXManager::Settings::*", filename, false);
            console.Call("postVerbose", new[] {"% - PostFX Manager - Save complete. Preset saved at : " + filename});
            }
        }
    }