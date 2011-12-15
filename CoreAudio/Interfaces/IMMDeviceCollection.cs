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
    /// Represents a collection of audio devices.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371396.aspx
    /// </remarks>
    public partial interface IMMDeviceCollection
    {
        /// <summary>
        /// Retrieves a count of the devices in the device collection.
        /// </summary>
        /// <param name="count">The number of devices in the device collection.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 count);

        /// <summary>
        /// Retrieves a pointer to the specified item in the device collection.
        /// </summary>
        /// <param name="index">The zero based device index.</param>
        /// <param name="device">The <see cref="IMMDevice"/> interface of the specified item in the device collection.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int Item(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IMMDevice device);
    }
}
