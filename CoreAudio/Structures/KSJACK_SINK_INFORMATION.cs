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
    /// Stores information about an audio jack sink.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd316549.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KSJACK_SINK_INFORMATION
    {
        /// <summary>
        /// Specifies the type of connection.
        /// </summary>
		[MarshalAs(UnmanagedType.I4)]
        public Int32 ConnType;

        /// <summary>
        /// Specifies the sink manufacturer identifier.
        /// </summary>
		[MarshalAs(UnmanagedType.U2)]
        public UInt16 ManufacturerId;

        /// <summary>
        /// Specifies the sink product identifier.
        /// </summary>
		[MarshalAs(UnmanagedType.U2)]
		public UInt16 ProductId;

        /// <summary>
        /// Specifies the latency of the audio sink.
        /// </summary>
		[MarshalAs(UnmanagedType.U2)]
		public UInt16 AudioLatency;

        /// <summary>
        /// Specifies whether the sink supports High-bandwidth Digital Content Protection (HDCP).
        /// </summary>
		[MarshalAs(UnmanagedType.Bool)]
        public bool HDCPCapable;

        /// <summary>
        /// Specifies whether the sink supports ACP Packet, ISRC1, or ISRC2.
        /// </summary>
		[MarshalAs(UnmanagedType.Bool)]
        public bool AICapable;

        /// <summary>
        /// Specifies the length of the string in the SinkDescription member.
        /// </summary>
		[MarshalAs(UnmanagedType.U1)]
        public byte SinkDescriptionLength;

		/* These fields are removed and will need to be marshalled separately 
		-----------------------------------------------------
        /// <summary>
        /// String containing the monitor sink name.
        /// </summary>
		/// NOTE: I can find no way to marshal this correctly.
		/// NOTE: Will need to use IntPtr in code and parse the size manually.
        UInt16[] SinkDescription;

        /// <summary>
        /// Specifies the video port identifier.
        /// </summary>
        LUID PortId;
		-----------------------------------------------------
		 */
	}
}
