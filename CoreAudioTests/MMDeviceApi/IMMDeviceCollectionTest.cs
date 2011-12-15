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
    /// Tests all methods of the IMMDeviceCollection interface.
    /// </summary>
    [TestClass]
    public class IMMDeviceCollectionTest
    {
        /// <summary>
        /// Tests that the individual render and capture device collections have a combined count equal to the total device count.
        /// </summary>
        [TestMethod]
        public void IMMDeviceCollection_GetCount()
        {
            int result = 0;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();
            
            IMMDeviceCollection allCaptureDevices;
            IMMDeviceCollection allRenderDevices;
            IMMDeviceCollection allDevices;

            enumerator.EnumAudioEndpoints(EDataFlow.eCapture, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL, out allCaptureDevices);
            enumerator.EnumAudioEndpoints(EDataFlow.eRender, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL, out allRenderDevices);
            enumerator.EnumAudioEndpoints(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL, out allDevices);

            Assert.IsNotNull(allCaptureDevices, "The IMMDeviceCollection object is null.");
            Assert.IsNotNull(allRenderDevices, "The IMMDeviceCollection object is null.");
            Assert.IsNotNull(allDevices, "The IMMDeviceCollection object is null.");

            UInt32 captureCount = UInt32.MaxValue, renderCount = UInt32.MaxValue, allCount = UInt32.MaxValue;

            result = allCaptureDevices.GetCount(out captureCount);
            AssertCoreAudio.IsHResultOk(result);
            Assert.AreNotEqual(UInt32.MaxValue, captureCount, "Device count was not received.");

            result = allRenderDevices.GetCount(out renderCount);
            AssertCoreAudio.IsHResultOk(result);
            Assert.AreNotEqual(UInt32.MaxValue, renderCount, "Device count was not received.");

            result = allDevices.GetCount(out allCount);
            AssertCoreAudio.IsHResultOk(result);
            Assert.AreNotEqual(UInt32.MaxValue, allDevices, "Device count was not received.");

            Assert.AreEqual(allCount, captureCount + renderCount, "The combined number of capture and render devices is not equal to the total device count.");
        }

        /// <summary>
        /// Tests the all devices from index zero to [count - 1] can be received with S_OK HRESULT and each device is not null.
        /// </summary>
        [TestMethod]
        public void IMMDeviceCollection_Item()
        {
            int result = 0;
            var enumerator = TestUtilities.CreateIMMDeviceEnumerator();

            IMMDeviceCollection allDevices;
            result = enumerator.EnumAudioEndpoints(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATEMASK_ALL, out allDevices);

            AssertCoreAudio.IsHResultOk(result);
            Assert.IsNotNull(allDevices, "The IMMDeviceCollection object is null");

            UInt32 count;
            allDevices.GetCount(out count);

            for (uint i = 0; i < count; i++)
            {
                IMMDevice device;
                result = allDevices.Item(i, out device);
                AssertCoreAudio.IsHResultOk(result);
            }
        }
    }
}
