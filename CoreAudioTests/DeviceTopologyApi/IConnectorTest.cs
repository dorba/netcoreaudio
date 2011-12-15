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
using System.Collections.Generic;
using Vannatech.CoreAudio.Enumerations;

namespace CoreAudioTests.DeviceTopologyApi
{
    // TODO: Create a custom test manager for IConnector tests, and properly dispose of COM interfaces.

    /// <summary>
    /// Tests all methods of the IConnector interface.
    /// </summary>
    [TestClass]
    public class IConnectorTest
    {
        /// <summary>
        /// Tests that each available connector in the system may be connectors to other connectors.
        /// </summary>
        [TestMethod]
        public void IConnector_ConnectTo()
        {
            int result = 0;
            IEnumerable<IConnector> inputConnectors, outputConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out inputConnectors, out outputConnectors);

            foreach (var cIn in inputConnectors)
            {
                bool isConnected;
                IConnector inOrig = null;
                cIn.IsConnected(out isConnected);

                if (isConnected)
                    cIn.GetConnectedTo(out inOrig);

                // New try connection to each output connector
                foreach (var cOut in outputConnectors)
                {
                    IConnector outOrig;
                    cOut.GetConnectedTo(out outOrig);

                    // Start by disconnecting the connectors
                    cIn.Disconnect();
                    cOut.Disconnect();

                    // Connect the input and output
                    result = cIn.ConnectTo(cOut);
                    AssertCoreAudio.IsHResultOk(result);

                    // Verify the connection was made.
                    string cInId, idVerify;
                    ((IPart)cIn).GetGlobalId(out cInId);
                    cOut.GetConnectorIdConnectedTo(out idVerify);
                    Assert.AreEqual(cInId, idVerify, "The connector IDs do not match.");

                    // Return the output connector to original state.
                    cOut.Disconnect();
                    cOut.ConnectTo(outOrig);
                }

                // Return the input connector to original state.
                cIn.Disconnect();

                if (inOrig != null)
                    cIn.ConnectTo(inOrig);
            }
        }

        /// <summary>
        /// Tests that each available connector in the system may be disconnected.
        /// </summary>
        [TestMethod]
        public void IConnector_Disconnect()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                bool isConnected;
                IConnector conOrig = null;
                con.IsConnected(out isConnected);

                if (isConnected)
                    con.GetConnectedTo(out conOrig);

                result = con.Disconnect();

                // HRESULT should either be S_OK, or E_NOTFOUND
                // E_NOTFOUND is returned when Disconnect method is called on a connector that is already disconnected.
                if (isConnected) AssertCoreAudio.IsHResultOk(result);
                else Assert.AreEqual(TestUtilities.HRESULTS.E_NOTFOUND, (uint)result, "A disconnected connector did not return E_NOTFOUND HRESULT.");

                // Verify connected state is now false.
                con.IsConnected(out isConnected);
                Assert.IsFalse(isConnected, "The connector was not properly disconnected.");

                // Return connector to original state.
                if (conOrig != null)
                    con.ConnectTo(conOrig);
            }
        }

        /// <summary>
        /// Tests that the global ID may be received, for each available connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_GetConnectorIdConnectedTo()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                bool isConnected;
                con.IsConnected(out isConnected);

                if (isConnected)
                {
                    IConnector cTo;
                    con.GetConnectedTo(out cTo);

                    string expectedId, idConTo;
                    ((IPart)cTo).GetGlobalId(out expectedId);
                    result = con.GetConnectorIdConnectedTo(out idConTo);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreEqual(expectedId, idConTo, "The connected to ID does not match the actual ID of the part.");
                }
            }
        }

        /// <summary>
        /// Tests that the other connector may be received, for each applicable connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_GetConnectedTo()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                IConnector cTo;
                result = con.GetConnectedTo(out cTo);

                // A result of E_NOTFOUND is valid in this context.
                if ((uint)result == TestUtilities.HRESULTS.E_NOTFOUND) continue;

                // Otherwise the result should be S_OK
                AssertCoreAudio.IsHResultOk(result);
            }
        }

        /// <summary>
        /// Tests that the data flow may be received, for each available connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_GetDataFlow()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                var valOne = DataFlow.In;
                var valTwo = DataFlow.Out;

                result = con.GetDataFlow(out valOne);
                AssertCoreAudio.IsHResultOk(result);

                result = con.GetDataFlow(out valTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(valOne, valTwo, "The data flow was not received.");
            }
        }

        /// <summary>
        /// Tests that the local device ID may be received, for each available connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_GetDeviceIdConnectedTo()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                bool isConnected;
                con.IsConnected(out isConnected);

                if (isConnected)
                {
                    IConnector cTo;
                    con.GetConnectedTo(out cTo);

                    IDeviceTopology topology;
                    ((IPart)cTo).GetTopologyObject(out topology);

                    string expectedId, devIdConTo;
                    topology.GetDeviceId(out expectedId);
                    result = con.GetDeviceIdConnectedTo(out devIdConTo);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreEqual(expectedId, devIdConTo, "The connected to device ID does not match the actual device ID.");
                }
            }
        }

        /// <summary>
        /// Tests that the connector type may be received, for each available connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_GetType()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                // Try confiming that the type is changed from unknown.
                var cType = ConnectorType.Unknown_Connector;
                result = con.GetType(out cType);
                AssertCoreAudio.IsHResultOk(result);

                if (cType == ConnectorType.Unknown_Connector)
                {
                    // If it's still unknown, verify that this is correct.
                    var cType2 = ConnectorType.Network;
                    con.GetType(out cType2);

                    AssertCoreAudio.IsHResultOk(result);
                    Assert.AreEqual(ConnectorType.Unknown_Connector, cType2, "The connector type was not received.");
                }
                else
                {
                    // The value being changed is enough to verify correctness of the method.
                }
            }
        }

        /// <summary>
        /// Tests that the connected state may be received, for each available connector in the system.
        /// </summary>
        [TestMethod]
        public void IConnector_IsConnected()
        {
            int result = 0;
            IEnumerable<IConnector> allConnectors;
            ToConnectors(TestUtilities.CreateIPartCollection(), out allConnectors);

            foreach (var con in allConnectors)
            {
                var valOne = true;
                var valTwo = false;

                result = con.IsConnected(out valOne);
                AssertCoreAudio.IsHResultOk(result);

                result = con.IsConnected(out valTwo);
                AssertCoreAudio.IsHResultOk(result);

                Assert.AreEqual(valOne, valTwo, "The connected state was not received.");
            }
        }

        #region Helper Methods

        /// <summary>
        /// Takes a collection of IPart objects and returns the subset of the collection that are IConnector objects.
        /// </summary>
        /// <param name="allParts">A collection that may contain both subunits and connectors.</param>
        /// <param name="allConnectors">A collection of connectors contained within the part collection.</param>
        private static void ToConnectors(IEnumerable<IPart> allParts, out IEnumerable<IConnector> allConnectors)
        {
            var cList = new List<IConnector>();
            IEnumerable<IConnector> cIns, cOuts;
            ToConnectors(allParts, out cIns, out cOuts);

            cList.AddRange(cIns);
            cList.AddRange(cIns);

            allConnectors = cList;
        }

        /// <summary>
        /// Takes a collection of IPart objects and returns the subset of the collection that are separated input and output IConnector objects.
        /// </summary>
        /// <param name="allParts">A collection that may contain both subunits and connectors.</param>
        /// <param name="inputConnectors">A collection of input connectors contained within the part collection.</param>
        /// <param name="outputConnectors">A collection of output connectors contained within the part collection.</param>
        private static void ToConnectors(IEnumerable<IPart> allParts, out IEnumerable<IConnector> inputConnectors, out IEnumerable<IConnector> outputConnectors)
        {
            var inList = new List<IConnector>();
            var outList = new List<IConnector>();

            foreach (IPart part in allParts)
            {
                PartType pType;
                part.GetPartType(out pType);

                if (pType == PartType.Connector)
                {
                    DataFlow flow;
                    var connector = (IConnector)part;
                    connector.GetDataFlow(out flow);

                    switch (flow)
                    {
                        case DataFlow.In:
                            inList.Add(connector);
                            break;

                        case DataFlow.Out:
                            outList.Add(connector);
                            break;
                    }
                }
            }

            if (!inList.Any()) Assert.Inconclusive("The test cannot be run properly. No connectors were found.");
            if (!outList.Any()) Assert.Inconclusive("The test cannot be run properly. No connectors were found.");

            inputConnectors = inList;
            outputConnectors = outList;
        }

        #endregion
    }
}