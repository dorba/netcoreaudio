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
using Vannatech.CoreAudio.Interfaces;
using Vannatech.CoreAudio.Constants;
using System.Collections.Generic;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Externals;

namespace CoreAudioTests.EndpointVolumeApi
{
    /// <summary>
    /// Tests all methods of the IAudioEndpointVolumeEx interface.
    /// </summary>
    [TestClass]
    public class IAudioEndpointVolumeExTest : TestClass<IAudioEndpointVolumeEx>
    {
        /// <summary>
        /// Tests that the volume range can be received for each channel on each available endpoint.
        /// </summary>
        [TestMethod]
        public void IAudioEndpointVolumeEx_GetVolumeRangeChannel()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                activation.GetChannelCount(out count);

                for (int i = 0; i < count; i++)
                {
                    float volumeMin = 123.456f, volumeMax = 123.456f, volumeStep = 123.456f;
                    var result = activation.GetVolumeRangeChannel((UInt32)i, out volumeMin, out volumeMax, out volumeStep);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreNotEqual(123.456f, volumeMin, "The min volume value was not received.");
                    Assert.AreNotEqual(123.456f, volumeMax, "The max volume value was not received.");
                    Assert.AreNotEqual(123.456f, volumeStep, "The volume step value was not received.");
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("There were not channels available to test against.");
        }
    }
}
