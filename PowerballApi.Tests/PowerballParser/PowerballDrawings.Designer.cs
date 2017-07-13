﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PowerballApi.UnitTests.PowerballParser {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class PowerballDrawings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PowerballDrawings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PowerballApi.UnitTests.PowerballParser.PowerballDrawings", typeof(PowerballDrawings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Draw Date   WB1 WB2 WB3 WB4 WB5 PB  PP
        ///01/50/2000  1  2  3  4  5  6  7.
        /// </summary>
        internal static string DataLineHasInvalidDate {
            get {
                return ResourceManager.GetString("DataLineHasInvalidDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Draw Date   WB1 WB2 WB3 WB4 WB5 PB  PP
        ///01/01/2000  1  2  3  4  5.
        /// </summary>
        internal static string DataLineTooShort {
            get {
                return ResourceManager.GetString("DataLineTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Draw Date   WB1 WB2 WB3 WB4 WB5 PB  PP
        ///01/01/2000  1  2  3  4  5  6.
        /// </summary>
        internal static string DataLineWithoutPowerplay {
            get {
                return ResourceManager.GetString("DataLineWithoutPowerplay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Draw Date   WB1 WB2 WB3 WB4 WB5 PB  PP
        ///01/01/2000  1  2  3  4  5  6  7.
        /// </summary>
        internal static string DataLineWithPowerplay {
            get {
                return ResourceManager.GetString("DataLineWithPowerplay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Draw Date   WB1 WB2 WB3 WB4 WB5 PB  PP
        ///01/01/2000  1  2  3  4  5  6  7
        ///01/01/2000  1  2  3  4  5  6  7.
        /// </summary>
        internal static string FileContainsDuplicateDates {
            get {
                return ResourceManager.GetString("FileContainsDuplicateDates", resourceCulture);
            }
        }
    }
}
