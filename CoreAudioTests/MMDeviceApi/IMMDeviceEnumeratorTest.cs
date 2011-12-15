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
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.MMDeviceApi
{
    /// <summary>
    /// Tests all methods of the IMMDeviceEnumerator interface.
    /// </summary>
    [TestClass]
    public class IMMDeviceEnumeratorTest
    {
        /// <summary>
        /// This test method does nothing. Testing of the EnumAudioEndpoints method is implicit by testing other aspects of the IMMDevice API.
        /// </summary>
        [TestMethod]
        public void IMMDeviceEnumerator_EnumAudioEndpoints()
        {
            // This method is thouroughly tested through various other unit tests.
            // The entry point for most other tests starts with calling EnumAudioEndpoints.
            // TODO: Add specific test for this.
        }

        /// <summary>
        /// Tests that the default audio endpoint for all combinations of data flow and roles can be created with S_OK HRESULT and that each device is not null.
        /// </summary>
        [TestMethod]
        public void IMMDeviceEnumerator_GetDefaultAudioEndpoint()
        {
            int result = 0;
            IMMDevice device = null;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();

            // data flow - eAll (this should always produce HRESULT of E_INVALIDARG, which is 0x80070057)
            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eAll, ERole.eCommunications, out device);
            Assert.AreEqual(0x80070057, (uint)result);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eAll, ERole.eConsole, out device);
            Assert.AreEqual(0x80070057, (uint)result);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eAll, ERole.eMultimedia, out device);
            Assert.AreEqual(0x80070057, (uint)result);

            // data flow - eCapture
            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eCommunications, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eConsole, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);

            // data flow - eRender
            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eCommunications, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eConsole, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);

            result = enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out device);
            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(device);
        }

        /// <summary>
        /// Tests that the GetDevice method can get each audio device individually, by ID.
        /// </summary>
        [TestMethod]
        public void IMMDeviceEnumerator_GetDevice()
        {
            int result = 0;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();
            var allDevices = TestUtilities.CreateIMMDeviceCollection(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL);

            foreach (var device in allDevices)
            {
                // Get the device ID.
                string deviceId = null;
                result = device.GetId(out deviceId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(deviceId, "The device string is null.");

                // Get the IMMDevice directly from the ID.
                IMMDevice deviceFromId = null;
                result = enumerator.GetDevice(deviceId, out deviceFromId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(deviceFromId, "The IMMDevice object is null.");

                // Ensure the IDs of each device match.
                string deviceId2 = null;
                result = deviceFromId.GetId(out deviceId2);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(deviceId2, "The device string is null.");

                Assert.AreEqual(deviceId, deviceId2, "The device IDs are not equal.");
            }
        }

        /// <summary>
        /// Tests that a valid client can be registered and an HRESULT of S_OK is returned.
        /// </summary>
        [TestMethod]
        public void IMMDeviceEnumerator_RegisterEndpointNotificationCallback()
        {
            int result = 0;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();

            var client = new MMDeviceNotifyClient();
            result = enumerator.RegisterEndpointNotificationCallback(client);
            AssertCoreAudio.IsHResultOk(result);
        }

        /// <summary>
        /// Tests that a previously registered client can be unregistered with HRESULT of S_OK. Also tests that unregistration of an invalid client will fail.
        /// </summary>
        [TestMethod]
        public void IMMDeviceEnumerator_UnregisterEndpointNotificationCallback()
        {
            int result = 0;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();

            // Test for unregistering a valid client.
            var client = new MMDeviceNotifyClient();
            result = enumerator.RegisterEndpointNotificationCallback(client);
            AssertCoreAudio.IsHResultOk(result);

            result = enumerator.UnregisterEndpointNotificationCallback(client);
            AssertCoreAudio.IsHResultOk(result);

            // Test for unregistering a non-registered client (should fail with HRESULT of ELEMENT_NOT_FOUND).
            result = enumerator.UnregisterEndpointNotificationCallback(new MMDeviceNotifyClient());
            Assert.AreEqual(TestUtilities.HRESULTS.E_NOTFOUND, (uint)result);
        }
    }
}
