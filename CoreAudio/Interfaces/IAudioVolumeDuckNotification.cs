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

namespace Vannatech.CoreAudio.Interfaces
{
    /// <summary>
	/// Used to by the system to send notifications about stream attenuation changes.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371006.aspx
    /// </remarks>
	public partial interface IAudioVolumeDuckNotification
    {
		/// <summary>
		/// Sends a notification about a pending system ducking event.
		/// </summary>
		/// <param name="sessionId">A string containing the session instance identifier of the communications session that raises the the auto-ducking event.</param>
		/// <param name="activeSessionCount">The number of active communications sessions.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int OnVolumeDuckNotification(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string sessionId,
			[In] UInt32 activeSessionCount);

		/// <summary>
		/// Sends a notification about a pending system unducking event.
		/// </summary>
		/// <param name="sessionId">A string containing the session instance identifier of the terminating communications session that intiated the ducking.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int OnVolumeUnduckNotification(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string sessionId);
    }
}
