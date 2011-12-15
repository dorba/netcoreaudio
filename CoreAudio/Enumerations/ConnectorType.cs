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
	/// Indicates the type of connection that a connector is part of.
	/// </summary>
	/// <remarks>
	/// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370801.aspx
	/// </remarks>
	public enum ConnectorType
	{
		/// <summary>
		/// The connector is part of a connection of unknown type.
		/// </summary>
		Unknown_Connector,

		/// <summary>
		/// The connector is part of a physical connection to an auxiliary device that is installed inside the system chassis.
		/// </summary>
		Physical_Internal,

		/// <summary>
		/// The connector is part of a physical connection to an external device.
		/// </summary>
		Physical_External,

		/// <summary>
		/// The connector is part of a software-configured I/O connection.
		/// </summary>
		Software_IO,

		/// <summary>
		/// The connector is part of a permanent connection that is fixed and cannot be configured under software control.
		/// </summary>
		Software_Fixed,

		/// <summary>
		/// The connector is part of a connection to a network.
		/// </summary>
		Network 
	}
}
