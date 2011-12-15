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

namespace CoreAudioTests.Wasapi
{
    /// <summary>
    /// Tests all methods of the IAudioClockAdjustment interface.
    /// </summary>
    [TestClass]
    public class IAudioClockAdjustmentTest
    {
        /// <summary>
        /// Tests that the sampe rate may be set, for each applicable audio client.
        /// </summary>
        [TestMethod]
        public void IAudioClockAdjustment_SetSampleRate()
        {
            var result = 0;
            var tested = false;
            var audioClients = TestUtilities.CreateAudioClientServiceCollection<IAudioClockAdjustment>(
                ComIIDs.IAudioClockAdjustmentIID, false, AUDCLNT_STREAMFLAGS_XXX.AUDCLNT_STREAMFLAGS_RATEADJUST);

            try
            {
                foreach (var ac in audioClients)
                {
                    // Start the audio client.
                    result = ac.AudioClient.Start();
                    if (result != 0) continue;

                    // Slight delay is required.
                    System.Threading.Thread.Sleep(100);

                    float[] testRates = new float[] { 8000f, 11025f, 22050f, 32000f, 44100f, 48000f, 88200f, 96000f };

                    foreach (var rate in testRates)
                    {
                        result = ac.ServiceInterface.SetSampleRate(rate);
                        if ((uint)result == TestUtilities.HRESULTS.E_INVALIDARG) continue;

                        AssertCoreAudio.IsHResultOk(result);
                        tested = true;
                    }

                    ac.AudioClient.Stop();
                }
            }
            finally
            {
                foreach (var ac in audioClients)
                    ac.Dispose();
            }

            if (!tested) Assert.Inconclusive("No valid sample rates could be found for any available audio client.");
        }
    }
}