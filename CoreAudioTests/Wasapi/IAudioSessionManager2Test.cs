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
using System.Runtime.InteropServices;

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioSessionManager2 interface.
    /// </summary>
    [TestClass]
    public class IAudioSessionManager2Test : TestClass<IAudioSessionManager2>
    {
        /// <summary>
        /// Tests that a session enumerator interface may be received, for each available session manager.
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager2_GetSessionEnumerator()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                IAudioSessionEnumerator sessionList;
                var result = activation.GetSessionEnumerator(out sessionList);
                AssertCoreAudio.IsHResultOk(result);
                Manager.EnsureDisposal(sessionList);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager2_RegisterDuckNotification()
        {
            Assert.Fail("TODO: Implement test for RegisterDuckNotification method");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager2_RegisterSessionNotification()
        {
            Assert.Fail("TODO: Implement test for RegisterSessionNotification method");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager2_UnregisterDuckNotification()
        {
            Assert.Fail("TODO: Implement test for UnregisterDuckNotification method");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager2_UnregisterSessionNotification()
        {
            Assert.Fail("TODO: Implement test for UnregisterSessionNotification method");
        }
    }
}