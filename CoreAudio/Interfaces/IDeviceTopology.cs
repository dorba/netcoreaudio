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
	/// Provides access to the topology of an audio device.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371376.aspx
    /// </remarks>
	public partial interface IDeviceTopology
    {
        /// <summary>
        /// Gets the number of connectors in the device-topology object.
        /// </summary>
		/// <param name="count">Receives the connector count.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetConnectorCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 count);

		/// <summary>
		/// Gets the connector that is specified by a connector number.
		/// </summary>
		/// <param name="index">The zero-based index of the connector.</param>
		/// <param name="connector">Receives the <see cref="IConnector"/> interface of the connector object.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetConnector(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IConnector connector);

        /// <summary>
        /// Gets the number of subunits in the device topology.
        /// </summary>
        /// <param name="subunitCount">Receives the subunit count.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetSubunitCount(
			[Out] [MarshalAs(UnmanagedType.U4)] out UInt32 subunitCount);

        /// <summary>
        /// Gets the subunit that is specified by a subunit number.
        /// </summary>
        /// <param name="subunitIndex">The zero-based index of the subunit.</param>
        /// <param name="subunit">Receives the <see cref="ISubunit"/> interface of the object.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetSubunit(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 subunitIndex,
			[Out] [MarshalAs(UnmanagedType.Interface)] out ISubunit subunit);

        /// <summary>
        /// Gets a part that is identified by its local ID.
        /// </summary>
        /// <param name="partId">The ID of the part to get.</param>
        /// <param name="part">Receives the <see cref="IPart"/> interface of the part object.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetPartById(
			[In] [MarshalAs(UnmanagedType.U4)] UInt32 partId,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IPart part);

		/// <summary>
		/// Gets the device identifier of the device that is represented by the device-topology object.
		/// </summary>
		/// <param name="deviceId">Receives a string containing the device ID.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetDeviceId(
			[Out] [MarshalAs(UnmanagedType.LPWStr)] out string deviceId);

		/// <summary>
		/// Gets a list of parts in the signal path that links two parts, if the path exists.
		/// </summary>
		/// <param name="partFrom">The part at the beginning of the signal path.</param>
		/// <param name="partTo">The part at the end of the signal path.</param>
		/// <param name="rejectMixedPaths">Specifies whether to reject paths that contain mixed data.</param>
		/// <param name="partList">Receives an <see cref="IPartsList"/> interface instance.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetSignalPath(
			[In] [MarshalAs(UnmanagedType.Interface)] IPart partFrom,
			[In] [MarshalAs(UnmanagedType.Interface)] IPart partTo,
			[In] [MarshalAs(UnmanagedType.Bool)] bool rejectMixedPaths,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IPartsList partList);
    }
}
