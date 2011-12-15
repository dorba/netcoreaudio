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
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Interfaces;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioCaptureClient interface.
    /// </summary>
    [TestClass]
    public class IAudioCaptureClientTest : TestClass<IAudioCaptureClient>
    {
        /// <summary>
        /// Tests that buffers and related data may be received, for each applicable endpoint in the system.
        /// </summary>
        [TestMethod]
        public void IAudioCaptureClient_GetBuffer()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                var bufferPtr = IntPtr.Zero;
                var frameCount = UInt32.MaxValue;
                var devicePosition = UInt64.MaxValue;
                var counterPosition = UInt64.MaxValue;
                AUDCLNT_BUFFERFLAGS bufferFlags;

                var result = runningService.GetBuffer(out bufferPtr, out frameCount, out bufferFlags, out devicePosition, out counterPosition);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(IntPtr.Zero, bufferPtr, "The buffer pointer was not received.");
                Assert.AreNotEqual(UInt32.MaxValue, frameCount, "The frame count was not received.");
                Assert.AreNotEqual(UInt64.MaxValue, devicePosition, "The device position was not received.");
                Assert.AreNotEqual(UInt64.MaxValue, counterPosition, "The counter position was not received.");
            });
        }

        /// <summary>
        /// Tests that the size of the next packet may be received, for each applicable endpoint in the system.
        /// </summary>
        [TestMethod]
        public void IAudioCaptureClient_GetNextPacketSize()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                var nextBufferSize = UInt32.MaxValue;
                var bufferPtr = IntPtr.Zero;
                var frameCount = UInt32.MaxValue;
                var devicePosition = UInt64.MaxValue;
                var counterPosition = UInt64.MaxValue;
                AUDCLNT_BUFFERFLAGS bufferFlags;

                var result = runningService.GetNextPacketSize(out nextBufferSize);
                runningService.GetBuffer(out bufferPtr, out frameCount, out bufferFlags, out devicePosition, out counterPosition);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, nextBufferSize, "The frame count was not received.");
                Assert.AreEqual(frameCount, nextBufferSize, "The actual frame count did not equal the promised buffer size.");
            });
        }

        /// <summary>
        /// Tests that a buffer may be released, for each applicable endpoint in the system.
        /// </summary>
        [TestMethod]
        public void IAudioCaptureClient_ReleaseBuffer()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                var bufferPtr = IntPtr.Zero;
                var frameCount = UInt32.MaxValue;
                var devicePosition = UInt64.MaxValue;
                var counterPosition = UInt64.MaxValue;
                AUDCLNT_BUFFERFLAGS bufferFlags;

                runningService.GetBuffer(out bufferPtr, out frameCount, out bufferFlags, out devicePosition, out counterPosition);
                var result = runningService.ReleaseBuffer(frameCount);

                AssertCoreAudio.IsHResultOk(result);
            });
        }
    }
}