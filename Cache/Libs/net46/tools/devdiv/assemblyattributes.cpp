//------------------------------------------------------------------------------
/// <copyright from='1997' to='2001' company='Microsoft Corporation'>           
///    Copyright (c) Microsoft Corporation. All Rights Reserved.                
///    Information Contained Herein is Proprietary and Confidential.            
/// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 * Assembly attributes. This file is preprocessed to generate a .cpp file
 * with the correct information.
 */

// This is necessary to make this file to compile with /clr:pure
#define RC_INVOKED

// HACK ALERT!  This is a temporary solution until we have a generalized
// version story.
#ifndef USE_VSVERSION
#include <fxver.h>
#else
#include <vsver.h>
#endif

// This hack exists because the C++ preprocessor will not consume #using directives directly.
#define USING #using <mscorlib.dll>
USING

using namespace System;
using namespace System::Reflection;
using namespace System::Resources;
using namespace System::Diagnostics;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::CompilerServices;
using namespace System::Security;

[assembly:ComVisible(ASSEMBLY_ATTRIBUTE_COM_VISIBLE)];

#ifdef ASSEMBLY_ATTRIBUTE_CLS_COMPLIANT
[assembly:CLSCompliant(ASSEMBLY_ATTRIBUTE_CLS_COMPLIANT != 0)];
#endif

#if ALLOW_PARTIALTRUSTCALLS
[assembly:AllowPartiallyTrustedCallers];
#else
#if CONDITIONAL_APTCA_L1 || CONDITIONAL_APTCA_L2
[assembly:AllowPartiallyTrustedCallers(PartialTrustVisibilityLevel=PartialTrustVisibilityLevel::NotVisibleByDefault)];
#endif
#endif

#if TRANSPARENT_ASSEMBLY
[assembly:SecurityTransparent];
#endif

// We only need an extra attribute for level 1 - level 2 always has transparency on
#if SECURITY_MIGRATION || CONDITIONAL_APTCA_L1
#if ENABLE_SECURITYTRANSPARENCY
[assembly:SecurityCritical];
#endif
#endif

// WPF test team continues to build 3.5 tests out of the same codebase.  Since SecurityRules is a new-to-v4
// construct, don't include this section at ALL if we're building for CLR 2.0.
#if !SUPPRESS_SECURITY_RULES
#if (TRANSPARENT_ASSEMBLY || ALLOW_PARTIALTRUSTCALLS || CONDITIONAL_APTCA_L1 || CONDITIONAL_APTCA_L2 || ENABLE_SECURITYTRANSPARENCY) && !SILVERLIGHT && !TESTBUILD_CLR20 && !TESTBUILD_CLR40
#if SECURITY_MIGRATION || CONDITIONAL_APTCA_L1
[assembly:SecurityRules(SecurityRuleSet::Level1)];
#else
[assembly:SecurityRules(SecurityRuleSet::Level2)];
#endif
#endif
#endif

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//

                                                         // Corresponding version resource field
[assembly:AssemblyTitle(VER_INTERNALNAME_STR)];          // InternalName
[assembly:AssemblyDescription(VER_FILEDESCRIPTION_STR)]; // FileDescription
[assembly:AssemblyDefaultAlias(VER_INTERNALNAME_STR)];   // InternalName
[assembly:AssemblyCompany(VER_COMPANYNAME_STR)];         // CompanyName
[assembly:AssemblyProduct(VER_PRODUCTNAME_STR)];         // ProductName
[assembly:AssemblyCopyright(VER_LEGALCOPYRIGHT_STR)];    // LegalCopyright
// [assembly:AssemblyCulture("neutral")];                // 


//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Revision
//      Build Number
//
// You can specify all the value or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
#if (!defined(USE_OWN_ASSEMBLYVERSION) || USE_OWN_ASSEMBLYVERSION==0) && !((defined(USE_OWN_ASSEMBLYVERSION_ONLY) && USE_OWN_ASSEMBLYVERSION_ONLY==1)  ||  (defined(USE_OWN_SAT_AND_ASSEMBLYVERSION_ONLY) && USE_OWN_SAT_AND_ASSEMBLYVERSION_ONLY==1))
[assembly:AssemblyVersion(VER_ASSEMBLYVERSION_STR)];
[assembly:AssemblyFileVersion(VER_FILEVERSION_STR)];
[assembly:AssemblyInformationalVersion(VER_PRODUCTVERSION_STR)];
[assembly:SatelliteContractVersion(VER_ASSEMBLYVERSION_STR)];
#endif

#if defined(USE_OWN_ASSEMBLYVERSION_ONLY) && USE_OWN_ASSEMBLYVERSION_ONLY==1
[assembly:AssemblyFileVersion(VER_FILEVERSION_STR)];
[assembly:AssemblyInformationalVersion(VER_PRODUCTVERSION_STR)];
[assembly:SatelliteContractVersion(VER_ASSEMBLYVERSION_STR)];
#endif

#if defined(USE_OWN_SAT_AND_ASSEMBLYVERSION_ONLY) && USE_OWN_SAT_AND_ASSEMBLYVERSION_ONLY==1
[assembly:AssemblyFileVersion(VER_FILEVERSION_STR)];
[assembly:AssemblyInformationalVersion(VER_PRODUCTVERSION_STR)];
#endif


#if USE_OWN_ASSEMBLYVERSION==1 && USE_BRANCH_FILEVERSION==1 && defined(BRANCH_NAME)
#define VER_BRANCH BRANCH_NAME.VER_FILEVERSION
#define VER_BRANCH_STR QUOTE_MACRO(VER_BRANCH)
[assembly:AssemblyFileVersion(VER_BRANCH_STR)];
#endif

[assembly:NeutralResourcesLanguageAttribute("en-US")];
//
// In order to sign your assembly you must specify a key to use. Refer to the 
// COM+ 2.0 documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified - the assembly cannot be signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. 
//   (*) If the key file and a key name attributes are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP - that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the file is installed into the CSP and used.
//   (*) Delay Signing is an advanced option - see the COM+ 2.0 documentation for 
//       more information on this.
//

#if SIGN_ASSEMBLY
#if NO_DELAY_SIGN
[assembly:AssemblyDelaySign(false)];
#else
[assembly:AssemblyDelaySign(true)];
#endif 
[assembly:AssemblyKeyFile(QUOTE_MACRO(ASSEMBLY_KEY_FILE))];
#if !SILVERLIGHT
#if FEATURE_STRONG_FRAMEWORK_SIGNATURES 
#if ASSEMBLY_KEY_TYPE_ECMA || ASSEMBLY_KEY_TYPE_MICROSOFT || ASSEMBLY_KEY_TYPE_FINAL
[assembly:AssemblySignatureKey("002400000c800000140100000602000000240000525341310008000001000100613399aff18ef1a2c2514a273a42d9042b72321f1757102df9ebada69923e2738406c21e5b801552ab8d200a65a235e001ac9adc25f2d811eb09496a4c6a59d4619589c69f5baf0c4179a47311d92555cd006acc8b5959f2bd6e10e360c34537a1d266da8085856583c85d81da7f3ec01ed9564c58d93d713cd0172c8e23a10f0239b80c96b07736f5d8b022542a4e74251a5f432824318b3539a5a087f8e53d2f135f9ca47f3bb2e10aff0af0849504fb7cea3ff192dc8de0edad64c68efde34c56d302ad55fd6e80f302d5efcdeae953658d3452561b5f36c542efdbdd9f888538d374cef106acf7d93a4445c3c73cd911f0571aaf3d54da12b11ddec375b3","a5a866e1ee186f807668209f3b11236ace5e21f117803a3143abb126dd035d7d2f876b6938aaf2ee3414d5420d753621400db44a49c486ce134300a2106adb6bdb433590fef8ad5c43cba82290dc49530effd86523d9483c00f458af46890036b0e2c61d077d7fbac467a506eba29e467a87198b053c749aa2a4d2840c784e6d")];
#elif ASSEMBLY_KEY_TYPE_MSSHARED
[assembly:AssemblySignatureKey("002400000c80000014010000060200000024000052534131000800000100010085aad0bef0688d1b994a0d78e1fd29fc24ac34ed3d3ac3fb9b3d0c48386ba834aa880035060a8848b2d8adf58e670ed20914be3681a891c9c8c01eef2ab22872547c39be00af0e6c72485d7cfd1a51df8947d36ceba9989106b58abe79e6a3e71a01ed6bdc867012883e0b1a4d35b1b5eeed6df21e401bb0c22f2246ccb69979dc9e61eef262832ed0f2064853725a75485fa8a3efb7e027319c86dec03dc3b1bca2b5081bab52a627b9917450dfad534799e1c7af58683bdfa135f1518ff1ea60e90d7b993a6c87fd3dd93408e35d1296f9a7f9a97c5db56c0f3cc25ad11e9777f94d138b3cea53b9a8331c2e6dcb8d2ea94e18bf1163ff112a22dbd92d429a","8913ef869646d14971df222c210018ab394cfe63f8eb9b4d894c0dda7368cfb69df15d049b347f8a8b9205cdcc3d6bd6690f0bd24b3da3179feb8c03f410703027c7844ff654997e38015dfc97222c15061af2a1d3fe91775b9dd4b8ede64d3d59816270a9520e393c8e60619b80d40fc1dc4f12b5aa0c2df20d02ea32960851")];
#endif
#endif
#endif 
#endif 



#if COM_COMPATIBLE_SIDEBYSIDE
// These two attributes are for supporting side-by-side of COM-visible
// objects with NDP 1.0 RTM.  This file is built into all assemblies
// that expose COM-visible types to be made Side by Side with NDP 1.0 RTM.
[assembly:ComCompatibleVersion(1,0,3300,0)]
#endif

#if PLATFORM_FRIEND_ASSEMBLY

//
// Silverlight platform assemblies may be friends of each other, allowing access to framework internal members
// 
[assembly: InternalsVisibleTo("Microsoft.Xna.Framework="  SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("Microsoft.Xna.Framework.Graphics="  SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("Microsoft.Xna.Framework.Graphics.Shaders="  SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("Microsoft.WlcProfile, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("Microsoft.WlcProfile.Internal, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Core, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Xml, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Runtime.Serialization, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.ServiceModel.Web, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Windows, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Windows.Browser, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Windows.RuntimeHost, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Windows.Xna, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]
[assembly: InternalsVisibleTo("System.Net, PublicKey=" SILVERLIGHT_PLATFORM_PUBLICKEY_FULL_STR, AllInternalsVisible = false)]

#endif // PLATFORM_FRIEND_ASEMBLY
