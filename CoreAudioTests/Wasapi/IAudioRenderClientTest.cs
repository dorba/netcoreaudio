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
    /// Tests all methods of the IAudioRenderClient interface.
    /// </summary>
    [TestClass]
    public class IAudioRenderClientTest : TestClass<IAudioRenderClient>
    {
        /// <summary>
        /// Tests that buffers may be received, for each applicable endpoint in the system.
        /// </summary>
        [TestMethod]
        public void IAudioRenderClient_GetBuffer()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                var bufferPtr = IntPtr.Zero;
                var frameCount = UInt32.MaxValue;

                var result = runningService.GetBuffer(128, out bufferPtr);
                runningService.ReleaseBuffer(frameCount, 0);

                if (TestUtilities.IsWasapiError(result))
                    return;

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(IntPtr.Zero, bufferPtr, "The buffer pointer was not received.");
            });
        }

        /// <summary>
        /// Tests that buffers may be released, for each applicable endpoint in the system.
        /// </summary>
        [TestMethod]
        public void IAudioRenderClient_ReleaseBuffer()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                var bufferPtr = IntPtr.Zero;
                var frameCount = UInt32.MaxValue;

                runningService.GetBuffer(128, out bufferPtr);
                var result = runningService.ReleaseBuffer(frameCount, 0);

                if (TestUtilities.IsWasapiError(result))
                    return;

                AssertCoreAudio.IsHResultOk(result);
            });
        }
    }
}