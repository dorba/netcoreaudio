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
using Vannatech.CoreAudio.Enumerations;

namespace Vannatech.CoreAudio.Interfaces
{
    /// <summary>
	/// Enables a client to write output data to a rendering endpoint buffer.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd368242.aspx
    /// </remarks>
	public partial interface IAudioRenderClient
    {
		/// <summary>
		/// Retrieves a pointer to the next available space in the rendering endpoint buffer.
		/// </summary>
		/// <param name="frameCount">The number of audio frames in the data packet that the caller plans to write to the requested space in the buffer.</param>
		/// <param name="dataPointer">Receives the starting address of the buffer area into which the caller will write the data packet.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetBuffer(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 frameCount,
			[Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr dataPointer);

		/// <summary>
		/// Releases the buffer space acquired in the previous call to the <see cref="IAudioRenderClient.GetBuffer"/> method.
		/// </summary>
		/// <param name="frameCount">The number of audio frames written by the client to the data packet.</param>
		/// <param name="bufferFlag">The buffer-configuration flags. This should be either zero or <see cref="AUDCLNT_BUFFERFLAGS.AUDCLNT_BUFFERFLAGS_SILENT"/></param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int ReleaseBuffer(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 frameCount,
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 bufferFlag);
    }
}
