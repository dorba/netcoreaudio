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

namespace Vannatech.CoreAudio.Interfaces
{
    /// <summary>
	/// Provides information about the audio data formats that are supported by a software-configured I/O connection.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371384.aspx
    /// </remarks>
	public partial interface IKsFormatSupport
    {
        /// <summary>
        /// Indicates whether the audio adapter device supports the specified audio stream format.
        /// </summary>
        /// <param name="format">An audio-stream format specifier.</param>
        /// <param name="size">The size in bytes of the buffer that contains the format specifier.</param>
        /// <param name="isSupported">Receives a value to indicate whether the format is supported.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int IsFormatSupported(
            [In] [MarshalAs(UnmanagedType.SysInt)] IntPtr format,
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 size,
            [Out] [MarshalAs(UnmanagedType.Bool)] out bool isSupported);

		/// <summary>
		/// Gets the preferred audio stream format for the connection.
		/// </summary>
		/// <param name="format">Receives the format specifier for the preferred format.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetDevicePreferredFormat(
            [Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr format);
    }
}
