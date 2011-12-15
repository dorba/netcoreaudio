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

namespace Vannatech.CoreAudio.Constants
{
	/// <summary>
	/// Hardware support flags for an audio endpoint device.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370835.aspx
	/// </remarks>
	public class ENDPOINT_HARDWARE_SUPPORT_XXX
	{
		/// <summary>
		/// The audio endpoint device supports a hardware volume control.
		/// </summary>
		public const int ENDPOINT_HARDWARE_SUPPORT_VOLUME = 0x00000001;

		/// <summary>
		/// The audio endpoint device supports a hardware mute control.
		/// </summary>
		public const int ENDPOINT_HARDWARE_SUPPORT_MUTE = 0x00000002;

		/// <summary>
		/// The audio endpoint device supports a hardware peak meter.
		/// </summary>
		public const int ENDPOINT_HARDWARE_SUPPORT_METER = 0x00000004;
	}
}
