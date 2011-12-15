// -----------------------------------------
// SoundScribe (TM) and related software.
// 
// Copyright (C) 2007-2011 Vannatech
// http://www.vannatech.com
// All rights reserved.
// 
// This source code is subject to the MIT License.
// http://www.opensource.org/licenses/mit-license.php
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// -----------------------------------------

using System;

namespace Vannatech.CoreAudio.Externals
{
    /// <summary>
    /// Values that are used in activation calls to indicate the execution contexts in which an object is to be run.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/ms693716.aspx
    /// Note: This item is external to CoreAudio API, and is defined in the Windows COM API.
    /// </remarks>
    public enum CLSCTX : uint
    {
        /// <summary>
        /// The code that creates and manages objects of this class is a DLL that runs in the same process as the caller of the function specifying the class context.
        /// </summary>
        CLSCTX_INPROC_SERVER = 0x1,

        /// <summary>
        /// The code that manages objects of this class is an in-process handler.
        /// </summary>
        CLSCTX_INPROC_HANDLER = 0x2,

        /// <summary>
        /// The EXE code that creates and manages objects of this class runs on same machine but is loaded in a separate process space.
        /// </summary>
        CLSCTX_LOCAL_SERVER = 0x4,

        /// <summary>
        /// Obsolete.
        /// </summary>
        CLSCTX_INPROC_SERVER16 = 0x8,

        /// <summary>
        /// A remote context.
        /// </summary>
        CLSCTX_REMOTE_SERVER = 0x10,

        /// <summary>
        /// Obsolete.
        /// </summary>
        CLSCTX_INPROC_HANDLER16 = 0x20,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_RESERVED1 = 0x40,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_RESERVED2 = 0x80,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_RESERVED3 = 0x100,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_RESERVED4 = 0x200,

        /// <summary>
        /// Disaables the downloading of code from the directory service or the Internet.
        /// </summary>
        CLSCTX_NO_CODE_DOWNLOAD = 0x400,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_RESERVED5 = 0x800,

        /// <summary>
        /// Specify if you want the activation to fail if it uses custom marshalling.
        /// </summary>
        CLSCTX_NO_CUSTOM_MARSHAL = 0x1000,

        /// <summary>
        /// Enables the downloading of code from the directory service or the Internet.
        /// </summary>
        CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000,

        /// <summary>
        /// Can be used to override the logging of failures
        /// </summary>
        CLSCTX_NO_FAILURE_LOG = 0x4000,
        
        /// <summary>
        /// Disables activate-as-activator (AAA) activations for this activation only.
        /// </summary>
        CLSCTX_DISABLE_AAA = 0x8000,

        /// <summary>
        /// Enables activate-as-activator (AAA) activations for this activation only.
        /// </summary>
        CLSCTX_ENABLE_AAA = 0x10000,

        /// <summary>
        /// Begin this activation from the default context of the current apartment.
        /// </summary>
        CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000,

        /// <summary>
        /// Activate or connect to a 32-bit version of the server; fail if one is not registered.
        /// </summary>
        CLSCTX_ACTIVATE_32_BIT_SERVER = 0x40000,

        /// <summary>
        /// Activate or connect to a 64 bit version of the server; fail if one is not registered. 
        /// </summary>
        CLSCTX_ACTIVATE_64_BIT_SERVER = 0x80000,

        /// <summary>
        /// Obsolete.
        /// </summary>
        CLSCTX_ENABLE_CLOAKING = 0x100000,

        /// <summary>
        /// Reserved.
        /// </summary>
        CLSCTX_PS_DLL = 0x80000000 

    }
}
