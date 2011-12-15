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
	/// Used by the <see cref="IPropertyStore.GetValue"/> and <see cref="IPropertyStore.SetValue"/> methods
	/// of <see cref="IPropertyStore"/> as the primary way to program item properties.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/aa380072.aspx
	/// Note: This item is external to CoreAudio API, and is defined in the Windows Structured Storage API.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct PROPVARIANT
	{
		/// <summary>
		/// Value type tag.
		/// </summary>
		public short vt;

		/// <summary>
		/// Reserved for future use.
		/// </summary>
		public short wReserved1;

		/// <summary>
		/// Reserved for future use.
		/// </summary>
		public short wReserved2;

		/// <summary>
		/// Reserved for future use.
		/// </summary>
		public short wReserved3;

        /// <summary>
        /// Represents the variant data section.
        /// </summary>
        public VariantData Data;
	}

    /// <summary>
    /// Represents the variant data section of the PROPVARIANT structure.
    /// </summary>
    /// <remarks>
    /// This only provides variants for use within the context of MMDevice properties.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct VariantData
    {
        /// <summary>
        /// Represents the data as a boolean value.
        /// </summary>
        [FieldOffset(0)]
        public bool AsBoolean;

        /// <summary>
        /// Represents the data as a unsigned 32-bit integer.
        /// </summary>
        [FieldOffset(0)]
        public UInt32 AsUInt32;

        /// <summary>
        /// Represents the data as a unicode string pointer.
        /// </summary>
        [FieldOffset(0)]
        public IntPtr AsStringPtr;

        /// <summary>
        /// Represents the data as a pointer to a WAVEFORMATEX structure.
        /// </summary>
        [FieldOffset(4)]
        public IntPtr AsFormatPtr;
    }
}
