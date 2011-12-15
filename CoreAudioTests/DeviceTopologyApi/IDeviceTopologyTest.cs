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
using System.Linq;
using CoreAudioTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vannatech.CoreAudio.Interfaces;
using Vannatech.CoreAudio.Constants;
using System.Runtime.InteropServices;

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IDeviceTopology interface.
    /// </summary>
    [TestClass]
    public class IDeviceTopologyTest : TestClass<IDeviceTopology>
    {
        /// <summary>
        /// Tests that the connectors for a device topology may be received, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetConnector()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                UInt32 count;
                activation.GetConnectorCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    IConnector connector;
                    var result = activation.GetConnector(i, out connector);

                    Marshal.FinalReleaseComObject(connector);

                    AssertCoreAudio.IsHResultOk(result);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No subunits were found.");
        }

        /// <summary>
        /// Tests that the connector count for a device topology may be received, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetConnectorCount()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                var result = activation.GetConnectorCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, count, "The connector count was not received.");
            });
        }

        /// <summary>
        /// Tests that the device ID for a device topology may be received, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetDeviceId()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                string devId = "abc123";
                var result = activation.GetDeviceId(out devId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", devId, "The device ID was not received.");
            });
        }

        /// <summary>
        /// Tests that parts in a device topology may be received by ID, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetPartById()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                IConnector connector;
                activation.GetConnector(0, out connector);

                UInt32 partId;
                string globalId;
                ((IPart)connector).GetLocalId(out partId);
                ((IPart)connector).GetGlobalId(out globalId);

                IPart part;
                var result = activation.GetPartById(partId, out part);
                AssertCoreAudio.IsHResultOk(result);

                string verifyId;
                part.GetGlobalId(out verifyId);
                Assert.AreEqual(globalId, verifyId, "The part received did not match the expected part.");

                Marshal.FinalReleaseComObject(connector);
                Marshal.FinalReleaseComObject(part);
            });
        }

        /// <summary>
        /// Tests that a signal path can be resolved by a device topology, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetSignalPath()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                var allParts = TestUtilities.CreateIPartCollection();

                try
                {
                    for (int p1 = 0; p1 < allParts.Count(); p1++)
                    {
                        for (int p2 = 0; p2 < allParts.Count(); p2++)
                        {
                            IPartsList partsList;
                            var result = activation.GetSignalPath(allParts.ElementAt(p1), allParts.ElementAt(p2), false, out partsList);

                            if ((uint)result == TestUtilities.HRESULTS.E_NOTFOUND)
                                continue;

                            AssertCoreAudio.IsHResultOk(result);

                            var pCount = UInt32.MaxValue;
                            partsList.GetCount(out pCount);

                            Marshal.FinalReleaseComObject(partsList);

                            Assert.AreNotEqual(UInt32.MaxValue, pCount, "There count of parts cound was not received. This indicates an error in the signal path.");
                            tested = true;
                        }
                    }
                }
                finally
                {
                    foreach (var part in allParts)
                        Marshal.FinalReleaseComObject(part);
                }
            });

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No valid signal paths were found.");
        }

        /// <summary>
        /// Tests that the subunits for a device topology may be received, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetSubunit()
        {
            var tested = false;

            ExecuteDeviceActivationTest(activation =>
            {
                UInt32 count;
                activation.GetSubunitCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    ISubunit subunit;
                    var result = activation.GetSubunit(i, out subunit);

                    Marshal.FinalReleaseComObject(subunit);

                    AssertCoreAudio.IsHResultOk(result);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No subunits were found.");
        }

        /// <summary>
        /// Tests that the subunit count for a device topology may be received, for each available endpoint.
        /// </summary>
        [TestMethod]
        public void IDeviceTopology_GetSubunitCount()
        {
            ExecuteDeviceActivationTest(activation =>
            {
                var count = UInt32.MaxValue;
                var result = activation.GetSubunitCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, count, "The subunit count was not received.");
            });
        }
    }
}