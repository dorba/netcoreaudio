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
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioStreamVolume interface.
    /// </summary>
    [TestClass]
    public class IAudioStreamVolumeTest : TestClass<IAudioStreamVolume>
    {
        /// <summary>
        /// Tests that all channel volumes may be received, for each available service.
        /// </summary>
        [TestMethod]
        public void IAudioStreamVolume_GetAllVolumes()
        {
            ExecuteServiceTest(service =>
            {
                UInt32 channelCount;
                service.GetChannelCount(out channelCount);

                var volumes = Enumerable.Repeat(123.456f, (int)channelCount).ToArray();
                var result = service.GetAllVolumes(channelCount, volumes);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsFalse(volumes.Any(val => val == 123.456f), "One or more volumes were not received.");
            });
        }

        /// <summary>
        /// Tests that the channel count may be received, for each available service.
        /// </summary>
        [TestMethod]
        public void IAudioStreamVolume_GetChannelCount()
        {
            ExecuteServiceTest(service =>
            {
                var channelCount = UInt32.MaxValue;
                var result = service.GetChannelCount(out channelCount);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, channelCount, "The channel count was not received.");
            });
        }

        /// <summary>
        /// Tests that individual channel volumes may be received, for each available service.
        /// </summary>
        [TestMethod]
        public void IAudioStreamVolume_GetChannelVolume()
        {
            ExecuteServiceTest(service =>
            {
                UInt32 channelCount;
                service.GetChannelCount(out channelCount);

                for (uint i = 0; i < channelCount; i++)
                {
                    var volume = 123.456f;
                    var result = service.GetChannelVolume(i, out volume);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, volume, "The channel volume was not received.");
                }
            });
        }

        /// <summary>
        /// Tests that all volumes may be set, for each available service.
        /// </summary>
        [TestMethod]
        public void IAudioStreamVolume_SetAllVolumes()
        {
            ExecuteServiceTest(service =>
            {
                UInt32 channelCount;
                service.GetChannelCount(out channelCount);

                float[] valOrig = new float[channelCount];
                service.GetAllVolumes(channelCount, valOrig);

                // Set to test values.
                var result = service.SetAllVolumes(channelCount, Enumerable.Repeat(0.5f, (int)channelCount).ToArray());
                AssertCoreAudio.IsHResultOk(result);

                // Check new values.
                float[] valNew = new float[channelCount];
                service.GetAllVolumes(channelCount, valNew);
                Assert.IsFalse(valNew.Any(val => val != 0.5f), "One or more volumes were not set.");

                // Return to original value.
                result = service.SetAllVolumes(channelCount, valOrig);
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that individual channel volumes may be set, for each available service.
        /// </summary>
        [TestMethod]
        public void IAudioStreamVolume_SetChannelVolume()
        {
            ExecuteServiceTest(service =>
            {
                UInt32 channelCount;
                service.GetChannelCount(out channelCount);

                for (uint i = 0; i < channelCount; i++)
                {
                    float valOrig;
                    service.GetChannelVolume(i, out valOrig);

                    // Set to test value.
                    var result = service.SetChannelVolume(i, 0.5f);
                    AssertCoreAudio.IsHResultOk(result);

                    // Check new value.
                    float valNew;
                    service.GetChannelVolume(i, out valNew);
                    Assert.AreEqual(0.5f, valNew, "The channel volume was not set.");

                    // Return to original value.
                    result = service.SetChannelVolume(i, valOrig);
                    AssertCoreAudio.IsHResultOk(result);
                }
            });
        }
    }
}