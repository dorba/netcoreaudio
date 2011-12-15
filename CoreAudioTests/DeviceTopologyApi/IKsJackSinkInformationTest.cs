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

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IKsJackSinkInformation interface.
    /// </summary>
    [TestClass]
    public class IKsJackSinkInformationTest : TestClass<IKsJackSinkInformation>
    {
        /// <summary>
        /// Tests that the sink information may be received, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IKsJackSinkInformation_GetJackSinkInformation()
        {
            ExecutePartActivationTest(activation =>
            {
                Assert.Fail("TODO: Must update IKsJackSinkInformation interface before valid unit test can be created.");
                var info = new KSJACK_SINK_INFORMATION();
                var result = activation.GetJackSinkInformation(out info);

                AssertCoreAudio.IsHResultOk(result);
            });
        }
    }
}