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

namespace Vannatech.CoreAudio.Enumerations
{
	/// <summary>
	/// Defines flags that indicate the status of an audio endpoint buffer.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371458.aspx
	/// </remarks>
	[Flags]
	public enum AUDCLNT_BUFFERFLAGS
	{
		/// <summary>
		/// The data in the packet is not correlated with the previous packet's device position.
		/// </summary>
		AUDCLNT_BUFFERFLAGS_DATA_DISCONTINUITY = 0x1,

		/// <summary>
		/// Treat all of the data in the packet as silence and ignore the actual data values.
		/// </summary>
		AUDCLNT_BUFFERFLAGS_SILENT = 0x2,

		/// <summary>
		/// The time at which the device's stream position was recorded is uncertain.
		/// </summary>
		AUDCLNT_BUFFERFLAGS_TIMESTAMP_ERROR = 0x4
	}
}
