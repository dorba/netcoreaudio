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

namespace Vannatech.CoreAudio.Externals
{
    /// <summary>
    /// Exposes methods for enumerating, getting, and setting property values.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/bb761474.aspx
    /// Note: This item is external to CoreAudio API, and is defined in the Windows Property System API.
    /// </remarks>
    [Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        /// <summary>
        /// Gets the number of properties attached to the file.
        /// </summary>
        /// <param name="propertyCount">Receives the property count.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetCount(
            [Out] [MarshalAs(UnmanagedType.U4)] out UInt32 propertyCount);

        /// <summary>
        /// Gets a property key from an item's array of properties.
        /// </summary>
        /// <param name="propertyIndex">The index of the property key in the array of <see cref="PROPERTYKEY"/> structures.</param>
        /// <param name="propertyKey">The unique identifier for a property.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetAt(
            [In] [MarshalAs(UnmanagedType.U4)] UInt32 propertyIndex,
            [Out] out PROPERTYKEY propertyKey);

        /// <summary>
        /// Gets data for a specific property.
        /// </summary>
        /// <param name="propertyKey">A <see cref="PROPERTYKEY"/> structure containing a unique identifier for the property in question.</param>
        /// <param name="value">Receives a <see cref="PROPVARIANT"/> structure that contains the property data.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int GetValue(
            [In] ref PROPERTYKEY propertyKey,
            [Out] out PROPVARIANT value);

        /// <summary>
        /// Sets a new property value, or replaces or removes an existing value.
        /// </summary>
        /// <param name="propertyKey">A <see cref="PROPERTYKEY"/> structure containing a unique identifier for the property in question.</param>
        /// <param name="value">A <see cref="PROPVARIANT"/> structure that contains the new property data.</param>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int SetValue(
            [In] ref PROPERTYKEY propertyKey,
            [In] ref PROPVARIANT value);

        /// <summary>
        /// Saves a property change.
        /// </summary>
        /// <returns>An HRESULT code indicating whether the operation passed of failed.</returns>
        [PreserveSig]
        int Commit();
    }
}
