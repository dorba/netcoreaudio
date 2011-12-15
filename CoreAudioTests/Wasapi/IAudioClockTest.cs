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

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioClock interface.
    /// </summary>
    [TestClass]
    public class IAudioClockTest : TestClass<IAudioClock>
    {
        /// <summary>
        /// Tests nothing, the GetCharacteristics method is reserved for future use.
        /// </summary>
        [TestMethod]
        public void IAudioClock_GetCharacteristics()
        {
            // Testing is not applicable. This method is reserved for future use.
        }

        /// <summary>
        /// Tests that the frequency may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClock_GetFrequency()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                UInt64 frequency = UInt64.MaxValue;
                var result = runningService.GetFrequency(out frequency);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt64.MaxValue, frequency, "The frequency was not received.");
            });
        }

        /// <summary>
        /// Tests that the position may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClock_GetPosition()
        {
            ExecuteRunningServiceTest(runningService =>
            {
                UInt64 devicePos = UInt64.MaxValue;
                UInt64 counterPos = UInt64.MaxValue;
                var result = runningService.GetPosition(out devicePos, out counterPos);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt64.MaxValue, devicePos, "The device position was not received.");
                Assert.AreNotEqual(UInt64.MaxValue, counterPos, "The counter position was not received.");
            });
        }
    }
}