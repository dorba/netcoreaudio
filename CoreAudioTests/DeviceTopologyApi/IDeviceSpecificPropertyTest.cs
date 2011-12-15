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

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IDeviceSpecificProperty interface.
    /// </summary>
    [TestClass]
    public class IDeviceSpecificPropertyTest : TestClass<IDeviceSpecificProperty>
    {
        /// <summary>
        /// Test that the property value range and step may be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IDeviceSpecificProperty_Get4BRange()
        {
            ExecutePartActivationTest(activation =>
            {
                Int32 pMin = Int32.MaxValue, pMax = Int32.MinValue, pStep = Int32.MinValue;
                var result = activation.Get4BRange(out pMin, out pMax, out pStep);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(Int32.MaxValue, pMin, "The minimum property value was not received.");
                Assert.AreNotEqual(Int32.MinValue, pMax, "The maximum property value was not received.");
                Assert.AreNotEqual(Int32.MinValue, pStep, "The property step value was not received.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IDeviceSpecificProperty_GetType()
        {
            ExecutePartActivationTest(activation =>
            {
                Assert.Fail("TODO: Determine how to properly test this method.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IDeviceSpecificProperty_GetValue()
        {
            ExecutePartActivationTest(activation =>
            {
                Assert.Fail("TODO: Determine how to properly test this method.");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IDeviceSpecificProperty_SetValue()
        {
            ExecutePartActivationTest(activation =>
            {
                Assert.Fail("TODO: Determine how to properly test this method.");
            });
        }
    }
}