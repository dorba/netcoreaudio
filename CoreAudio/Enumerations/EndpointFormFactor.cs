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

namespace Vannatech.CoreAudio.Enumerations
{
	/// <summary>
	/// Defines constants that indicate the general physical attributes of an audio endpoint device.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370830.aspx
	/// </remarks>
	public enum EndpointFormFactor
	{
		/// <summary>
		/// An audio endpoint device that the user accesses remotely through a network.
		/// </summary>
		RemoteNetworkDevice = 0,

		/// <summary>
		/// A set of speakers.
		/// </summary>
		Speakers = 1,

		/// <summary>
		/// An audio endpoint device that sends or receives a line-level a line-level analog signal.
		/// </summary>
		LineLevel = 2,

		/// <summary>
		/// A set of headphones.
		/// </summary>
		Headphones = 3,

		/// <summary>
		/// A microphone.
		/// </summary>
		Microphone = 4,

		/// <summary>
		/// An earphone or a pair of earphones with an attached mouthpiece for two-way communication.
		/// </summary>
		Headset = 5,

		/// <summary>
		/// The part of a telephone that is held in the hand and that contains a speaker and a microphone for two-way communication.
		/// </summary>
		Handset = 6,

		/// <summary>
		/// An audio endpoint device that connects to an audio adapter through a connector for a digital interface of unknown type.
		/// </summary>
		UnknownDigitalPassthrough = 7,

		/// <summary>
		/// An audio endpoint device that connects to an audio adapter through a Sony/Philips Digital Interface (S/PDIF) connector.
		/// </summary>
		SPDIF = 8,

		/// <summary>
		/// An audio endpoint device that connects to an audio adapter through a High-Definition Multimedia Interface (HDMI) connector.
		/// </summary>
		DigitalAudioDisplayDevice = 9,

		/// <summary>
		/// An audio endpoint device with unknown physical attributes.
		/// </summary>
		UnknownFormFactor = 10
	}
}
