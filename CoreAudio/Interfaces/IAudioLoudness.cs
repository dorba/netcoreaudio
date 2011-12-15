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
	/// Provides access to a loudness compensation control.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd368224.aspx
    /// </remarks>
	public partial interface IAudioLoudness
    {
		/// <summary>
		/// Gets the current state (enabled or disabled) of the loudness control.
		/// </summary>
		/// <param name="isEnabled">Receives the current loudness state.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetEnabled(
			[Out] [MarshalAs(UnmanagedType.Bool)] out bool isEnabled);

		/// <summary>
		/// Enables or disables the loudness control.
		/// </summary>
		/// <param name="isEnabled">Sets the new loudness state.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int SetEnabled(
			[In] [MarshalAs(UnmanagedType.Bool)] bool isEnabled,
			[In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
    }
}
