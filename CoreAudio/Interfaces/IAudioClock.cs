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
	/// Enables a client to monitor a stream's data rate and the current position in the stream.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370881.aspx
    /// </remarks>
	public partial interface IAudioClock
    {
		/// <summary>
		/// Gets the device frequency.
		/// </summary>
		/// <param name="frequency">Receives the device frequency.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetFrequency(
			[Out] [MarshalAs(UnmanagedType.U8)] out UInt64 frequency);

		/// <summary>
		/// Gets the current position in the stream.
		/// </summary>
		/// <param name="devicePosition">Receives the device position.</param>
		/// <param name="counterPosition">Receives the value of the performance counter at the time that the device read the position.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetPosition(
			[Out] [MarshalAs(UnmanagedType.U8)] out UInt64 devicePosition,
			[Out, Optional] [MarshalAs(UnmanagedType.U8)] out UInt64 counterPosition);

		/// <summary>
		/// Reserved for future use.
		/// </summary>
		/// <param name="characteristics">Receives a value that indicates the characteristics of the audio clock.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetCharacteristics(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 characteristics);
    }
}
