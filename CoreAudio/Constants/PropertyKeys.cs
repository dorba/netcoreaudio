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

namespace Vannatech.CoreAudio.Constants
{
	/// <summary>
	/// Defines all applicable property keys for CoreAudio API.
	/// </summary>
    public static class PropertyKeys
    {
		/* Defined by MMDevice API */
		/* ----------------------- */

		/// <summary>
		/// Indicates the physical attributes of the audio endpoint device.
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_FormFactor = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// Specifies the CLSID of the registered provider of the device-properties extension for the audio endpoint device.
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_ControlPanelPageProvider = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

        /// <summary>
		/// Associates a kernel-streaming (KS) pin category with an audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEndpoint_Association = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

        /// <summary>
		/// Defines the physical speaker configuration for the audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEndpoint_PhysicalSpeakers = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

        /// <summary>
		/// Supplies the DirectSound device identifier that corresponds to the audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEndpoint_GUID = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

        /// <summary>
		/// Specifies whether system effects are enabled in the shared-mode stream that flows to or from the audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEndpoint_Disable_SysFx = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

        /// <summary>
		/// Specifies the channel-configuration mask for the full-range speakers that are connected to the audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEndpoint_FullRangeSpeakers = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// Indicates whether the endpoint supports the event-driven mode.
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_Supports_EventDriven_Mode = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// Contains an output category GUID for an audio endpoint device. 
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_JackSubType = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);
        
		/// <summary>
		/// Specifies the device format, which is the format that the audio engine uses for the shared-mode stream that flows to or from the audio endpoint device.
        /// </summary>
        public static readonly Guid PKEY_AudioEngine_DeviceFormat = new Guid(0xf19f064d, 0x82c, 0x4e27, 0xbc, 0x73, 0x68, 0x82, 0xa1, 0xbb, 0x8e, 0x4c);

		/// <summary>
		/// Specifies the default format of the device that is used for rendering or capturing a stream.
		/// </summary>
		public static readonly Guid PKEY_AudioEngine_OEMFormat = new Guid(0xe4870e26, 0x3cc5, 0x4cd2, 0xba, 0x46, 0xca, 0xa, 0x9a, 0x70, 0xed, 0x4);

		/* External to MMDevice API */
		/* ------------------------ */

		/// <summary>
		/// Contains the friendly name of the endpoint device.
		/// </summary>
		public static readonly Guid PKEY_DeviceInterface_FriendlyName = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0);

	}
}
