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
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Externals;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.Common
{
    /// <summary>
    /// Test manager used to run tests within the context of an MMDeviceActivation object.
    /// </summary>
    /// <typeparam name="T">The interface type.</typeparam>
    public class DeviceActivationTestManager<T> : TestManager<T>
    {
        /// <summary>
        /// Creats a new test manager instance.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="comIId">The COM IID of the interface.</param>
        /// <returns>A new activation test manager instance.</returns>
        public static DeviceActivationTestManager<T> Create(string comIId)
        {
            return new DeviceActivationTestManager<T>(TestUtilities.CreateDeviceActivationCollection<T>(comIId));
        }

        /// <summary>
        /// A list of device activation objects to test against.
        /// </summary>
        protected IEnumerable<DeviceActivation<T>> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="items">A list of device activations to use.</param>
        public DeviceActivationTestManager(IEnumerable<DeviceActivation<T>> items)
        {
            Items = items;
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
        protected override void OnRun()
        {
            foreach (var i in Items)
            {
                try
                {
                    OnTestReady(i.ActiveInterface);
                }
                finally
                {
                    EnsureDisposal(i);
                }
            }
        }
    }
}
