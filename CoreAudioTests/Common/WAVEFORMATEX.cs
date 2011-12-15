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

namespace CoreAudioTests.Common
{
    /// <summary>
    /// Defines the format of waveform-audio data.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd757720.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct WAVEFORMATEX
    {
        /// <summary>
        /// Waveform-audio format type.
        /// </summary>
        public UInt16 wFormatTag;

        /// <summary>
        /// Number of audio channels.
        /// </summary>
        public UInt16 nChannels;

        /// <summary>
        /// Sample rate, in samples per second (hertz).
        /// </summary>
        public UInt32 nSamplesPerSec;

        /// <summary>
        /// The required average data-transfer rate, in bytes per second, for the format tag.
        /// </summary>
        public UInt32 nAvgBytesPerSec;

        /// <summary>
        /// Block alignment, in bytes.
        /// </summary>
        public UInt16 nBlockAlign;

        /// <summary>
        /// Bits per sample for the format type.
        /// </summary>
        public UInt16 wBitsPerSample;

        /// <summary>
        /// Size, in bytes, of extra format information appended to the end of the structure.
        /// </summary>
        public UInt16 cbSize;
    }
}
