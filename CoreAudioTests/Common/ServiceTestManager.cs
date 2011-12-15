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
    /// Test manager used to run tests within the context of a AudioClientService object.
    /// </summary>
    /// <typeparam name="T">The interface type.</typeparam>
    public class ServiceTestManager<T> : TestManager<T>
    {
        /// <summary>
        /// Creats a new test manager instance.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="comIId">The COM IID of the interface.</param>
        /// <returns>A new service test manager instance.</returns>
        public static ServiceTestManager<T> Create(string comIId)
        {
            return new ServiceTestManager<T>(TestUtilities.CreateAudioClientServiceCollection<T>(comIId));
        }

        /// <summary>
        /// Creats a new test manager instance.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="comIId">The COM IID of the interface.</param>
        /// <param name="exclusiveMode">A value indicating whether or not to use exclusive mode.</param>
        /// <returns>A new service test manager instance.</returns>
        public static ServiceTestManager<T> Create(string comIId, bool exclusiveMode)
        {
            return new ServiceTestManager<T>(TestUtilities.CreateAudioClientServiceCollection<T>(comIId, exclusiveMode));
        }

        /// <summary>
        /// Creats a new test manager instance.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <param name="comIId">The COM IID of the interface.</param>
        /// <param name="exclusiveMode">A value indicating whether or not to use exclusive mode.</param>
        /// <param name="streamFlags">The stream flags to use during initialization.</param>
        /// <returns>A new service test manager instance.</returns>
        public static ServiceTestManager<T> Create(string comIId, bool exclusiveMode, UInt32 streamFlags)
        {
            return new ServiceTestManager<T>(TestUtilities.CreateAudioClientServiceCollection<T>(comIId, exclusiveMode, streamFlags));
        }

        /// <summary>
        /// A list of audio client service objects to test against.
        /// </summary>
        protected IEnumerable<AudioClientService<T>> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether or not to start the audio client before running a test.
        /// </summary>
        public bool AutoStartClient
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="items">A list of device activations to use.</param>
        public ServiceTestManager(IEnumerable<AudioClientService<T>> items)
        {
            Items = items;
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
        protected override void OnRun()
        {
            var isTested = false;

            foreach (var i in Items)
            {
                try
                {
                    var isRunning = false;

                    if (AutoStartClient)
                    {
                        var startResult = i.AudioClient.Start();
                        if (startResult != 0) continue;

                        // Slight delay is required.
                        System.Threading.Thread.Sleep(100);
                        isRunning = true;
                    }

                    OnTestReady(i.ServiceInterface);

                    if (isRunning) i.AudioClient.Stop();
                    isTested = true;
                }
                finally
                {
                    EnsureDisposal(i);
                }
            }

            if (!isTested) Assert.Inconclusive("No valid services were found to test against.");
        }
    }
}
