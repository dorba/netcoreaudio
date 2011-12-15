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
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.MMDeviceApi
{
    /// <summary>
    /// Tests all methods of the IMMEndpoint interface.
    /// </summary>
    [TestClass]
    public class IMMEndpointTest
    {
        /// <summary>
        /// Tests that an IMMDevice object can be cast to IMMEndpoint and the method returns a valid data flow, with HRESULT of S_OK.
        /// </summary>
        [TestMethod]
        public void IMMEndpoint_GetDataFlow()
        {
            int result = 0;
            var allDevices = TestUtilities.CreateIMMDeviceCollection(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL);

            foreach (var device in allDevices)
            {
                // Cast compiles to QueryInterface call.
                var endpoint = (IMMEndpoint)device;
                Assert.IsNotNull(endpoint);

                EDataFlow dataFlow = EDataFlow.eAll;
                result = endpoint.GetDataFlow(out dataFlow);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(EDataFlow.eAll, dataFlow);
            }
        }
    }
}
