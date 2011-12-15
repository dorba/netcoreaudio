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
    /// Tests all methods of the IAudioSessionEnumerator interface.
    /// </summary>
    /// <remarks>
    /// This test class uses the context of IAudioSessionManager2 because
    /// that interface must be used to retrieve the IAudioSessionEnumerator instance.
    /// </remarks>
    [TestClass]
    public class IAudioSessionEnumeratorTest : TestClass<IAudioSessionManager2>
    {
        /// <summary>
        /// Tests that the count may be received, for each available session enumerator.
        /// </summary>
        [TestMethod]
        public void IAudioSessionEnumerator_GetCount()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                IAudioSessionEnumerator sessionList;
                var result = activation.GetSessionEnumerator(out sessionList);
                if (result != 0) return;
                Manager.EnsureDisposal(sessionList);

                var count = Int32.MinValue;
                result = sessionList.GetCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(Int32.MinValue, count, "The count was not received.");
                tested = true;
            });

            if (!tested) Assert.Inconclusive("No valid session enumerator interfaces could be created.");
        }

        /// <summary>
        /// Tests that each specific session may be received, for each available session enumerator.
        /// </summary>
        [TestMethod]
        public void IAudioSessionEnumerator_GetSession()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                IAudioSessionEnumerator sessionList;
                var result = activation.GetSessionEnumerator(out sessionList);
                if (result != 0) return;
                Manager.EnsureDisposal(sessionList);

                Int32 count;
                sessionList.GetCount(out count);

                for (int i = 0; i < count; i++)
                {
                    IAudioSessionControl session;
                    result = sessionList.GetSession(i, out session);

                    AssertCoreAudio.IsHResultOk(result);
                    Manager.EnsureDisposal(session);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("No valid session enumerator interfaces could be created.");
        }
    }
}