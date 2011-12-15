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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Externals;

namespace CoreAudioTests.Common
{
    /// <summary>
    /// Extensions for assert class to handle CoreAudio API specifics.
    /// </summary>
    public static class AssertCoreAudio
    {
        /// <summary>
        /// Verifies the HRESULT is S_OK.
        /// </summary>
        /// <param name="hResult">The HRESULT value.</param>
        public static void IsHResultOk(int hResult)
        {
            Assert.AreEqual(0, hResult, "The request did not return a successful HRESULT code.");
        }

        /// <summary>
        /// Verifies the a returned device state is a valid value.
        /// </summary>
        /// <param name="deviceState">The device state.</param>
        public static void IsDeviceStateValid(UInt32 deviceState)
        {
            bool isValid = (deviceState == DEVICE_STATE_XXX.DEVICE_STATE_ACTIVE ||
                deviceState == DEVICE_STATE_XXX.DEVICE_STATE_DISABLED ||
                deviceState == DEVICE_STATE_XXX.DEVICE_STATE_NOTPRESENT ||
                deviceState == DEVICE_STATE_XXX.DEVICE_STATE_UNPLUGGED);

            Assert.IsTrue(isValid, "The device state is not valid.");
        }

        /// <summary>
        /// Verifies the returned property key is valid.
        /// </summary>
        /// <param name="propertyKey">The property key.</param>
        public static void IsPropertyKeyValid(PROPERTYKEY propertyKey)
        {
            Assert.IsNotNull(propertyKey, "The property key is null.");
            Assert.AreNotEqual(Guid.Empty, propertyKey.fmtid, "The property key format ID is not valid.");
            Assert.AreNotEqual(0, propertyKey.pid, "The property key property ID is not valid.");
        }
    }
}
