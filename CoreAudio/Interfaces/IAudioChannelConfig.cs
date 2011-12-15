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
	/// Provides access to a hardware channel-configuration control.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370862.aspx
    /// </remarks>
	public partial interface IAudioChannelConfig
	{
		/// <summary>
		/// Sets the channel-configuration mask in a channel-configuration control.
		/// </summary>
		/// <param name="channelConfig">The new channel-configuration mask value.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
		[PreserveSig]
		int SetChannelConfig(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 channelConfig, // TODO: get possible value from ksmedia.h
			[In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Gets the current channel-configuration mask from a channel-configuration control.
		/// </summary>
		/// <param name="channelConfig">Receives the current channel-configuration mask value.</param>
		/// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
		[PreserveSig]
		int GetChannelConfig(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 channelConfig); // TODO: get possible value from ksmedia.h
	}
}
