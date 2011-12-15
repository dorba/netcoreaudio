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
	/// Provides access to jack sink information if the jack is supported by the hardware.
    /// </summary>
    /// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd371393.aspx
    /// </remarks>
	public partial interface IKsJackSinkInformation
    {
		/// <summary>
		/// Retrieves the sink information for the specified jack. 
		/// </summary>
		/// <param name="information">Receives a structure of type <see cref="KSJACK_SINK_INFORMATION"/> that contains the jack sink information.</param>
		/// <returns>An HRESULT code indicating whether the operation succeeded of failed.</returns>
		[PreserveSig]
		int GetJackSinkInformation(
			[Out] out KSJACK_SINK_INFORMATION information); // TODO: This will have to be changed to an IntPtr. See KSJACK_SINK_INFORMATION.cs for details.
    }
}
