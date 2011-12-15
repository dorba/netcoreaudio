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
    /// Defines the format of waveform-audio data for formats having more than two channels.
    /// </summary>
    /// <remarks>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/dd757721.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct WAVEFORMATEXTENSIBLE
    {
        /// <summary>
        /// The <see cref="WAVEFORMATEX"/> structure that specifies the basic format.
        /// </summary>
        [FieldOffset(0)]
        public WAVEFORMATEX Format;

        /// <summary>
        /// Variant data, which may contain either the valid bits per sample, or samples per block.
        /// </summary>
        [FieldOffset(18)]
        public SamplesUnion Samples;

        /// <summary>
        /// Bitmask specifying the assignment of channels in the stream to speaker positions.
        /// </summary>
        [FieldOffset(20)]
        public UInt32 dwChannelMask;

        /// <summary>
        /// Subformat of the data.
        /// </summary>
        [FieldOffset(24)]
        public Guid SubFormat;

        /// <summary>
        /// Defines possible values of variant data for the <see cref="WAVEFORMATEXTENSIBLE"/> Samples member.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct SamplesUnion
        {
            /// <summary>
            /// The number of bits of precision in the signal.
            /// </summary>
            [FieldOffset(0)]
            public UInt16 wValidBitsPerSample;

            /// <summary>
            /// The number of samples contained in one compressed block of audio data.
            /// </summary>
            [FieldOffset(0)]
            public UInt16 wSamplesPerBlock;

            /// <summary>
            /// Reserved for internal use by operating system.
            /// </summary>
            [FieldOffset(0)]
            public UInt16 wReserved;
        }
    }
}
