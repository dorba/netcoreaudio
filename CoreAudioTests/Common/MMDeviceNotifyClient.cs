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
    /// Used for testing endpoint notification methods.
    /// </summary>
    public class MMDeviceNotifyClient : IMMNotificationClient
    {
        public void OnDefaultDeviceChanged(EDataFlow dataFlow, ERole deviceRole, string defaultDeviceId)
        {
            Assert.AreEqual(_dataFlow, dataFlow);
            Assert.AreEqual(_role, deviceRole);
            Assert.AreEqual(_deviceId, defaultDeviceId);
        }

        public void OnDeviceAdded(string deviceId)
        {
            Assert.AreEqual(_deviceId, deviceId);
        }

        public void OnDeviceRemoved(string deviceId)
        {
            Assert.AreEqual(_deviceId, deviceId);
        }

        public void OnDeviceStateChanged(string deviceId, UInt32 newState)
        {
            Assert.AreEqual(_deviceId, deviceId);
            Assert.AreEqual(_newState, newState);
        }

        public void OnPropertyValueChanged(string deviceId, PROPERTYKEY propertyKey)
        {
            Assert.AreEqual(_deviceId, deviceId);
            Assert.AreEqual(_propertyKey, propertyKey);
        }

        // Below members are not part of the interface
        // And are only used for verifying correctness.

        private EDataFlow _dataFlow;
        private ERole _role;
        private string _deviceId;
        private int _newState;
        private PROPERTYKEY _propertyKey;

        internal void SetExpected(EDataFlow dataflow, ERole role, string deviceId, int newState, PROPERTYKEY propertyKey)
        {
            _dataFlow = dataflow;
            _role = role;
            _deviceId = deviceId;
            _newState = newState;
            _propertyKey = propertyKey;
        }
    }
}