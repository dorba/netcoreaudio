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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioClient interface.
    /// </summary>
    [TestClass]
    public class IAudioClientTest : TestClass<IAudioClient>
    {
        /// <summary>
        /// Tests that the buffer size may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetBufferSize()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var size = UInt32.MaxValue;
                var result = ac.GetBufferSize(out size);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, size, "The size was not received.");
            });
        }

        /// <summary>
        /// Tests that the buffer size may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetCurrentPadding()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var count = UInt32.MaxValue;
                var result = ac.GetCurrentPadding(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, count, "The padding frame count was not received.");
            });
        }

        /// <summary>
        /// Tests that the device period values may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetDevicePeriod()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var procInterval = UInt64.MaxValue;
                var minInterval = UInt64.MaxValue;
                var result = ac.GetDevicePeriod(out procInterval, out minInterval);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt64.MaxValue, procInterval, "The process interval was not received.");
                Assert.AreNotEqual(UInt64.MaxValue, minInterval, "The minimum interval was not received.");
            });
        }

        /// <summary>
        /// Tests that the mix format may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetMixFormat()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var formatPtr = IntPtr.Zero;
                var result = ac.GetMixFormat(out formatPtr);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(IntPtr.Zero, formatPtr, "The mix format pointer was not received.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetService()
        {
            // Testing of GetService method is implied, as this method is required for all other tests.
            // TODO: Low priority - add tests here to specifically get each available service.
        }

        /// <summary>
        /// Tests that the stream latency may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_GetStreamLatency()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var latency = UInt64.MaxValue;
                var result = ac.GetStreamLatency(out latency);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt64.MaxValue, latency, "The stream latency was not received.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioClient_Initialize()
        {
            // Testing of Initialize method is implied, as this method is required for all other tests.
            // TODO: Low priority - add tests produce WASAPI errors, anad verify expected error codes.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioClient_IsFormatSupported()
        {
            // Testing of IsFormatSupported method is implied, as this method is required for all other tests.
            // TODO: Low priority - add a more robust test with a wide range of formats.
        }

        /// <summary>
        /// Tests that the Start method executes properly, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_Reset()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                ac.Start();
                var result = ac.Reset();
                Assert.AreNotEqual(0, result, "The audio client successfully reset, but was not stopped.");

                ac.Stop();
                result = ac.Reset();
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioClient_SetEventHandle()
        {
            Assert.Fail("TODO: Implement test for SetEventHandle method");
        }

        /// <summary>
        /// Tests that the Start method executes properly, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_Start()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                var result = ac.Start();
                AssertCoreAudio.IsHResultOk(result);

                result = ac.Start();
                Assert.AreNotEqual(0, result, "The audio client successfully started, but was already running.");
            });
        }

        /// <summary>
        /// Tests that the Stop method executes properly, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClient_Stop()
        {
            ExecuteCustomTest(new AudioClientTestManager(), ac =>
            {
                ac.Start();
                var result = ac.Stop();
                AssertCoreAudio.IsHResultOk(result);

                // A second call to Stop should fail.
                result = ac.Stop();
                Assert.AreNotEqual(0, result, "The audio client successfully stopped, but was not started.");
            });
        }

        /// <summary>
        /// Test manager specific to audio client testing.
        /// </summary>
        private class AudioClientTestManager : DeviceActivationTestManager<IAudioClient>
        {
            /// <summary>
            /// Creates a new instance of the class.
            /// </summary>
            public AudioClientTestManager()
                : base(TestUtilities.CreateDeviceActivationCollection<IAudioClient>(ComIIDs.IAudioClientIID))
            {
            }

            protected override void OnRun()
            {
                // Begins with shared mode testing
                // Then runs again in exclusive mode
                var shareMode = AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_SHARED;

            testStart:
                var tested = false;

                try
                {
                    foreach (var activation in Items)
                    {
                        var formatPtr = TestUtilities.GetFormatPointer(activation.ActiveInterface, shareMode);
                        if (formatPtr == IntPtr.Zero) continue;

                        var context = Guid.NewGuid();
                        var init = activation.ActiveInterface.Initialize(shareMode, 0, 5000000, 0, formatPtr, context);
                        if (init != 0) continue;

                        OnTestReady(activation.ActiveInterface);
                        tested = true;
                    }
                }
                finally
                {
                    foreach (var activation in Items)
                        EnsureDisposal(activation);
                }

                if (!tested) Assert.Inconclusive("No valid audio clients were tested.");

                if (shareMode == AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_SHARED)
                {
                    // Get items and retest in exclusive mode.
                    Items = TestUtilities.CreateDeviceActivationCollection<IAudioClient>(ComIIDs.IAudioClientIID);
                    shareMode = AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_EXCLUSIVE;
                    goto testStart;
                }
            }
        }
    }
}