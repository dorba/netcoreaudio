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
	/// Indicate characteristics of an audio session associated with the stream.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370789.aspx
	/// </remarks>
	public class AUDCLNT_SESSIONFLAGS_XXX
	{
		/// <summary>
		/// The session expires when there are no associated streams and owning session control objects holding references.
		/// </summary>
		public const int AUDCLNT_SESSIONFLAGS_EXPIREWHENUNOWNED = 0x10000000;

		/// <summary>
		/// The volume control is hidden in the volume mixer user interface when the audio session is created.
		/// </summary>
		public const int AUDCLNT_SESSIONFLAGS_DISPLAY_HIDE = 0x20000000;

		/// <summary>
		/// The volume control is hidden in the volume mixer user interface after the session expires.
		/// </summary>
		public const int AUDCLNT_SESSIONFLAGS_DISPLAY_HIDEWHENEXPIRED = 0x40000000;
	}
}
