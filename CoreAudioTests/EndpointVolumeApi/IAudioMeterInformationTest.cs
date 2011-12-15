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
using System.Linq;
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Interfaces;
using Vannatech.CoreAudio.Constants;

namespace CoreAudioTests.EndpointVolumeApi
{
    /// <summary>
    /// Tests all methods of the IAudioMeterInformation interface.
    /// </summary>
    [TestClass]
    public class IAudioMeterInformationTest : TestClass<IAudioMeterInformation>
    {
        /// <summary>
        /// Tests that the peak value can be received for each channel, on each available device.
        /// </summary>
        [TestMethod]
        public void IAudioMeterInformation_GetChannelsPeakValues()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                UInt32 count;
                activation.GetMeteringChannelCount(out count);

                var peaks = Enumerable.Repeat(123.456f, (int)count).ToArray();
                var result = activation.GetChannelsPeakValues(count, peaks);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsFalse(peaks.Any(f => f == 123.456f), "One or more channel values was not received.");
            });
        }

        /// <summary>
        /// Tests that the metering channel count can be received, for each available device.
        /// </summary>
        [TestMethod]
        public void IAudioMeterInformation_GetMeteringChannelCount()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                var result = activation.GetMeteringChannelCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(uint.MaxValue, count);
            });
        }

        /// <summary>
        /// Tests that the peak value can be received, for each available device.
        /// </summary>
        [TestMethod]
        public void IAudioMeterInformation_GetPeakValue()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var peak = 123.456f;
                var result = activation.GetPeakValue(out peak);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(123.456f, peak);
            });
        }

        /// <summary>
        /// Tests that the hardware support mask can be received and is within the valid range, for each available device.
        /// </summary>
        [TestMethod]
        public void IAudioMeterInformation_QueryHardwareSupport()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                uint mask = uint.MaxValue;
                var result = activation.QueryHardwareSupport(out mask);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsTrue((mask >= 0) && (mask <= 7), "The hardware mask is not in the valid range.");
            });
        }
    }
}