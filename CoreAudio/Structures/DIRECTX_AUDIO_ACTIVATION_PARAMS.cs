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

namespace Vannatech.CoreAudio.Structures
{
    /// <summary>
    /// Specifies the initialization parameters for a DirectSound stream.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd370826.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct DIRECTX_AUDIO_ACTIVATION_PARAMS
    {
        /// <summary>
        /// The size in bytes of the structure.
        /// </summary>
        int ParamsSize;

        /// <summary>
        /// A GUID value that identifies the audio session that the stream belongs to.
        /// </summary>
        Guid AudioSession;

        /// <summary>
        /// Stream-initialization flags.
        /// </summary>
        int AudioStreamFlags;
    }
}
