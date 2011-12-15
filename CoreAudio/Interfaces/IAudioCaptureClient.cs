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
    /// Enables a client to read input data from a capture endpoint buffer.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370858.aspx
    /// </remarks>
    public partial interface IAudioCaptureClient
    {
        /// <summary>
        /// Retrieves a pointer to the next available packet of data in the capture endpoint buffer.
        /// </summary>
        /// <param name="dataPointer">Receives the starting address of a byte array containing the next data packet that is available for the client to read.</param>
        /// <param name="frameCount">Receives the number of audio frames available in the data packet</param>
        /// <param name="bufferStatus">Receives the buffer-status flags.</param>
        /// <param name="devicePosition">Receives the device position of the first audio frame in the data packet.</param>
        /// <param name="counterPosition">
        /// Receives the value of the performance counter at the time that the audio endpoint
        /// device recorded the device position of the first audio frame in the data packet.
        /// </param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetBuffer(
            [Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr dataPointer,
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 frameCount,
            [Out] [MarshalAs(UnmanagedType.U4)] out AUDCLNT_BUFFERFLAGS bufferStatus,
            [Out, Optional] [MarshalAs(UnmanagedType.U8)] out UInt64 devicePosition,
            [Out, Optional] [MarshalAs(UnmanagedType.U8)] out UInt64 counterPosition);

        /// <summary>
        /// Releases a buffer.
        /// </summary>
        /// <param name="numFramesRead">The number of audio frames that the client read from the capture buffer.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int ReleaseBuffer(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 numFramesRead);

        /// <summary>
        /// Retrieves the number of frames in the next data packet in the capture endpoint buffer.
        /// </summary>
        /// <param name="frameCount">Receives the number of audio frames in the next capture packet.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetNextPacketSize(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 frameCount);
    }
}
