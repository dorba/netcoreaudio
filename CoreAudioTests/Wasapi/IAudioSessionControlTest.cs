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
    /// Tests all methods of the IAudioSessionControl interface.
    /// </summary>
    [TestClass]
    public class IAudioSessionControlTest : TestClass<IAudioSessionControl>
    {
        /// <summary>
        /// Tests that the icon path may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetIconPath()
        {
            ExecuteServiceTest(service =>
            {
                string iconPath = "abc123";
                var result = service.GetIconPath(out iconPath);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", iconPath, "The icon path was not received.");
            });
        }

        /// <summary>
        /// Tests that the display name may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetDisplayName()
        {
            ExecuteServiceTest(service =>
            {
                string displayName = "abc123";
                var result = service.GetDisplayName(out displayName);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", displayName, "The display name was not received.");
            });
        }

        /// <summary>
        /// Tests that the grouping parameter may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetGroupingParam()
        {
            ExecuteServiceTest(service =>
            {
                var groupingId = Guid.NewGuid();
                var guidOrig = new Guid(groupingId.ToByteArray());

                var result = service.GetGroupingParam(out groupingId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(guidOrig, groupingId, "The grouping ID was not received.");
            });
        }

        /// <summary>
        /// This method is not applicable.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetLastActivation()
        {
            // This method is not defined in audiopolicy.h file, but is on MSDN.
            // It appears this was removed when moving from Vista to Win7.
        }

        /// <summary>
        /// This method is not applicable.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetLastInactivation()
        {
            // This method is not defined in audiopolicy.h file, but is on MSDN.
            // It appears this was removed when moving from Vista to Win7.
        }

        /// <summary>
        /// Tests that the state may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_GetState()
        {
            ExecuteServiceTest(service =>
            {
                var stateOne = AudioSessionState.AudioSessionStateActive;
                var stateTwo = AudioSessionState.AudioSessionStateInactive;

                var result = service.GetState(out stateOne);
                AssertCoreAudio.IsHResultOk(result);
                result = service.GetState(out stateTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(stateOne, stateTwo, "The state was not received.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_RegisterAudioSessionNotification()
        {
            Assert.Fail("TODO: Implement test for RegisterAudioSessionNotification method");
        }

        /// <summary>
        /// Tests that the display name may be set, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_SetDisplayName()
        {
            ExecuteServiceTest(service =>
            {
                var result = service.SetDisplayName("abc123", Guid.NewGuid());
                AssertCoreAudio.IsHResultOk(result);

                string nameVerify;
                service.GetDisplayName(out nameVerify);
                Assert.AreEqual("abc123", nameVerify, "The display name was not set.");
            });
        }

        /// <summary>
        /// Tests that the grouping parameter may be set, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_SetGroupingParam()
        {
            ExecuteServiceTest(service =>
            {
                var groupingId = Guid.NewGuid();
                var result = service.SetGroupingParam(groupingId, Guid.NewGuid());
                AssertCoreAudio.IsHResultOk(result);

                Guid guidVerify;
                service.GetGroupingParam(out guidVerify);
                Assert.AreEqual(groupingId, guidVerify, "The grouping ID was not set.");
            });
        }

        /// <summary>
        /// Tests that the icon path may be set, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_SetIconPath()
        {
            ExecuteServiceTest(service =>
            {
                var result = service.SetIconPath("abc123", Guid.NewGuid());
                AssertCoreAudio.IsHResultOk(result);

                string pathVerify;
                service.GetIconPath(out pathVerify);
                Assert.AreEqual("abc123", pathVerify, "The icon path was not set.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionControl_UnregisterAudioSessionNotification()
        {
            Assert.Fail("TODO: Implement test for UnregisterAudioSessionNotification method");
        }
    }
}