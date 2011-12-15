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

namespace Vannatech.CoreAudio.Externals
{
	/// <summary>
	/// The STGM constants are flags that indicate conditions for creating and deleting the object and access modes for the object.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/aa380337.aspx
	/// Note: This item is external to CoreAudio API, and is defined in the Windows Structured Storage API.
	/// </remarks>
	public class STGM
	{
		/// <summary>
		/// Indicates that the object is read-only, meaning that modifications cannot be made.
		/// </summary>
		public const int STGM_READ = 0x00000000;

		/// <summary>
		/// Enables you to save changes to the object, but does not permit access to its data.
		/// </summary>
		public const int STGM_WRITE = 0x00000001;

		/// <summary>
		/// Enables access and modification of object data.
		/// </summary>
		public const int STGM_READWRITE = 0x00000002;
	}
}
