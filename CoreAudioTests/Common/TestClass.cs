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
    /// Base class for tests that utilize MMDevice Activation or AudioClient Services.
    /// </summary>
    /// <typeparam name="T">The interface type.</typeparam>
    public abstract class TestClass<T>
    {
        /// <summary>
        /// The COM IID.
        /// </summary>
        protected virtual string ComIID
        {
            get
            {
                var interfaceName = typeof(T).Name;
                var fieldInfo = typeof(ComIIDs).GetField(interfaceName + "IID");

                if (fieldInfo == null)
                    throw new ArgumentException("Could not find the COM IID for the specified ");

                return fieldInfo.GetValue(null) as string;
            }
        }

        /// <summary>
        /// Gets the test manager used to run tests.
        /// </summary>
        protected TestManager<T> Manager
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs tests via a custom test manager.
        /// </summary>
        /// <param name="testManager">The test manager.</param>
        /// <param name="onTestReady">The test method to use.</param>
        protected void ExecuteCustomTest(TestManager<T> testManager, TestReadyDelegate<T> onTestReady)
        {
            Manager = testManager;
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IMMDevice.Activate method.
        /// </summary>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecuteDeviceActivationTest(TestReadyDelegate<T> onTestReady)
        {
            Manager = DeviceActivationTestManager<T>.Create(ComIID);
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IPart.Activate method.
        /// </summary>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecutePartActivationTest(TestReadyDelegate<T> onTestReady)
        {
            Manager = PartActivationTestManager<T>.Create(ComIID);
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IAudioClient.GetService method.
        /// Tests are run in both shared and exclusive mode.
        /// </summary>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecuteServiceTest(TestReadyDelegate<T> onTestReady)
        {
            Manager = ServiceTestManager<T>.Create(ComIID, false);
            Manager.OnTestReady = onTestReady;
            Manager.Run();

            Manager = ServiceTestManager<T>.Create(ComIID, true);
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IAudioClient.GetService method.
        /// Tests are run only in the specified share mode.
        /// </summary>
        /// <param name="shareMode">The audio client share mode.</param>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecuteServiceTest(AUDCLNT_SHAREMODE shareMode, TestReadyDelegate<T> onTestReady)
        {
            var exclusiveMode = (shareMode == AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_EXCLUSIVE);

            Manager = ServiceTestManager<T>.Create(ComIID, exclusiveMode);
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IAudioClient.GetService method.
        /// Tests are run in both shared and exclusive mode, with the audio client in the running (started) state.
        /// </summary>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecuteRunningServiceTest(TestReadyDelegate<T> onTestReady)
        {
            var serviceTestManager = ServiceTestManager<T>.Create(ComIID, false);
            serviceTestManager.AutoStartClient = true;

            Manager = serviceTestManager;
            Manager.OnTestReady = onTestReady;
            Manager.Run();

            serviceTestManager = ServiceTestManager<T>.Create(ComIID, true);
            serviceTestManager.AutoStartClient = true;

            Manager = serviceTestManager;
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }

        /// <summary>
        /// Runs each test, creating the specified interface from the IAudioClient.GetService method.
        /// Tests are run only in the specified share mode, with the audio client in the running (started) state.
        /// </summary>
        /// <param name="shareMode">The audio client share mode.</param>
        /// <param name="onTestReady">The test method to use.</param>
        protected virtual void ExecuteRunningServiceTest(AUDCLNT_SHAREMODE shareMode, TestReadyDelegate<T> onTestReady)
        {
            var exclusiveMode = (shareMode == AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_EXCLUSIVE);
            var serviceTestManager = ServiceTestManager<T>.Create(ComIID, exclusiveMode);
            serviceTestManager.AutoStartClient = true;

            Manager = serviceTestManager;
            Manager.OnTestReady = onTestReady;
            Manager.Run();
        }
    }
}
