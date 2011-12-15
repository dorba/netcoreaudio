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
using Vannatech.CoreAudio.Structures;

namespace Vannatech.CoreAudio.Interfaces
{
    /// <summary>
    /// Provides information about the jacks or internal connectors that provide a physical connection between a device and an endpoint.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371388.aspx
    /// </remarks>
    public partial interface IKsJackDescription2
    {
        /// <summary>
        /// Gets the number of jacks on the connector, which are required to connect to an endpoint device.
        /// </summary>
        /// <param name="count">Receives the number of audio jacks associated with the connector.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetJackCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 count);

        /// <summary>
        /// Gets the description of a specified audio jack.
        /// </summary>
        /// <param name="index">The zero-based index of the jack.</param>
        /// <param name="descriptor">Receives a structure of type <see cref="KSJACK_DESCRIPTION2"/> that contains information about the jack.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetJackDescription(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
            [Out] out KSJACK_DESCRIPTION2 descriptor);
    }
}
