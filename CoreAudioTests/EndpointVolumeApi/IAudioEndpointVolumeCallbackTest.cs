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
using Vannatech.CoreAudio.Structures;

namespace CoreAudioTests.EndpointVolumeApi
{
    /// <summary>
    /// Tests all methods of the IAudioEndpointVolumeCallback interface.
    /// </summary>
    [TestClass]
    public class IAudioEndpointVolumeCallbackTest : TestClass<IAudioEndpointVolume>
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolumeCallback_OnNotify()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var context = Guid.NewGuid();
                var client = new AudioEndpointVolumeCallback();
                activation.RegisterControlChangeNotify(client);

                client.SetExpected("MuteState", new AUDIO_VOLUME_NOTIFICATION_DATA
                {
                    EventContext = context,
                    IsMuted = true
                });

                System.Threading.Thread.Sleep(100);
                activation.SetMute(true, context);

                var loopCount = 0;
                while (client.Result == -1 && loopCount < 20)
                {
                    System.Threading.Thread.Sleep(100);
                    loopCount++;
                }

                if (client.Result == -1)
                    Assert.Inconclusive("The result was not received.");
                else if (client.Result == 1)
                    Assert.Fail("The client did not receive the correct notification.");

                client.SetExpected("MasterVolume", new AUDIO_VOLUME_NOTIFICATION_DATA
                {
                    EventContext = context,
                    MasterVolume = -10.0f
                });

                System.Threading.Thread.Sleep(100);
                activation.SetMasterVolumeLevel(-10.0f, context);

                loopCount = 0;
                while (client.Result == -1 && loopCount < 20)
                {
                    System.Threading.Thread.Sleep(100);
                    loopCount++;
                }

                if (client.Result == -1)
                    Assert.Inconclusive("The result was not received.");
                else if (client.Result == 1)
                    Assert.Fail("The client did not receive the correct notification.");

                activation.UnregisterControlChangeNotify(client);
            });
        }
    }

    /// <summary>
    /// Local class used to test methods that require an object implementing IAudioEndpointVolumeCallback
    /// </summary>
    internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
    {
        private bool _tested;
        private bool _passed;
        private string _type;
        private AUDIO_VOLUME_NOTIFICATION_DATA _expected;

        public int OnNotify(IntPtr dataPtr)
        {
            if(String.IsNullOrEmpty(_type)) return 0;
            var localType = _type;
            _type = null;
            _tested = true;

            AUDIO_VOLUME_NOTIFICATION_DATA notificationData = (AUDIO_VOLUME_NOTIFICATION_DATA)System.Runtime.InteropServices.Marshal.PtrToStructure(dataPtr, typeof(AUDIO_VOLUME_NOTIFICATION_DATA));

            Assert.AreEqual(_expected.EventContext, notificationData.EventContext);

            switch (localType)
            {
                case "MuteState":
                    Assert.AreEqual(_expected.IsMuted, notificationData.IsMuted);
                    break;

                case "MasterVolume":
                    Assert.AreEqual(_expected.MasterVolume, notificationData.MasterVolume);
                    break;
            }

            _passed = true;
            return 0;
        }

        public void SetExpected(string type, AUDIO_VOLUME_NOTIFICATION_DATA expected)
        {
            _type = type;
            _expected = expected;
            _tested = false;
            _passed = false;
        }

        public int Result
        {
            get 
            {
                if (!_tested) return -1;
                else if (_tested && _passed) return 0;
                else return 1;
            }
        }
    }
}