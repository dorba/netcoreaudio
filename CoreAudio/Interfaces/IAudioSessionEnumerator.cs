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
	/// Enumerates audio sessions on an audio device.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd368281.aspx
    /// </remarks>
	public partial interface IAudioSessionEnumerator
    {
		/// <summary>
		/// Gets the total number of audio sessions that are open on the audio device.
		/// </summary>
		/// <param name="count">Receives the total number of audio sessions.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetCount(
			[Out] [MarshalAs(UnmanagedType.I4)] out Int32 count);

		/// <summary>
		/// Gets the audio session specified by an audio session number. 
		/// </summary>
		/// <param name="index">The zero-based index of the session.</param>
		/// <param name="session">Receives an <see cref="IAudioSessionControl"/> session object in the collection that is maintained by the session enumerator.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetSession(
			[In] [MarshalAs(UnmanagedType.I4)] Int32 index,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl session);
    }
}
