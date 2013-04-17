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
// (a) Licensee may not: (i) create any derivative works of DNT or DNTC, including but not
// limited to translations, localizations, technology add-ons, or game making software
// other than Games; (ii) reverse engineer , or otherwise attempt to derive the algorithms
// for DNT or DNTC (iii) redistribute, encumber , sell, rent, lease, sublicense, or otherwise
// transfer rights to DNT or DNTC; or (iv) remove or alter any tra demark, logo, copyright
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
using System.Collections.Generic;
using System.Text;

#endregion

namespace WinterLeaf.Classes
    {
    /// <summary>
    /// This is a helper class to attempt to clean up the code
    /// when creating a object inside of torque.
    /// </summary>
    public sealed class Torque_Class_Helper
        {
        /// <summary>
        /// Object Properties.
        /// </summary>
        private Dictionary<string, string> _mParams = new Dictionary<string, string>();

        /// <summary>
        /// Constructor Creates an object to template a torquescript class with.
        /// </summary>
        /// <param name="className"></param>
        public Torque_Class_Helper(string className)
            {
            LClassName = className;
            LInstanceName = "";
            }

        /// <summary>
        ///   Creates an object to template a torquescript class with.
        /// </summary>
        /// <param name="className"> The name of the object type </param>
        /// <param name="instanceName"> The name of this instance of the object </param>
        public Torque_Class_Helper(string className, string instanceName)
            {
            LClassName = className;
            LInstanceName = instanceName;
            }

        /// <summary>
        ///   The name of the instance of the object
        /// </summary>
        public string LInstanceName { get; set; }

        /// <summary>
        ///   The name of the TorqueScript class.
        /// </summary>
        public string LClassName { get; set; }

        /// <summary>
        ///   The properties in the script block/datablock
        /// </summary>
        public Dictionary<string, string> Props
            {
            get { return _mParams; }
            set { _mParams = value; }
            }

        public void PropsAddString(string key, string str)
            {
            _mParams.Add(key, '"' + str + '"');
            }

        /// <summary>
        ///   This will return the TorqueScript code that will be executed when Create is called.
        /// </summary>
        /// <returns> </returns>
        public override string ToString()
            {
            StringBuilder result = new StringBuilder();
            result.Append(" new ");
            result.Append(LClassName);
            result.Append("(");
            result.Append(LInstanceName);
            result.Append(")\r\n{");
            foreach (KeyValuePair<string, string> ele in _mParams)
                {
                result.Append(ele.Key);
                result.Append(" = ");
                result.Append(ele.Value.Trim() != "" ? ele.Value : @"""""");
                result.Append(";\r\n");
                }
            result.Append("};");
            return (result.ToString());
            }

        /// <summary>
        ///   Passes the ToString() TorqueScript to the Eval statement capturing the id of the object created
        /// </summary>
        /// <param name="m_ts"> A reference to the dnTorque Class(you can just type in m_ts) </param>
        /// <returns> </returns>
        public UInt32 Create(dnTorque m_ts)
            {
            UInt32 r;
            string varnam = "$IReallyUniqueForthisWorkAround123654";
            m_ts.SetVar(varnam, "0");

            m_ts.Evaluate(varnam + " = " + ToString(), true);
            string id = m_ts.GetVar(varnam);
            return !UInt32.TryParse(id, out r) ? 0 : r;
            }

        /// <summary>
        /// Takes the passed TorqueScript and evaluates it in the dll.
        /// </summary>
        /// <param name="m_ts"></param>
        public void CreateDB(dnTorque m_ts)
            {
            m_ts.Evaluate(ToString(), true);
            }
        }
    }