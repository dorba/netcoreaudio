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
using Vannatech.CoreAudio.Enumerations;
using System.Runtime.InteropServices;

namespace CoreAudioTests.DeviceTopologyApi
{
    /// <summary>
    /// Tests all methods of the IPart interface.
    /// </summary>
    [TestClass]
    public class IPartTest : TestClass<IPart>
    {
        /// <summary>
        /// Tests that any valid interface can be activated, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_Activate()
        {
            // There's should be no need to test this method. It's implicitly tested through other classes which require this method to work.
            // TODO: Add a test (low priority).
        }

        /// <summary>
        /// Tests that an incoming parts list may be enumerated, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_EnumPartsIncoming()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                IPartsList partsList;
                var result = part.EnumPartsIncoming(out partsList);
                Manager.EnsureDisposal(partsList);

                // HRESULT of E_NOTFOUND is valid when no incoming parts exist.
                if ((uint)result == TestUtilities.HRESULTS.E_NOTFOUND) return;

                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that an incoming parts list may be enumerated, for each applicable part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_EnumPartsOutgoing()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                IPartsList partsList;
                var result = part.EnumPartsOutgoing(out partsList);
                Manager.EnsureDisposal(partsList);

                // HRESULT of E_NOTFOUND is valid when no outgoing parts exist.
                if ((uint)result == TestUtilities.HRESULTS.E_NOTFOUND) return;

                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// Tests that a control interface can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetControlInterface()
        {
            var tested = false;

            ExecuteCustomTest(new PartTestManager(), part =>
            {
                UInt32 count;
                part.GetControlInterfaceCount(out count);

                for (uint i = 0; i < count; i++)
                {
                    IControlInterface ctrl;
                    var result = part.GetControlInterface(i, out ctrl);
                    Manager.EnsureDisposal(ctrl);

                    AssertCoreAudio.IsHResultOk(result);
                    tested = true;
                }
            });

            if (!tested) Assert.Inconclusive("The test cannot be run properly. No control interfaces were found.");
        }

        /// <summary>
        /// Tests that the control interface count can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetControlInterfaceCount()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                var count = UInt32.MaxValue;
                var result = part.GetControlInterfaceCount(out count);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, count, "The count was not received.");
            });
        }

        /// <summary>
        /// Tests that the global ID can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetGlobalId()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                string globalId = "abc123";
                var result = part.GetGlobalId(out globalId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", globalId, "The global ID was not received.");
            });
        }

        /// <summary>
        /// Tests that the local ID can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetLocalId()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                UInt32 localId = UInt32.MaxValue;
                var result = part.GetLocalId(out localId);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(UInt32.MaxValue, localId, "The local ID was not received.");
            });
        }

        /// <summary>
        /// Tests that the global ID can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetName()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                string name = "abc123";
                var result = part.GetName(out name);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual("abc123", name, "The name was not received.");
            });
        }

        /// <summary>
        /// Tests that the part type can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetPartType()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                PartType typeOne = PartType.Connector;
                PartType typeTwo = PartType.Subunit;

                var result = part.GetPartType(out typeOne);
                AssertCoreAudio.IsHResultOk(result);

                result = part.GetPartType(out typeTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(typeOne, typeTwo, "The part type was not received.");
            });
        }

        /// <summary>
        /// Tests that the part type can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetSubType()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                var subType = Guid.Empty;
                var result = part.GetSubType(out subType);

                AssertCoreAudio.IsHResultOk(result);
                Assert.AreNotEqual(Guid.Empty, subType, "The sub type was not received.");
            });
        }

        /// <summary>
        /// Tests that the topology object can be received, for each available part in the system.
        /// </summary>
        [TestMethod]
        public void IPart_GetTopologyObject()
        {
            ExecuteCustomTest(new PartTestManager(), part =>
            {
                IDeviceTopology topology;
                var result = part.GetTopologyObject(out topology);
                Manager.EnsureDisposal(topology);

                AssertCoreAudio.IsHResultOk(result);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IPart_RegisterControlChangeCallback()
        {
            Assert.Fail("TODO: Implement test for RegisterControlChangeCallback method");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IPart_UnregisterControlChangeCallback()
        {
            Assert.Fail("TODO: Implement test for UnregisterControlChangeCallback method");
        }

        /// <summary>
        /// Class used to manage testing of IPart interface.
        /// </summary>
        private class PartTestManager : TestManager<IPart>
        {
            protected override void OnRun()
            {
                var allParts = TestUtilities.CreateIPartCollection();

                try
                {
                    foreach (var part in allParts)
                    {
                        OnTestReady(part);
                    }
                }
                finally
                {
                    foreach (var part in allParts)
                        Marshal.FinalReleaseComObject(part);
                }
            }
        }
    }
}