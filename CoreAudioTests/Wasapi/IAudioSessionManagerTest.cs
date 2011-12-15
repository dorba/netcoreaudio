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
    /// Tests all methods of the IAudioSessionManager interface.
    /// </summary>
    [TestClass]
    public class IAudioSessionManagerTest : TestClass<IAudioSessionManager>
    {
        /// <summary>
        /// Tests that the audio session control may be received, for each available session manager.
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager_GetAudioSessionControl()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                IAudioSessionControl sessionControl;
                var result = activation.GetAudioSessionControl(Guid.NewGuid(), 0, out sessionControl);
                AssertCoreAudio.IsHResultOk(result);
                Manager.EnsureDisposal(sessionControl);
            });
        }

        /// <summary>
        /// Tests that the simple volume interface may be received, for each available session manager.
        /// </summary>
        [TestMethod]
        public void IAudioSessionManager_GetSimpleAudioVolume()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                ISimpleAudioVolume audioVolume;
                var result = activation.GetSimpleAudioVolume(Guid.NewGuid(), 0, out audioVolume);
                AssertCoreAudio.IsHResultOk(result);
                Manager.EnsureDisposal(audioVolume);
            });
        }
    }
}