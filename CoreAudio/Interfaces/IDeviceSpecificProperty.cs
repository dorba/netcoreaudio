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
	/// Provides access to the control value of a device-specific hardware control.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371121.aspx
    /// </remarks>
	public partial interface IDeviceSpecificProperty
    {
        /// <summary>
        /// Gets the data type of the device-specific property.
        /// </summary>
        /// <param name="dataType">Receives the data type of the device-specific property value.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetType(
            [Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr dataType); // TODO: Fix this, C++ is (VARTYPE *)

        /// <summary>
        /// Gets the value of the device-specific property.
        /// </summary>
        /// <param name="propertyValue">Receives the property value.</param>
        /// <param name="propertySize">Sends the size in bytes of the property value, then receives the actual size of the property value written to the buffer.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int GetValue(
			[Out] [MarshalAs(UnmanagedType.SysInt)] out IntPtr propertyValue,
			[In, Out] [MarshalAs(UnmanagedType.U4)] ref UInt32 propertySize);

        /// <summary>
        /// Sets the value of the device-specific property.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="propertySize">The property value size.</param>
        /// <param name="eventContext">A user context value that is passed to the notification callback.</param>
        /// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
        [PreserveSig]
        int SetValue(
            [In] [MarshalAs(UnmanagedType.SysInt)] IntPtr propertyValue,
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 propertySize,
            [In, Optional] [MarshalAs(UnmanagedType.LPStruct)] Guid eventContext);

		/// <summary>
		/// Gets the 4-byte range of the device-specific property.
		/// </summary>
		/// <param name="propertyMin">Receives the minimum property value.</param>
		/// <param name="propertyMax">Receives the maximum property value.</param>
		/// <param name="propertyInc">Receives the stepping value between consecutive property values in the range.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int Get4BRange(
			[Out] [MarshalAs(UnmanagedType.I4)] out Int32 propertyMin,
			[Out] [MarshalAs(UnmanagedType.I4)] out Int32 propertyMax,
			[Out] [MarshalAs(UnmanagedType.I4)] out Int32 propertyInc);

    }
}
