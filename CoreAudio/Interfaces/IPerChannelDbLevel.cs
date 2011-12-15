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
	/// Represents a generic subunit control interface that provides per-channel control over the
	/// volume level, in decibels, of an audio stream or of a frequency band in an audio stream.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371447.aspx
    /// </remarks>
	public partial interface IPerChannelDbLevel 
    {
		/// <summary>
		/// Gets the number of channels in the audio stream.
		/// </summary>
		/// <param name="channelCount">Receives the number of channels in the audio stream.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetChannelCount(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 channelCount);

        /// <summary>
        /// Gets the range, in decibels, of the volume level of the specified channel.
        /// </summary>
        /// <param name="index">The zero-based channel index.</param>
        /// <param name="volumeMin">Receives the minimum volume level in decibels.</param>
        /// <param name="volumeMax">Receives the maximum volume level in decibels.</param>
        /// <param name="volumeStep">Receives the volume increment level in decibels.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetLevelRange(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
            [Out] [MarshalAs(UnmanagedType.R4)] out float volumeMin,
			[Out] [MarshalAs(UnmanagedType.R4)] out float volumeMax,
			[Out] [MarshalAs(UnmanagedType.R4)] out float volumeStep);

		/// <summary>
		/// Gets the volume level, in decibels, of the specified channel.
		/// </summary>
        /// <param name="index">The zero-based channel index.</param>
        /// <param name="level">Receives the volume level, in decibels, of the specified channel.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetLevel(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
			[Out] [MarshalAs(UnmanagedType.R4)]  out float level);

		/// <summary>
		/// Sets the volume level, in decibels, of the specified channel.
		/// </summary>
        /// <param name="index">The zero-based channel index.</param>
        /// <param name="level">The new volume level, in decibels, of the specified channel.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int SetLevel(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

        /// <summary>
        /// Sets all channels in the audio stream to the same uniform volume level, in decibels.
        /// </summary>
        /// <param name="level">The new uniform level in decibels.</param>
        /// <param name="eventContext">A user context value that is passed to the notification callback.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int SetLevelUniform(
			[In] [MarshalAs(UnmanagedType.R4)] float level,
            [In, Optional]  [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Sets the volume levels, in decibels, of all the channels in the audio stream.
		/// </summary>
        /// <param name="levels">An array of new volume levels, per channel, in decibels.</param>
		/// <param name="channelCount">The number of channels in the audio stream. This must match the array length.</param>
		/// <param name="eventContext">A user context value that is passed to the notification callback.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int SetLevelAllChannels(
            [In] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeParamIndex = 2)] float[] levels,
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 channelCount,
            [In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);
    }
}
