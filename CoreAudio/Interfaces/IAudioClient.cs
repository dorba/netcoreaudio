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
using Vannatech.CoreAudio.Externals;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Constants;

namespace Vannatech.CoreAudio.Interfaces
{
    /// <summary>
	/// Enables a client to create and initialize an audio stream between an audio application and the
	/// audio engine (for a shared-mode stream) or the hardware buffer (for an exclusive-mode stream).
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370865.aspx
    /// </remarks>
	public partial interface IAudioClient
    {
		/// <summary>
		/// Initializes the audio stream.
		/// </summary>
		/// <param name="shareMode">The sharing mode for the connection.</param>
		/// <param name="streamFlags">One or more <see cref="AUDCLNT_STREAMFLAGS_XXX"/> flags to control creation of the stream.</param>
		/// <param name="bufferDuration">The buffer capacity as a time value.</param>
		/// <param name="devicePeriod">
		/// In exclusive mode, this parameter specifies the requested scheduling period for successive
		/// buffer accesses by the audio endpoint device. In shared mode, it should always be set to zero.
		/// </param>
		/// <param name="format">The format descriptor.</param>
		/// <param name="audioSessionId">The ID of the audio session.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Initialize(
			[In] [MarshalAs(UnmanagedType.I4)] AUDCLNT_SHAREMODE shareMode,
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 streamFlags,
			[In] [MarshalAs(UnmanagedType.U8)] UInt64 bufferDuration,
			[In] [MarshalAs(UnmanagedType.U8)] UInt64 devicePeriod,
            [In] [MarshalAs(UnmanagedType.SysInt)] IntPtr format, // TODO: Explore options for WAVEFORMATEX definition here
			[In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid audioSessionId);

		/// <summary>
		/// Retrieves the size (maximum capacity) of the audio buffer associated with the endpoint.
		/// </summary>
        /// <param name="size">Receives the number of audio frames that the buffer can hold.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetBufferSize(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 size);

		/// <summary>
		/// Retrieves the maximum latency for the current stream and can be called any time after the stream has been initialized.
		/// </summary>
		/// <param name="latency">Receives a time value representing the latency.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetStreamLatency(
            [Out] [MarshalAs(UnmanagedType.U8)] out UInt64 latency);

		/// <summary>
		/// Retrieves the number of frames of padding in the endpoint buffer.
		/// </summary>
		/// <param name="frameCount">Receives the number of audio frames of padding in the buffer.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetCurrentPadding(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 frameCount);

		/// <summary>
		/// Indicates whether the audio endpoint device supports a particular stream format.
		/// </summary>
		/// <param name="shareMode">The sharing mode for the stream format.</param>
		/// <param name="format">The specified stream format.</param>
		/// <param name="closestMatch">The supported format that is closest to the format specified in the format parameter.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int IsFormatSupported(
            [In] [MarshalAs(UnmanagedType.I4)] AUDCLNT_SHAREMODE shareMode,
            [In] [MarshalAs(UnmanagedType.SysInt)] IntPtr format, // TODO: Explore options for WAVEFORMATEX definition here
			[Out, Optional] out IntPtr closestMatch); // TODO: Sort out WAVEFORMATEX **match (returned)

		/// <summary>
		/// Retrieves the stream format that the audio engine uses for its internal processing of shared-mode streams.
		/// </summary>
        /// <param name="format">Receives the address of the mix format.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetMixFormat(
            [Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr format); // TODO: Explore options for WAVEFORMATEX definition here

		/// <summary>
		/// Retrieves the length of the periodic interval separating successive processing passes by the audio engine on the data in the endpoint buffer.
		/// </summary>
		/// <param name="processInterval">Receives a time value specifying the default interval between processing passes by the audio engine.</param>
		/// <param name="minimumInterval">Receives a time value specifying the minimum interval between processing passes by the audio endpoint device.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetDevicePeriod(
            [Out, Optional] [MarshalAs(UnmanagedType.U8)] out UInt64 processInterval,
            [Out, Optional] [MarshalAs(UnmanagedType.U8)] out UInt64 minimumInterval);

		/// <summary>
		/// Starts the audio stream.
		/// </summary>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Start();

		/// <summary>
		/// Stops the audio stream.
		/// </summary>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Stop();

		/// <summary>
		/// Resets the audio stream.
		/// </summary>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Reset();

		/// <summary>
		/// Sets the event handle that the audio engine will signal each time a buffer becomes ready to be processed by the client.
		/// </summary>
		/// <param name="handle">The event handle.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int SetEventHandle(
            [In] [MarshalAs(UnmanagedType.SysInt)] IntPtr handle);

		/// <summary>
		/// Accesses additional services from the audio client object.
		/// </summary>
		/// <param name="interfaceId">The interface ID for the requested service.</param>
		/// <param name="instancePtr">Receives the address of an instance of the requested interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetService(
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid interfaceId,
			[Out] [MarshalAs(UnmanagedType.IUnknown)] out object instancePtr);
    }
}
