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
    /// Tests all methods of the IAudioEndpointVolume interface.
    /// </summary>
    [TestClass]
    public class IAudioEndpointVolumeTest : TestClass<IAudioEndpointVolume>
    {
        /// <summary>
        /// Tests that the channel count can be received for each available endpoint, and an HRESULT of S_OK is returned.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetChannelCount()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                var result = activation.GetChannelCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(uint.MaxValue, count, "The channel count value was not received.");
            });
        }

        /// <summary>
        /// Tests that the volume level can be obtained for each available endpoint channel.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetChannelVolumeLevel()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    float level = 123.456f;
                    var result = activation.GetChannelVolumeLevel(i, out level);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, level, "The level value was not received.");
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        /// <summary>
        /// Tests that the scalar volume level can be obtained for each available endpoint channel.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetChannelVolumeLevelScalar()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    float level = 123.456f;
                    var result = activation.GetChannelVolumeLevelScalar(i, out level);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, level, "The level value was not received.");
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        /// <summary>
        /// Tests that the master volume level can be obtained for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetMasterVolumeLevel()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                float level = 123.456f;
                var result = activation.GetMasterVolumeLevel(out level);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(123.456f, level, "The level value was not received.");
            });
        }

        /// <summary>
        /// Tests that the master scalar volume level can be obtained for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetMasterVolumeLevelScalar()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                float level = 123.456f;
                var result = activation.GetMasterVolumeLevelScalar(out level);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(123.456f, level, "The level value was not received.");
            });
        }

        /// <summary>
        /// Tests that the muting state can be received for each available endpoint, with an HRESULT of S_OK returned.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetMute()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                bool valOne = true, valTwo = false;
                
                var result = activation.GetMute(out valOne);
                AssertCoreAudio.IsHResultOk(result);

                result = activation.GetMute(out valTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(valOne, valTwo, "The mute state was not received.");
            });
        }

        /// <summary>
        /// Tests that the volume ranges for each available endpoint can be received.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetVolumeRange()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                float volumeMin = 123.456f, volumeMax = 123.456f, volumeStep = 123.456f;
                var result = activation.GetVolumeRange(out volumeMin, out volumeMax, out volumeStep);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(123.456f, volumeMin, "The min volume value was not received.");
                Assert.AreNotEqual(123.456f, volumeMax, "The max volume value was not received.");
                Assert.AreNotEqual(123.456f, volumeStep, "The volume step value was not received.");
            });
        }
        
        /// <summary>
        /// Tests that the volume step info for each available endpoint can be received.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_GetVolumeStepInfo()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var step = UInt32.MaxValue;
                var stepCount = UInt32.MaxValue;
                var result = activation.GetVolumeStepInfo(out step, out stepCount);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, step, "The step value was not received.");
                Assert.AreNotEqual(UInt32.MaxValue, stepCount, "The step count value was not received.");
            });
        }

        /// <summary>
        /// Tests that the hardware support flags for each available endpoint can be received, and are within a valid range.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_QueryHardwareSupport()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var mask = UInt32.MaxValue;
                var result = activation.QueryHardwareSupport(out mask);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsTrue((mask >= 0) && (mask <= 7), "The hardware mask is not in the valid range.");

            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_RegisterControlChangeNotify()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var client = new AudioEndpointVolumeCallback();
                var result = activation.RegisterControlChangeNotify(client);
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that the volume level can be set for each channel on each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_SetChannelVolumeLevel()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                UInt32 count;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    // determine valid range.
                    float volumeMin, volumeMax, volumeStep;
                    activation.GetVolumeRange(out volumeMin, out volumeMax, out volumeStep);

                    // get the original level.
                    float levelOrig, levelNew, levelCheck;
                    activation.GetChannelVolumeLevel(i, out levelOrig);

                    // create a random valid level
                    var rand = new Random();
                    levelNew = (float)rand.Next((int)volumeMin, (int)volumeMax);

                    // set the new level.
                    Guid context = Guid.NewGuid();
                    var result = activation.SetChannelVolumeLevel(i, levelNew, context);
                    AssertCoreAudio.IsHResultOk(result);

                    // check that the level was set.
                    activation.GetChannelVolumeLevel(i, out levelCheck);
                    Assert.AreEqual(levelNew, levelCheck, volumeStep, "The new volume level was not set properly.");

                    // reset the level to the original.
                    result = activation.SetChannelVolumeLevel(i, levelOrig, context);
                    AssertCoreAudio.IsHResultOk(result);
                    tested = true;
                }
            });
            
            if(!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        /// <summary>
        /// Tests that the scalar volume level can be set for each channel on each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_SetChannelVolumeLevelScalar()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                UInt32 count;
                activation.GetChannelCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    // get the original level.
                    float levelOrig, levelNew, levelCheck;
                    activation.GetChannelVolumeLevelScalar(i, out levelOrig);

                    // create a random valid level
                    var rand = new Random();
                    levelNew = (float)rand.NextDouble();

                    // set the new level.
                    Guid context = Guid.NewGuid();
                    var result = activation.SetChannelVolumeLevelScalar(i, levelNew, context);
                    AssertCoreAudio.IsHResultOk(result);

                    // check that the level was set.
                    activation.GetChannelVolumeLevelScalar(i, out levelCheck);
                    Assert.AreEqual(levelNew, levelCheck, 0.001f, "The new volume level was not set properly.");

                    // reset the level to the original.
                    result = activation.SetChannelVolumeLevelScalar(i, levelOrig, context);
                    AssertCoreAudio.IsHResultOk(result);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No channels were available to test against.");
        }

        /// <summary>
        /// Tests that the master volume level can be set for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_SetMasterVolumeLevel()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                // determine valid range.
                float volumeMin, volumeMax, volumeStep;
                activation.GetVolumeRange(out volumeMin, out volumeMax, out volumeStep);

                // get the original level.
                float levelOrig, levelNew, levelCheck;
                activation.GetMasterVolumeLevel(out levelOrig);

                // create a random valid level
                var rand = new Random();
                levelNew = (float)rand.Next((int)volumeMin, (int)volumeMax);

                // set the new level.
                Guid context = Guid.NewGuid();
                var result = activation.SetMasterVolumeLevel(levelNew, context);
                AssertCoreAudio.IsHResultOk(result);

                // check that the level was set.
                activation.GetMasterVolumeLevel(out levelCheck);
                Assert.AreEqual(levelNew, levelCheck, volumeStep, "The new volume level was not set properly.");

                // reset the level to the original.
                result = activation.SetMasterVolumeLevel(levelOrig, context);
            });
        }

        /// <summary>
        /// Tests that the master scalar volume level can be set for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_SetMasterVolumeLevelScalar()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                // get the original level.
                float levelOrig, levelNew, levelCheck;
                activation.GetMasterVolumeLevelScalar(out levelOrig);

                // create a random valid level
                var rand = new Random();
                levelNew = (float)rand.NextDouble();

                // set the new level.
                Guid context = Guid.NewGuid();
                var result = activation.SetMasterVolumeLevelScalar(levelNew, context);
                AssertCoreAudio.IsHResultOk(result);

                // check that the level was set.
                activation.GetMasterVolumeLevelScalar(out levelCheck);
                Assert.AreEqual(levelNew, levelCheck, 0.001f, "The new volume level was not set properly.");

                // reset the level to the original.
                result = activation.SetMasterVolumeLevelScalar(levelOrig, context);
            });
        }

        /// <summary>
        /// Tests that the muting state can be set for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_SetMute()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                // get the original mute state.
                bool muteOrig, muteNew, muteCheck;
                activation.GetMute(out muteOrig);
                muteNew = !muteOrig;

                // change it to the opposite.
                Guid context = Guid.NewGuid();
                var result = activation.SetMute(muteNew, context);
                AssertCoreAudio.IsHResultOk(result);

                // check that the value changed.
                activation.GetMute(out muteCheck);
                Assert.AreEqual(muteNew, muteCheck, "The muting state was not set properly.");

                // return to original state.
                result = activation.SetMute(muteOrig, context);
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_UnregisterControlChangeNotify()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var client = new AudioEndpointVolumeCallback();
                activation.RegisterControlChangeNotify(client);
                var result = activation.UnregisterControlChangeNotify(client);
                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that the volume step down decrements the step by one, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_VolumeStepDown()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                // get the original value.
                float levelOrig;
                activation.GetMasterVolumeLevelScalar(out levelOrig);

                // set to a volume in the middle.
                Guid context = Guid.NewGuid();
                activation.SetMasterVolumeLevelScalar(0.5f, context);

                // get the initial step info
                UInt32 step, stepCount;
                activation.GetVolumeStepInfo(out step, out stepCount);

                // determine valid number of steps to take.
                uint stepsToTake = (uint)Math.Floor(stepCount / 2.0f);

                while (stepsToTake > 0)
                {
                    // step down the volume.
                    var result = activation.VolumeStepDown(context);
                    AssertCoreAudio.IsHResultOk(result);

                    // check that the volume step decremented one.
                    var previousStep = step;
                    activation.GetVolumeStepInfo(out step, out stepCount);
                    Assert.AreEqual(previousStep - 1, step, "The volume step did not decrement.");

                    stepsToTake--;
                }

                // reset the volume level to original value.
                activation.SetMasterVolumeLevelScalar(levelOrig, context);
            });
        }

        /// <summary>
        /// Tests that the volume step up increments the step by one, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolume_VolumeStepUp()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                // get the original value.
                float levelOrig;
                activation.GetMasterVolumeLevelScalar(out levelOrig);

                // set to a volume in the middle.
                Guid context = Guid.NewGuid();
                activation.SetMasterVolumeLevelScalar(0.5f, context);

                // get the initial step info
                UInt32 step, stepCount;
                activation.GetVolumeStepInfo(out step, out stepCount);

                // determine valid number of steps to take.
                uint stepsToTake = (uint)Math.Floor(stepCount / 2.0f);

                while (stepsToTake > 0)
                {
                    // step up the volume.
                    var result = activation.VolumeStepUp(context);
                    AssertCoreAudio.IsHResultOk(result);

                    // check that the volume step decremented one.
                    uint previousStep = step;
                    activation.GetVolumeStepInfo(out step, out stepCount);
                    Assert.AreEqual(previousStep + 1, step, "The volume step did not increment.");

                    stepsToTake--;
                }

                // reset the volume level to original value.
                activation.SetMasterVolumeLevelScalar(levelOrig, context);
            });
        }
    }
}
