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
    /// Defines constants that indicate the direction in which audio data flows between an audio endpoint device and an application.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370828.aspx
    /// </remarks>
    public enum EDataFlow
    {
        /// <summary>
        /// Audio data flows from the application to the audio endpoint device, which renders the stream.
        /// </summary>
        eRender = 0,

        /// <summary>
        /// Audio data flows from the audio endpoint device that captures the stream, to the application.
        /// </summary>
        eCapture = 1,

        /// <summary>
        /// Audio data can flow either from the application to the audio endpoint device, or from the audio endpoint device to the application.
        /// </summary>
        eAll = 2
    }
}
