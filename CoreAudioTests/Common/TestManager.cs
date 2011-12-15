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
    /// Delegate used to handle a test.
    /// </summary>
    /// <typeparam name="T">The interface type.</typeparam>
    /// <param name="testInterface">The interface instance.</param>
    public delegate void TestReadyDelegate<T>(T testInterface);

    /// <summary>
    /// Base class for all test managers.
    /// </summary>
    /// <typeparam name="T">The interface type.</typeparam>
    public abstract class TestManager<T>
    {
        // Internal collection of disposable items.
        private List<object> _disposables;

        /// <summary>
        /// The method called when a test is ready to be run.
        /// </summary>
        public TestReadyDelegate<T> OnTestReady
        {
            get;
            set;
        }

        /// <summary>
        /// Creats a new instance of the class.
        /// </summary>
        public TestManager()
        {
            _disposables = new List<object>();
        }

        /// <summary>
        /// Ensures that either IDisposable objects or COM objects are properly cleaned up.
        /// </summary>
        /// <param name="disposable">The object to dispose of.</param>
        public void EnsureDisposal(object obj)
        {
            var disposable = obj as IDisposable;
            if(obj != null || (disposable.GetType().Name.Contains("ComObject")))
                _disposables.Add(obj);
        }

        /// <summary>
        /// Begins running the tests.
        /// </summary>
        public void Run()
        {
            if (OnTestReady == null)
                throw new InvalidOperationException("The OnTestReady property must be set");

            try
            {
                OnRun();
            }
            finally
            {
                foreach (var d in _disposables)
                {
                    var d2 = d as IDisposable;
                    if (d2 != null) d2.Dispose();
                    else Marshal.FinalReleaseComObject(d);
                }
            }
        }

        /// <summary>
        /// Method that implements the actual test iteration.
        /// </summary>
        protected abstract void OnRun();
    }
}
