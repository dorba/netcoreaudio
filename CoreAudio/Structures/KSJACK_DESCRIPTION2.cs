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
using System.Runtime.InteropServices;

namespace Vannatech.CoreAudio.Structures
{
    /// <summary>
    /// Describes an audio jack.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd316545.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
	public struct KSJACK_DESCRIPTION2
    {
        /// <summary>
        /// Reserved for future use.
        /// </summary>
		[MarshalAs(UnmanagedType.U4)]
        public UInt32 DeviceStateInfo;

        /// <summary>
        /// Stores the audio jack's capabilities.
        /// </summary>
		/// <remarks>
		/// From Ksmedia.h, the available flags for this are:
		/// 1. JACKDESC2_PRESENCE_DETECT_CAPABILITY (0x00000001)
		/// 2. JACKDESC2_DYNAMIC_FORMAT_CHANGE_CAPABILITY (0x00000002) 
		/// </remarks>
		[MarshalAs(UnmanagedType.U4)]
		public UInt32 JackCapabilities;
    }
}
