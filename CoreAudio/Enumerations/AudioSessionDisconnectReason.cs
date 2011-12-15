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
	/// Defines constants that indicate a reason for an audio session being disconnected.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: Unknown
	/// </remarks>
	public enum AudioSessionDisconnectReason
	{
		/// <summary>
		/// The user removed the audio endpoint device.
		/// </summary>
		DisconnectReasonDeviceRemoval = 0,

		/// <summary>
		/// The Windows audio service has stopped.
		/// </summary>
		DisconnectReasonServerShutdown = 1,

		/// <summary>
		/// The stream format changed for the device that the audio session is connected to.
		/// </summary>
		DisconnectReasonFormatChanged = 2,

		/// <summary>
		/// The user logged off the WTS session that the audio session was running in.
		/// </summary>
		DisconnectReasonSessionLogoff = 3,

		/// <summary>
		/// The WTS session that the audio session was running in was disconnected.
		/// </summary>
		DisconnectReasonSessionDisconnected = 4,

		/// <summary>
		/// The (shared-mode) audio session was disconnected to make the audio endpoint device available for an exclusive-mode connection.
		/// </summary>
		DisconnectReasonExclusiveModeOverride = 5
	}
}
