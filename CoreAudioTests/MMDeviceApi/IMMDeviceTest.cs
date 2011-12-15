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
using System.Runtime.InteropServices;
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Constants;
using Vannatech.CoreAudio.Enumerations;
using Vannatech.CoreAudio.Externals;
using Vannatech.CoreAudio.Interfaces;

namespace CoreAudioTests.MMDeviceApi
{
    /// <summary>
    /// Tests all methods of the IMMDevice interface.
    /// </summary>
    [TestClass]
    public class IMMDeviceTest
    {
        /// <summary>
        /// This test ensures that each device can use any valid COM interface returned from the Activate method.
        /// It checks to make sure each received interface is not null and an HRESULT of S_OK is returned.
        /// </summary>
        [TestMethod]
        public void IMMDevice_Activate()
        {
            var devices = TestUtilities.CreateIMMDeviceCollection(EDataFlow.eAll, DEVICE_STATE_XXX.DEVICE_STATE_ACTIVE);

            foreach (var d in devices)
            {
                var iid = Guid.Empty;
                var result = -1;
                object objInterface = null;

                // Check IAudioClient
                iid = new Guid(ComIIDs.IAudioClientIID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IAudioClient);

                // Check IAudioEndpointVolume
                iid = new Guid(ComIIDs.IAudioEndpointVolumeIID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IAudioEndpointVolume);

                // Check IAudioMeterInformation
                iid = new Guid(ComIIDs.IAudioMeterInformationIID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IAudioMeterInformation);

                // Check IAudioSessionManager
                iid = new Guid(ComIIDs.IAudioSessionManagerIID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IAudioSessionManager);

                // Check IAudioSessionManager2
                iid = new Guid(ComIIDs.IAudioSessionManager2IID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IAudioSessionManager2);

                // Check IDeviceTopology
                iid = new Guid(ComIIDs.IDeviceTopologyIID);
                result = d.Activate(iid, (uint)CLSCTX.CLSCTX_INPROC_SERVER, IntPtr.Zero, out objInterface);
                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(objInterface as IDeviceTopology);
            }
        }

        /// <summary>
        /// This test ensures that each device can get its ID. It also checks that the received ID is not null.
        /// </summary>
        [TestMethod]
        public void IMMDevice_GetId()
        {
            var devices = TestUtilities.CreateIMMDeviceCollection();

            foreach (var d in devices)
            {
                string strId;
                var result = d.GetId(out strId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.IsNotNull(strId);
            }
        }

        /// <summary>
        /// This test ensures that each device can get its state. It also checks that the received state is a valid device state constant.
        /// </summary>
        [TestMethod]
        public void IMMDevice_GetState()
        {
            var devices = TestUtilities.CreateIMMDeviceCollection();

            foreach (var d in devices)
            {
                UInt32 deviceState;
                var result = d.GetState(out deviceState);

                AssertCoreAudio.IsHResultOk(result);
                AssertCoreAudio.IsDeviceStateValid(deviceState);
            }
        }

        /// <summary>
        /// This test ensures that each device can open a property store in READWRITE mode and that the received property store is non-null.
        /// It also checks that the property store object works correctly by making a call to get the property count.
        /// </summary>
        [TestMethod]
        public void IMMDevice_OpenPropertyStore()
        {
            var tested = false;
            var devices = TestUtilities.CreateIMMDeviceCollection();

            foreach (var d in devices)
            {
                // Open the property store
                IPropertyStore propertyStore;
                var result = d.OpenPropertyStore(STGM.STGM_READ, out propertyStore);
                AssertCoreAudio.IsHResultOk(result);
                
                // Verify the count can be received.
                var propertyCount = UInt32.MaxValue;
                result = propertyStore.GetCount(out propertyCount);
                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, propertyCount, "The property count was not received.");

                // Get each property key, then get value.
                for (uint i = 0; i < propertyCount; i++)
                {
                    PROPERTYKEY propertyKey;
                    result = propertyStore.GetAt(i, out propertyKey);
                    AssertCoreAudio.IsHResultOk(result);

                    var value = GetPropertyValue(propertyStore, propertyKey);
                    
                    if (value != null)
                        tested = true;
                }
            }

            if (!tested) Assert.Inconclusive("No property values returned valid, non-null values.");
        }

        private object GetPropertyValue(IPropertyStore propertyStore, PROPERTYKEY propertyKey)
        {
            object returnObj = null;
            PROPVARIANT propVariant;
            var result = propertyStore.GetValue(ref propertyKey, out propVariant);
            AssertCoreAudio.IsHResultOk(result);

            var vType = (VarEnum)propVariant.vt;
            if (vType == VarEnum.VT_EMPTY)
                return null;

            switch (vType)
            {
                case VarEnum.VT_BOOL:
                    returnObj = propVariant.Data.AsBoolean;
                    break;
                case VarEnum.VT_UI4:
                    returnObj = propVariant.Data.AsUInt32;
                    break;
                case VarEnum.VT_LPWSTR:
                case VarEnum.VT_CLSID:
                    returnObj = Marshal.PtrToStringUni(propVariant.Data.AsStringPtr);
                    break;
                case VarEnum.VT_BLOB:
                    returnObj = propVariant.Data.AsFormatPtr;
                    break;
            }

            if (propertyKey.fmtid == PropertyKeys.PKEY_AudioEngine_DeviceFormat ||
                propertyKey.fmtid == PropertyKeys.PKEY_AudioEngine_OEMFormat)
            {
                Assert.AreEqual(VarEnum.VT_BLOB, vType, "The device format property was not of varient type VT_BLOB.");
                var format = (WAVEFORMATEX)Marshal.PtrToStructure((IntPtr)returnObj, typeof(WAVEFORMATEX));

                if (format.nChannels != 0 && format.nSamplesPerSec != 0 && format.wBitsPerSample != 0)
                    Assert.AreEqual(format.nChannels * format.nSamplesPerSec * format.wBitsPerSample, format.nAvgBytesPerSec * 8, "The wave format was not valid.");
            }

            return returnObj;
        }
    }
}
