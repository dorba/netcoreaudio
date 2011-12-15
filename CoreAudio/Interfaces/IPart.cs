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
	/// Represents a part (connector or subunit) of a device topology.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371429.aspx
    /// </remarks>
	public partial interface IPart
    {
        /// <summary>
        /// Gets the friendly name of this part.
        /// </summary>
        /// <param name="name">Receives the friendly name of this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetName(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string name);

        /// <summary>
        /// Gets the local ID of this part.
        /// </summary>
        /// <param name="localId">Receives the local ID of this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetLocalId(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 localId);

        /// <summary>
        /// Gets the global ID of this part.
        /// </summary>
        /// <param name="globalId">Receives a string that contains the global ID.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetGlobalId(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string globalId);

        /// <summary>
        /// Gets the part type of this part.
        /// </summary>
        /// <param name="partType">Receives the method writes the part type.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetPartType(
            [Out] out PartType partType);

        /// <summary>
        /// Gets the part subtype of this part.
        /// </summary>
        /// <param name="subType">Receives the subtype ID for this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetSubType(
            [Out] out Guid subType);

        /// <summary>
        /// Gets the number of control interfaces that this part supports.
        /// </summary>
        /// <param name="count">Receives the number of control interfaces on this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetControlInterfaceCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 count);

        /// <summary>
        /// Gets a reference to the specified control interface, if this part supports it.
        /// </summary>
        /// <param name="index">The zero-based index of the control interface.</param>
        /// <param name="control">Receives the <see cref="IControlInterface"/> interface of the specified audio function.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetControlInterface(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 index,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IControlInterface control);

        /// <summary>
        /// Retrieves a list of all the parts that reside on data paths that are upstream from this part.
        /// </summary>
        /// <param name="partList">Receives an <see cref="IPartsList"/> interface that encapsulates the list of parts that are immediately upstream from this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int EnumPartsIncoming(
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPartsList partList);

        /// <summary>
        /// Retrieves a list of all the parts that reside on data paths that are downstream from this part.
        /// </summary>
        /// <param name="partList">Receives an <see cref="IPartsList"/> interface that encapsulates the list of parts that are immediately downstream from this part.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int EnumPartsOutgoing(
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPartsList partList);

        /// <summary>
        /// Gets a reference to the <see cref="IDeviceTopology"/> interface of the device topology object that contains this part.
        /// </summary>
        /// <param name="deviceTopology">Receives the <see cref="IDeviceTopology"/> interface of the device topology object.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetTopologyObject(
            [Out] [MarshalAs(UnmanagedType.Interface)] out IDeviceTopology deviceTopology);

		/// <summary>
		/// Activates an interface on a connector or subunit.
		/// </summary>
		/// <param name="classContext">The execution context in which the code that manages the newly created object will run.</param>
		/// <param name="interfaceId">The interface ID for the requested control function.</param>
		/// <param name="instancePtr">Receives the address of an instance implementing the interface specified by the interfaceId parameter.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Activate(
			[In] UInt32 classContext,
			[In] ref Guid interfaceId,
			[Out, Optional] [MarshalAs(UnmanagedType.IUnknown)] out object instancePtr);

		/// <summary>
		/// Registers the <see cref="IControlChangeNotify"/> interface, which the client implements to receive notifications of status changes in this part.
		/// </summary>
		/// <param name="interfaceId">The function-specific control interface that is to be monitored for control changes.</param>
		/// <param name="client">A client object that implements the <see cref="IControlChangeNotify"/> interface.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int RegisterControlChangeCallback(
			[In] ref Guid interfaceId,
			[In] IControlChangeNotify client);

		/// <summary>
		/// Removes a previous registration of an <see cref="IControlChangeNotify"/> interface.
		/// </summary>
		/// <param name="client">The client whose registration is to be removed.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int UnregisterControlChangeCallback(
			[In] IControlChangeNotify client);
    }
}
