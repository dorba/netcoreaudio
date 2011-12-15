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
    /// Tests all methods of the IAudioClock2 interface.
    /// </summary>
    [TestClass]
    public class IAudioClock2Test
    {
        /// <summary>
        /// Tests that the device position may be received, for each available audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClock2_GetDevicePosition()
        {
            var result = 0;
            var tested = false;
            var exclusive = false;

        //beginTest:
            var audioClients = TestUtilities.CreateAudioClientServiceCollection<IAudioClock>(ComIIDs.IAudioClockIID, exclusive);

            try
            {
                foreach (var ac in audioClients)
                {
                    // Start the audio client.
                    result = ac.AudioClient.Start();
                    if (result != 0) continue;

                    // Slight delay is required.
                    System.Threading.Thread.Sleep(100);

                    UInt64 devicePos = UInt64.MaxValue;
                    UInt64 counterPos = UInt64.MaxValue;

                    var clock2 = (IAudioClock2)ac.ServiceInterface;
                    result = clock2.GetDevicePosition(out devicePos, out counterPos);
                    ac.AudioClient.Stop();

                    // Not all IAudioClock instances support IAudioClock2.
                    if ((uint)result == TestUtilities.HRESULTS.E_NOINTERFACE) continue;

                    AssertCoreAudio.IsHResultOk(result);
                    // TODO: Determine if these tests need to exist. Method doesn't always set the output variables.
                    //Assert.AreNotEqual(UInt64.MaxValue, devicePos, (exclusive ? "Exclusive Mode: " : "Shared Mode: ") + "The device position was not received.");
                    //Assert.AreNotEqual(UInt64.MaxValue, counterPos, (exclusive ? "Exclusive Mode: " : "Shared Mode: ") + "The counter position was not received.");
                    tested = true;
                }
            }
            finally
            {
                foreach (var ac in audioClients)
                    ac.Dispose();
            }

            if (!tested) Assert.Inconclusive((exclusive ? "Exclusive Mode: " : "Shared Mode: ") + "No valid calls to GetDevicePosition were made without WASAPI errors.");

            // TODO: Determine if this should work in exclusive mode. All calls to QueryInterface (from IAudioClock to IAudioClock2) currently return E_NOINTERFACE.
            //if (!exclusive)
            //{
            //    // Rerun test in exclusive mode.
            //    tested = false;
            //    exclusive = true;
            //    goto beginTest;
            //}
        }
    }
}