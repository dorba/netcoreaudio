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
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioSessionControl2 interface.
    /// </summary>
    /// <remarks>
    /// This test class uses the context of IAudioSessionControl because
    /// that interface must be used to retrieve the IAudioSessionControl2 instance.
    /// </remarks>
    [TestClass]
    public class IAudioSessionControl2Test : TestClass<IAudioSessionControl>
    {
        /// <summary>
        /// Tests that the process ID can be received, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl2_GetProcessId()
        {
            var tested = false;

            ExecuteServiceTest(service =>
            {
                var sc2 = ToAudioSessionControl2(service);
                if (sc2 == null) return;
                Manager.EnsureDisposal(sc2);

                var processId = UInt32.MaxValue;
                var result = sc2.GetProcessId(out processId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, processId, "The process ID was not received.");
                tested = true;
            });

            if (!tested) Assert.Inconclusive("No audio clients were found which supported the IAudioSessionControl2 interface.");
        }

        /// <summary>
        /// Tests that the session ID can be received, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl2_GetSessionIdentifier()
        {
            var tested = false;

            ExecuteServiceTest(service =>
            {
                var sc2 = ToAudioSessionControl2(service);
                if (sc2 == null) return;
                Manager.EnsureDisposal(sc2);

                var sessionId = "abc123";
                var result = sc2.GetSessionIdentifier(out sessionId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", sessionId, "The session ID was not received.");
                tested = true;
            });

            if (!tested) Assert.Inconclusive("No audio clients were found which supported the IAudioSessionControl2 interface.");
        }

        /// <summary>
        /// Tests that the session instance ID can be received, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl2_GetSessionInstanceIdentifier()
        {
            var tested = false;

            ExecuteServiceTest(service =>
            {
                var sc2 = ToAudioSessionControl2(service);
                if (sc2 == null) return;
                Manager.EnsureDisposal(sc2);

                var instanceId = "abc123";
                var result = sc2.GetSessionInstanceIdentifier(out instanceId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", instanceId, "The session instance ID was not received.");
                tested = true;
            });

            if (!tested) Assert.Inconclusive("No audio clients were found which supported the IAudioSessionControl2 interface.");
        }

        /// <summary>
        /// Tests that the system sound boolean flag can be received, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl2_IsSystemSoundsSession()
        {
            var tested = false;

            ExecuteServiceTest(service =>
            {
                var sc2 = ToAudioSessionControl2(service);
                if (sc2 == null) return;
                Manager.EnsureDisposal(sc2);

                var result = sc2.IsSystemSoundsSession();

                Assert.IsTrue(result == 0 || result == 1, "The boolean flag is not within the valid range.");
                tested = true;
            });

            if (!tested) Assert.Inconclusive("No audio clients were found which supported the IAudioSessionControl2 interface.");
        }

        /// <summary>
        /// Tests that the ducking preference can be set, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl2_SetDuckingPreference()
        {
            var tested = false;

            ExecuteServiceTest(service =>
            {
                var sc2 = ToAudioSessionControl2(service);
                if (sc2 == null) return;
                Manager.EnsureDisposal(sc2);

                var result = sc2.SetDuckingPreference(true);
                
                // Check for wrong endpoint type.
                // This method call is only valid for render devices.
                if ((uint)result == 0x88890003) return;

                AssertCoreAudio.IsHResultOk(result);
                result = sc2.SetDuckingPreference(false);
                AssertCoreAudio.IsHResultOk(result);

                tested = true;
            });

            if (!tested) Assert.Inconclusive("No audio clients were found which supported the IAudioSessionControl2 interface.");
        }

        /// <summary>
        /// Converts IAudioSessionControl to IAudioSessionControl2.
        /// </summary>
        /// <param name="sessionControl">The original interface.</param>
        /// <returns>A valid IAudioSessionControl2 instance, or null if one cannot be created.</returns>
        private IAudioSessionControl2 ToAudioSessionControl2(IAudioSessionControl sessionControl)
        {
            var sc2 = (IAudioSessionControl2)sessionControl;
            var result = sc2.IsSystemSoundsSession();

            if ((uint)result == TestUtilities.HRESULTS.E_NOINTERFACE)
                return null;

            return sc2;
        }
    }
}