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
    /// Tests all methods of the ISimpleAudioVolume interface.
    /// </summary>
    [TestClass]
    public class ISimpleAudioVolumeTest : TestClass<ISimpleAudioVolume>
    {
        /// <summary>
        /// Tests that the master volume may be received, for each available service.
        /// </summary>
        [TestMethod]
        public void ISimpleAudioVolume_GetMasterVolume()
        {
            ExecuteServiceTest(service =>
            {
                var volume = 123.456f;
                var result = service.GetMasterVolume(out volume);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(123.456f, volume, "The master volume was not received.");
            });
        }

        /// <summary>
        /// Tests that the master volume may be received, for each available service.
        /// </summary>
        [TestMethod]
        public void ISimpleAudioVolume_GetMute()
        {
            ExecuteServiceTest(service =>
            {
                var valOne = true;
                var valTwo = false;

                var result = service.GetMute(out valOne);
                AssertCoreAudio.IsHResultOk(result);

                result = service.GetMute(out valTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(valOne, valTwo, "The mute state was not received.");
            });
        }

        /// <summary>
        /// Tests that the master volume may be set, for each available service.
        /// </summary>
        [TestMethod]
        public void ISimpleAudioVolume_SetMasterVolume()
        {
            ExecuteServiceTest(service =>
            {
                var context = Guid.NewGuid();
                float valOrig, valNew;
                service.GetMasterVolume(out valOrig);

                // Set to test value.
                var result = service.SetMasterVolume(0.5f, context);
                AssertCoreAudio.IsHResultOk(result);
                service.GetMasterVolume(out valNew);
                Assert.AreEqual(0.5f, valNew, "The master volume was not set.");

                // Return to original value.
                result = service.SetMasterVolume(valOrig, context);
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that the mute state may be set, for each available service.
        /// </summary>
        [TestMethod]
        public void ISimpleAudioVolume_SetMute()
        {
            ExecuteServiceTest(service =>
            {
                var context = Guid.NewGuid();
                bool valOrig, valNew;
                service.GetMute(out valOrig);

                // Test set mute to on.
                var result = service.SetMute(true, context);
                AssertCoreAudio.IsHResultOk(result);
                service.GetMute(out valNew);
                Assert.AreEqual(true, valNew, "The mute state was not set.");

                // Test set mute to off.
                result = service.SetMute(false, context);
                AssertCoreAudio.IsHResultOk(result);
                service.GetMute(out valNew);
                Assert.AreEqual(false, valNew, "The mute state was not set.");

                // Return to original value.
                result = service.SetMute(valOrig, context);
                AssertCoreAudio.IsHResultOk(result);
            });
        }
    }
}